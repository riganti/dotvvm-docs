# Custom JavaScript translators

DotVVM implements an extensible binding translations mechanism, which is an API for building **abstract syntax trees** ([AST](https://en.wikipedia.org/wiki/Abstract_syntax_tree)) that represent JavaScript expressions. We can then register custom translators for specific .NET methods, and extend the set of supported methods used in data bindings.

## Register custom translator

Every translator must implement the `IJavascriptMethodTranslator` interface. However, it is a bit simpler to utilize a general-purpose implementation called `GenericMethodCompiler` exposed by DotVVM.

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

### Example 2: Translator for `Array.Length` property getter method

In the following code snippet, we will create a custom translation for the `Length` property getter method on the type `System.Array`. In this case, we need to work with a single arguments that represents the array. Since the `Length` property is bound to a specific instance, the array argument is passed as the `this` pointer, which corresponds to the `args[0]` expression. An example translation can be seen in the code snippet below.

```csharp
var translator = new GenericMethodCompiler(a => a[0].Member("length"));
```
When used, the translator above will yield the following JavaScript expression: `args[0].length`. Now, the only thing left is registering this translation. We can perform the registration using the following code snippet.

```csharp
AddPropertyGetterTranslator(typeof(Array), nameof(Array.Length), translator);
```

## See also

* [Data-binding overview](~/pages/concepts/data-binding/binding-context)
* [Supported expressions and .NET methods](~/pages/concepts/data-binding/supported-expressions)
* [Abstract syntax tree](https://en.wikipedia.org/wiki/Abstract_syntax_tree)