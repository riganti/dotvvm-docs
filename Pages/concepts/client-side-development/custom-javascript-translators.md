# Custom JavaScript translators

DotVVM implements an extensible binding translations mechanism, which is an API for building **abstract syntax trees** ([AST](https://en.wikipedia.org/wiki/Abstract_syntax_tree)) that represent JavaScript expressions. We can then register custom translators for specific .NET methods, and extend the set of supported methods used in data bindings.

## Register custom translator

Every translator must implement the `IJavascriptMethodTranslator` interface. However, usually it is simpler to implement it with a lambda function wrapped in `GenericMethodCompiler`.

### Example 1: translator for `Enumerable.Where` method

In the following code snippet, we will create a custom translation for the method `Where(source, predicate)` on the type `System.Linq.Enumerable`. Whenever constructing the `GenericMethodCompiler`, we need to provide a transformation for the provided method arguments. In this case, we need to work with two arguments: 1) source collection, 2) predicate that filters elements. Therefore, an example translator that combines these arguments into a JavaScript expression can be seen in the code snippet below:

```CSHARP
var translator = new GenericMethodCompiler(args => args[1].Member("filter").Invoke(args[2]));
```

In the code snippet above, `args[1]` represents the source collection, and `args[2]` represents the predicate. When used, this translator will yield the following JavaScript expression: `args[1].filter(args[2])`.

At this point, you might notice that we are indexing the `args` array from 1. Actually, the zero-th array element represents the `this` pointer. Since in C# the `Where` method is static, the zero-th array element is `null`, and as such is not used by the translator. The only step left is to register the new translator. This can be achieved in `DotvvmStartup.cs` file using the following code snippet.

```CSHARP
var whereMethodInfo = /* obtain method info using reflection */;
config.Markup.JavascriptTranslator.MethodCollection.AddMethodTranslator(whereMethodInfo, translator);
```

Since DotVVM 4.3, instead of obtaining MethodInfo, we can supply a System.Linq.Expressions lambda, which uses the method we want to translate.
We need to supply a type argument for C# to compile the code, but we want to register the translation for all types `T` in `IEnumerable<T>`.
DotVVM defines a special `Generic.T` type to instruct the method registry, that this the translation works for any type.

```CSHARP
config.Markup.JavascriptTranslator.MethodCollection.AddMethodTranslator(
    () => Enumerable.Empty<Generic.T>().Where(_ => true),
    translator
)
```

### Example 2: Translator for `Array.Length` property getter method

In the following code snippet, we will create a custom translation for the `Length` property getter method on the type `System.Array`. In this case, we need to work with a single arguments that represents the array. Since the `Length` property is bound to a specific instance, the array argument is passed as the `this` pointer, which corresponds to the `args[0]` expression. An example translation can be seen in the code snippet below.

```CSHARP
var translator = new GenericMethodCompiler(a => a[0].Member("length"));
```

When used, the translator above will yield the following JavaScript expression: `args[0].length`. Now, the only thing left is registering this translation. We can perform the registration using the following code snippet.

```CSHARP
config.Markup.JavascriptTranslator.MethodCollection.AddPropertyTranslator(
    () => default(Generic.T[]).Length,
    translator
);
```

## See also

* [Control development overview](overview)
* [Data-binding overview](~/pages/concepts/data-binding/binding-context)
* [Supported expressions and .NET methods](~/pages/concepts/data-binding/supported-expressions)
* [Binding system extensibility](binding-extensibility)
* [Binding extension parameters](binding-extension-parameters)
