# Binding System Extensibility

As of version 2.0, the binding system is pretty powerful, so let's have a look how to exploit that.

## Binding Properties

Each binding is, in fact, a `Dictionary` of its properties - things like the executable delegate, javascript translation, the original string and so on. The property can be looked up by its type using `GetProperty(Type)` method on the `IBinding` instance or you can also use generic extension method `binding.GetProperty<MyBindingProperty>()`. These properties cannot be added manually, they are fixed when the binding is created by the dothtml page compiler, but there is a concept of "property resolver" which can compute a property from the properties that are already present in the binding - in fact almost all properties are resolved using one of these resolved from the base ones:
* The `KnockoutJsExpressionBindingProperty` (that contain Javascript Syntax Tree of the binding) is computed from `ParsedExpressionBindingProperty` and `DataContextStack`
* `ParsedExpressionBindingProperty` is also a resolved property, specifically from `OriginalStringBindingProperty`, `DataContextStack` and `BindingParserOptions`
* `BindingParserOptions` may be computed from the binding type, but here, the resolver is used just as a fallback, it is normally specified at binding construction.

You may now doubt about runtime performance if the property is parsed every time the property is requested. For that reason, the result is cached in the dictionary, so when resolved, the property behaves like it would be assigned to a constructor. And the binding instance is shared between requests, so it the expression is compiled only once in the application lifetime. Because of that, it's very important that the functions are pure and their result is immutable.

