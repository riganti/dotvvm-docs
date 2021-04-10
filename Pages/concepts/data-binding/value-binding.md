# Value binding

The **value binding** is the most frequently used binding in DotVVM. It allows you to bind a property in the viewmodel to a property of a control in the DotHTML file, or just render the value as a text. 

The data-binding works in both directions - whenever the `Url` property changes, the control that this property is bound to, will be updated accordingly. Also, if the user makes a change in the control, the property value will be written back to the viewmodel.

## Example

Let's have the following viewmodel:

```CSHARP
public class MyViewModel {
    ...
    public string Url { get; set; }
    ...
}
```

In the DotHTML markup, you can bind the property to the `Text` property of a [TextBox](~/controls/builtin/TextBox) control:

```DOTHTML
<dot:TextBox Text="{value: Url}" />
```

If you run the page and view the page source code, you'll see that DotVVM translated the binding into a [Knockout](https://knockoutjs.com/) `data-bind` attribute. DotVVM uses this popular JavaScript library to perform the data-binding and provide the MVVM experience. This is how the HTML will look like in the browser:

```DOTHTML
<input type="text" data-bind="attr: { 'href': Url }" />
```

## Supported expressions in value binding

Let's look at the binding expression:

```DOTHTML
{value: Url}
```

The `value` prefix indicates the type of the binding - **value binding**. There are [other types of binding expressions](~/pages/concepts/data-binding/overview).

The `Url` is an expression that will be evaluated in the browser. The expression can use the public properties in the viewmodel, 
access elements of collections, call some methods (only those which DotVVM can translate in the JavaScript), and use a few operators. 

> Remember that DotVVM translates expressions specified in value binding in JavaScript so they can be evaluated using Knockout JS. Not all expressions can be translated - see the following section.

* Member access
   * `SomeProperty`
   * `SomeProperty.OtherProperty`
* Access the parent [binding contexts](~/pages/concepts/data-binding/binding-context)
  * `_root.SomeProperty`
  * `_parent.SomeProperty`
* Collections and array elements access
   * `SomeCollection[6]`
   * `SomeCollection[6].OtherProperty`
* Access the collection metadata (if the current [binding context](~/pages/concepts/data-binding/binding-context) is a collection)
   * `_collection.Index`
   * `_collection.IsOdd`, `_collection.IsEven`...
* Binary operations
   * `SomeProperty >= 0`
   * `SomeProperty + 1`
   * `SomeProperty != OtherProperty`
* Ternary conditional operator
   * `SomeProperty ? "some string" : "other string"`
* Method invocation (only supported methods)
   * `SomeMethod(argument)`
* Block expression
   * `(expression1; expression2; expression3)`
   * *Note*: This is a composition of supported expressions within one data-binding. DotVVM uses parentheses `( ... )` to enclose expressions as compared to C#, which uses curly braces `{ ... }`. Result type of any composite expression is determined by the last child expression.
* Lambda functions (**new in version 3.0**)
   * `(int intArg, string strArg) => SomeMethod(intArg, strArg)`
   * *Note*: Type inference for lambda parameters is not available in version 3.0, therefore type information needs to be explicitly supplied together with lambda parameters definition. Type inference is an upcoming feature in DotVVM 3.1.
* Local variables (**new in version 3.0**)
   * `var myVariable = SomeFunction(arg1, arg2); SomeMethod(myVariable)`
   * *Note*: Variables are by design single-assignable (immutable). Variables may shadow property names and previously defined variables.

### .NET methods supported in value bindings

DotVVM can translate several .NET methods on basic types or collections to JavaScript, so you can safely use them in value bindings.

#### String methods
* `String.Length`

#### Collection methods
* `ICollection.Count` and `Array.Length`

#### Enum methods
* `Enums.GetNames<TEnum>()`

#### Task methods
* `Task<T>.Result`

#### Formatting
* `String.Format(format, arg1 [, arg2, [ arg3]])`
* `String.Format(format, argumentArray)`
* `Object.ToString()`
* `Convert.ToString()`
* `DateTime.ToString()` and `DateTime.ToString(format)`
* <code><em>numericType</em>.ToString()</code> and <code><em>numericType</em>.ToString(format)</code>

#### Nullable types
* `Nullable<T>.HasValue`
* `Nullable<T>.Value`

#### LINQ methods
* `Enumerable.Select<T,U>(IEnumerable<T> collection, Func<T,U> selector)`
* `Enumerable.Where<T>(IEnumerable<T> collection, Func<T,bool> predicate)`
* *Note*: These methods are **new in version 3.0**.

#### REST API binding methods
* `Api.RefreshOnChange`
* `Api.RefreshOnEvent`
* `Api.PushEvent` 
* *Note*: See [REST API bindings](~/pages/concepts/respond-to-user-actions/rest-api/bindings/overview) for more info

### Provide custom method translators

It is possible to register custom translators for any method. See [Provide custom JavaScript translators](~/pages/concepts/control-development/provide-custom-method-translators) for more information.  

### Upcoming support for .NET methods in DotVVM 3.1

We plan to add support for the following methods in DotVVM 3.1.

#### LINQ methods
* `Enumerable.All<T>(IEnumerable<T> collection, Func<T,bool> predicate)`
* `Enumerable.Any<T>(IEnumerable<T> collection)`
* `Enumerable.Any<T>(IEnumerable<T> collection, Func<T,bool> predicate)`
* `Enumerable.Concat<T>(IEnumerable<T> first, IEnumerable<T> second)`
* `Enumerable.Distinct<T>(IEnumerable<T> collection)`
   * *Note*: this method is restricted to primitive types.
