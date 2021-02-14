## Binding Extension Parameters

Any DotVVM binding is an expression that may use certain identifiers and there are a few types of them:

* Class Names - For example, `System.String`, these don't have any value, you can only access (static) members on them.
	The set of accessible classes is controlled by `NamespaceImport`s, you can set it globally in the `DotvvmConfiguration` or locally using `@import` directive.
* DataContext Parameters - For example, `_this`, `_parent4` or `_root`. These identifiers have a value and you can do anything with them and the value comes from the list of all data contexts of the invoking control.
* Properties/Methods of current view model - For example `MyProperty`. This is just an alias for `_this.MyProperty`, the value comes from the data context.
* Extension Parameters - For example, `_index`, `_api`. This is also an identifier with a value and these parameters can be user-defined. The value is usually computed from some properties on the invoking control and can be used arbitrarily in the binding. The point of this document is to have a look in depth how do they work.


### Extension Parameter API

The new ExtensionParameter is defined by the `BindingExtensionParameter` abstract class. In order to be recognized by the binding compiler, it has to be registered globally in `DotvvmConfiguration.Markup.DefaultExtensionParameters` or locally in `DataContextStack.ExtensionParameters`. In order to make it useful, we will have to define some methods and values:

* `identifier` - A name that will be used in binding expression to reference this parameter
* `type` - Type of the parameter. If you'd do the `_index` parameter it would be `System.Int32`, the `_page` is of type `BindingPageInfo`. You can, of course, create a new type that will be used specifically for this purpose.
* `inherit` - When the extension parameter is introduced in a specific data context (like the `_index` that is only present in Repeater-like controls), this parameter controls if the parameter will also be valid in child data contexts.
* `GetServerEquivalent(Expression controlParameter)` - When the binding is compiled to be used in .NET code, this method is invoked to get an expression that returns the value of the parameter. You can use a reference to the current control in the code.
* `GetJsTranslation(JsExpression dataContext)` - This gets the expression that can be used in the translated Javascript expressions. Here, you can use a reference to the knockout context to compute the value.


For example, you may know the [`@import` directive](/Pages/advanced-ioc-di-container.md) - it basically introduces a new extension parameter that represents a service imported from `IServiceProvider`. Let's have a look at how we could implement it. First, we will need class inheriting from `BindingExtensionParameter`:

```csharp
public class InjectedServiceExtensionParameter : BindingExtensionParameter
{
	...
}
```

Then, we'll need a constructor that sets the parameters of the base class. We don't need to do anything inside it, and we will let the users decide which `type` they want to import and which `identifier` they want to give it:

```csharp
public InjectedServiceExtensionParameter(string identifier, ITypeDescriptor type)
	: base(identifier, type, inherit: true) { }
```

We'll need to implement the runtime behavior of the parameter - which value should the identifier have. The parameter is translated into an expression that may use the current control by the `GetServerEquivalent` method:

```csharp
public override Expression GetServerEquivalent(Expression controlParameter)
{
	// Extract System.Type from the ITypeDescriptor, so we can use it to invoke `IServiceProvider.GetService`
	var type = ResolvedTypeDescriptor.ToSystemType(this.ParameterType);
	// Create expression that invokes the `ResolveStaticCommandService` method that is defined bellow
	var expr = ExpressionUtils.Replace((DotvvmBindableObject c) => ResolveStaticCommandService(c, type), controlParameter);
	// And cast the result to the expected type
	return Expression.Convert(expr, type);
}

/// This is a helper method that finds a IServiceProvider in the control tree and resolves a service of the `type`
private object ResolveStaticCommandService(DotvvmBindableObject c, Type type)
{
	// The IDotvvmRequestContext is saved in the control tree
	var context = (IDotvvmRequestContext)c.GetValue(Internal.RequestContextProperty, true);
	return context.Services.GetService(type);
}
```

The `ExpressionUtils.Replace` is a helper method that exploits the fact that we are using standard Linq.Expressions to represent bindings during compilation. It takes the lambda as Expression and simply replaces occurrences of `c` with the expression in `controlParameter`.

Finally, we'll have to say how should it be translated to Javascript. Unfortunately, there is not a reasonable way to translate this into Javascript, so we will simply throw an exception. It will basically forbid the usage of this extension parameter in bindings translated to Javascript (`value` and `staticCommand` bindings)

```csharp
public override JsExpression GetJsTranslation(JsExpression dataContext)
{
	throw new InvalidOperationException($"Can't use injected services in javascript-translated bindings.");
}
```


> If you'd like to see implementation of a few more, have a look at [the real implementation on Github](https://github.com/riganti/dotvvm/blob/master/src/DotVVM.Framework/Compilation/ControlTree/BindingExtensionParameter.cs)

### Registration

It's very simple to register the extension parameter globally (for all pages), you simply put the modification of `DotvvmConfiguration` into a startup class:

```csharp
config.Markup.DefaultExtensionParameters.Add(
	new InjectedServiceExtensionParameter("myCoolService", new ResolvedTypeDescriptor(typeof(MyCoolService))));
```

The parameters are part of the data context (represented by `DataContextStack` class), so they can be added by `DataContextChangeAttributes` and you can use to set the extension parameter locally. For example, the `_index` and `_collection` parameters are created only inside Repeater-like controls. DotVVM compiler tracks the data context for each control and each control may modify the data context that will be inside its children or inside its templates.

This is done by annotating the template property or the control by an attribute that inherits `DataContextChangeAttribute`. For example, `Repeater` changes data context of its `ItemTemplate` into the type of element of the collection bound in `DataSource` property. This is done by annotating the property with two change attributes - the changes the type to be the type of the `DataSource` property and the second changes the type to be element of that collection:

```csharp

[ControlPropertyBindingDataContextChange(nameof(DataSource))]
[CollectionElementDataContextChange(1)]
public ITemplate ItemTemplate { ... }
```

These attributes can also add extension parameters to the content. For example, if we'd like to allow children of our control use some injected service parameter, we could implement the attribute as follows:

```csharp
public class CollectionElementDataContextChangeAttribute : DataContextChangeAttribute
{
	public override int Order { get; }

	public CollectionElementDataContextChangeAttribute(int order)
	{
		Order = order;
	}

	public override ITypeDescriptor GetChildDataContextType(ITypeDescriptor dataContext, IDataContextStack controlContextStack, IAbstractControl control, IPropertyDescriptor property = null) => null; // we don't want to change the type

	public override Type GetChildDataContextType(Type dataContext, DataContextStack controlContextStack, DotvvmBindableObject control, DotvvmProperty property = null) => null;

	public override IEnumerable<BindingExtensionParameter> GetExtensionParameters(ITypeDescriptor dataContext) =>
		return new BindingExtensionParameter[] {
			new InjectedServiceExtensionParameter("myService", new ResolvedTypeDescriptor(typeof(IMyService)))
		};
}
```