As you can see, these resolvers may form a dependency graph (only an acyclic one), if you are interested, almost all default resolvers are in the [GeneralBindingPropertyResolvers](https://github.com/riganti/dotvvm/blob/master/src/DotVVM.Framework/Compilation/Binding/GeneralBindingPropertyResolvers.cs) class. As you can see, all of them are just simple functions that take the dependencies as arguments and return the result. The main point of this is extensibility, so let's write a custom property and resolver:

It will contain a property used in the binding, for example a PropertyInfo of `Name` for binding `{value: Name}` or `{value: _root.Article.Name}`. First, we have to declare the binding property type:

```CSHARP
public sealed class UsedPropertyBindingProperty {
    public readonly PropertyInfo PropertyInfo;
    public UsedPropertyBindingProperty(PropertyInfo prop) {
        this.PropertyInfo = prop;
    }
}
```

Then you can write the function. It requires the `ParsedExpressionBidningProperty` which contains the parsed semantic tree of the binding.

```cs
public class MyResolvers {
    public UsedPropertyBindingProperty GetUsedProperty(ParsedExpressionBindingProperty parsedExpression) {
        var expr = parsedExpression.Expression;

        // unwrap possible casts
        while (expr.NodeType == ExpressionType.Convert) expr = ((UnaryExpresssion)expr).Operand;

        // check the node type
        if (expr.NodeType == ExpressionType.MemberAccess) {
            var member = ((MemberExpression)expr).Member;
            // check type type of the member
            if (member is PropertyInfo) {
                return new UsedPropertyBindingProperty((PropertyInfo)member);
            } else {
                throw new Exception($"Member expression {expr} is not a property access.");
            }
        } else {
            throw new Exception($"Expression {expr} is not a property access.");
        }
    }
}
```

You can see, the resolver may throw an exception when the expression is not a member access (for example when the binding would be `{value: Number + 1}`), this exception is thrown when the property resolver is invoked - when the `GetProperty` method is called, which means that it will be thrown in control lifecycle event at runtime. If you'd like to report an error during the page compilation if the property can't be resolved, you can use `[BindingCompilationRequirements(required: new [] { typeof(UsedPropertyBindingProperty) })]` attribute on the control property that contains the affected binding. Or if the property is optional, you can use `binding.GetProperty<...>(ErrorHandlingMode.ReturnNull)` to return null without throwing exception.

Last thing missing is registration of the `MyResolvers` class. It can be added to the `BindingCompilationOptions.TransformerClasses` property using the Asp.Net Core configuration in the `ConfigureServices` method:

```cs
services.Configure<BindingCompilationOptions>(o => {
    o.TransformerClasses.Add(new MyResolvers());
});
```

## Derived Bindings

The binding properties allow you to create almost anything from other binding properties - including other bindings. Derived binding can, for example, contain a negated expression:

```cs
public NegatedBindingExpression NegateBinding(ParsedExpressionBindingProperty e, IBinding binding) {
    return new NegatedBindingExpression(binding.DeriveBinding(
        new ParsedExpressionBindingProperty(
            // transform `!expr` -> `expr`
            e.Expression.NodeType == ExpressionType.Not ? e.Expression.CastTo<UnaryExpression>().Operand :
            // `a == b` -> `a != b`
            e.Expression.NodeType == ExpressionType.Equal ? e.Expression.CastTo<BinaryExpression>().UpdateType(ExpressionType.NotEqual) :
            // `a != b` -> `a == b`
            e.Expression.NodeType == ExpressionType.NotEqual ? e.Expression.CastTo<BinaryExpression>().UpdateType(ExpressionType.Equal) :
            // `expr` -> `!expr`
            (Expression)Expression.Not(e.Expression)
        )
    ));
}
```

Note the usage of `DeriveBinding` extension method on the `IBinding` instance - it copies the essential properties from the base binding (like data context, location in page), creates a binding of the same type and adds a new property of type `ParsedExpressionBindingProperty`. You could potentially create a binding just from string expression (`OriginalStringBindingProperty`), if don't want to bother with the expression trees.

This binding property is present by default in the DotVVM Framework, but you can define your own in the same way.

## Post-processing Existing Properties

You can register a resolver with signature like:

```cs
public ParsedExpressionBindingProperty WrapExpression(ParsedExpressionBindingProperty prop, some other dependencies) {
    return new ParsedExpressionBindingProperty(Expression.Add(prop.Expression, Expression.Contant(1)));
}
```

It will be executed always after the property is resolved, which means that all bindings will be incremented by one. Incrementing all bindings does not seem to be much useful, but I'm sure post-processing expressions or tweaking generated Javscript is really powerfull metaprogramming technique. Just please, use it wisely, all bindings incremented by one may be pretty tricky to debug for your teammates.

### Custom Binding Type

You can even create your own binding, you just need to inherit from `BindingExpression` and register the name at `ControlResolverBase.BindingTypes` with its `BindingParserOptions`. You can have a look how `ResourceBindingExpression` is defined in the framework:

```cs

[BindingCompilationRequirements(
    // the binding implicitly requires an executable BindingDelegate
    required: new[] {typeof(CompiledBindingExpression.BindingDelegate)}
)]
[Options]
public class ResourceBindingExpression : BindingExpression, IStaticValueBinding
{
    // You need a contructor with this exact signature
    public ResourceBindingExpression(BindingCompilationService service, IEnumerable<object> properties) : base(service, properties) { }

    // You can have helpers for the binding properties, so they can be accessed like normal .NET properties
    public CompiledBindingExpression.BindingDelegate BindingDelegate => this.GetProperty<CompiledBindingExpression.BindingDelegate>();
    public Type ResultType => this.GetProperty<ResultTypeBindingProperty>().Type;

    // The [Options] attribute that is applied to this class.
    public class OptionsAttribute : BindingCompilationOptionsAttribute
    {
        public override IEnumerable<Delegate> GetResolvers() => new Delegate[] {
            // Here you can return your own resolvers. These override the default ones from the resolver classes, so you may have a custom parser, translator to Javascript or anything you want.
        };
    }
}

// It's also useful to have a generic variant of the class. This will allow you to use IStaticValueBinding<TResultType> with your binding type
public class ResourceBindingExpression<T> : ResourceBindingExpression, IStaticValueBinding<T>
{
    public ResourceBindingExpression(BindingCompilationService service, IEnumerable<object> properties) : base(service, properties) { }

    public new CompiledBindingExpression.BindingDelegate<T> BindingDelegate => base.BindingDelegate.ToGeneric<T>();
}
```

And it is registered in the collection like this:

```cs
ControlResolverBase.BindingTypes.Add(ParserConstants.ResourceBinding, BindingParserOptions.Create(typeof(ResourceBindingExpression<>)));
```