* `Enumerable.FirstOrDefault<T>(IEnumerable<T> collection)` and `Enumerable.FirstOrDefault<T>(IEnumerable<T> collections, Func<T,bool> predicate)`
* `Enumerable.LastOrDefault<T>(IEnumerable<T> collection)` and `Enumerable.LastOrDefault<T>(IEnumerable<T> collection, Func<T,bool> predicate)`
* `Enumerable.Max<T>(IEnumerable<T> collection)` and `Enumerable.Max<T,U>(IEnumerable<T> collection, Func<T,U> selector)`
   * *Note*: these methods are restricted to numeric types
* `Enumerable.Min<T>(IEnumerable<T> collection)` and `Enumerable.Min<T,U>(IEnumerable<T> collection, Func<T,U> selector)`
   * *Note*: these methods are restricted to numeric types
* `Enumerable.OrderBy<T,U>(IEnumerable<T> collection, Func<T,U> selector)`
   * *Note*: this method is restricted to primitive types
* `Enumerable.OrderByDescending<T,U>(IEnumerable<T> collection, Func<T,U> selector)`
   * *Note*: this method is restricted to primitive types
* `Enumerable.Skip<T>(IEnumerable<T> collection, int count)`
* `Enumerable.Take<T>(IEnumerable<T> collection, int count)`
* `Enumerable.ToArray<T>(IEnumerable<T> collection)`
* `Enumerable.ToList<T>(IEnumerable<T> collection)`

#### String methods
* `String.Contains(string value)` and `String.Contains(char value)`
* `String.EndsWith(string value)` and `String.EndsWith(char value)`
* `String.IndexOf(string value)` and `String.IndexOf(char value)`
* `String.IndexOf(string value, int startIndex)` and `String.IndexOf(char value, int startIndex)`
* `String.IsNullOrEmpty(string value)`
* `String.Join(string separator, IEnumerable<string> values)` and `String.Join(char separator, IEnumerable<string> values)`
* `String.LastIndexOf(string value)` and `String.LastIndexOf(char value)`
* `String.LastIndexOf(string value, int startIndex)` and `String.LastIndexOf(char value, int startIndex)`
* `String.Replace(char oChar, char nChar)` and `String.Replace(string oStr, string nStr)`
* `String.Split(char separator, StringSplitOptions = StringSplitOptions.None)` and `String.Split(string separator, StringSplitOptions = StringSplitOptions.None)`
* `String.StartsWith(string value)` and `String.StartsWith(char value)`
* `String.ToLower()`
* `String.ToUpper()`

#### Math methods
* Basic: `Math.Abs`, `Math.Exp`, `Math.Max`, `Math.Min`, `Math.Pow` `Math.Sign`, `Math.Sqrt`
* Rounding: `Math.Ceiling`, `Math.Floor`, `Math.Round`, `Math.Trunc`
* Logarithmic: `Math.Log`, `Math.Log10`
* Trigonometric: `Math.Acos`, `Math.Asin`, `Math.Atan`, `Math.Atan2`, `Math.Cos`, `Math.Cosh`, `Math.Sin`, `Math.Sinh`, `Math.Tan`, `Math.Tanh`

## Null handling

You don't have to worry about `null` values in binding expressions. If some part of the expression evaluates to `null`, the whole expression will return `null`. 

Internally, DotVVM treats every `.` as `.?` in C# 6.

> In previous versions of DotVVM, when you tried to call a method in a value binding and any of its arguments evaluated to `null`, the method wasn't invoked and the result of the expression was `null`. From DotVVM 3.0, this behavior was changed - the method will be invoked and `null` value will be passed as an argument.

## Double and single quotes

Because the bindings in HTML attributes are often wrapped in double quotes, DotVVM allows to use single quotes (apostrophes) for strings as well. 

This is different from the C# syntax where double quotes are used for `string` values while single quotes are used for `char` values.
In DotVVM, the single and double quotes can be used interchangeably.

```DOTHTML
<a class="{value: Active ? 'active' : 'not-active' }"></a>
```

## Enums

If you have a property of an enum type in your viewmodel, you may need to work with that value in the binding. 

```CSHARP
public class MyViewModel {
    ...
    public MyApp.Enums.ButtonColor Color { get; set; }    // ButtonColor is enum
    ...
}
```

You can use the `@import` directive to import the namespace in which the enum is declared. Then, you can use the `ButtonColor.Red` to reference the enum member.

```DOTHTML
@viewModel ...
@import MyApp.Enums

<div class-red="{value: Color == ButtonColor.Red}"></div>
```

On the client-side, the enum values are converted to strings on the client side, so you can compare the value with strings. The following expression will also work - this is different from C# where enums cannot be compared with `string` values directly.

```DOTHTML
<a class="{value: Color == 'Red' ? 'button-red' : 'button-normal'}">button</a>
```

## See also

* [Data-binding overview](~/pages/concepts/data-binding/overview)
* [Resource binding](~/pages/concepts/data-binding/resource-binding)
* [Binding context](~/pages/concepts/data-binding/binding-context)
* [Respond to user actions](~/pages/concepts/respond-to-user-actions/overview)