# Value binding

The **value binding** is the most frequently used binding in DotVVM.

It allows you to bind a property in the viewmodel to a property of a control in the DOTHTML file, or just render the value as a text.
 
Let's have the following viewmodel:

```CSHARP
public class MyViewModel {
    ...
    public string Url { get; set; }
    ...
}
```

In the DOTHTML markup, we can bind the property to the hyperlink's `href` attribute:

```DOTHTML
<a href="{value: Url}">Go To URL</a>
```

If you run the page and view the page source code, you'll see that DotVVM translated the binding into a Knockout JS expression. DotVVM uses this 
popular JavaScript framework to perform the data binding.
 
This is the HTML that will be rendered and sent to the browser:

```DOTHTML
<a data-bind="attr: { 'href': Url }">Go To URL</a>
```

The word `value` represents the type of the data binding. 

The `Url` is an expression that will be evaluated in the client's browser. The expression can use the public properties from the viewmodel, 
access elements of collections and use supported operators. 

You cannot, for example, call methods from the value bindings.

## Expressions Supported in Value Bindings

* Member access
   * `SomeProperty`
   * `SomeProperty.OtherProperty`
* Collections and array elements access
   * `SomeCollection[6]`
   * `SomeCollection[6].OtherProperty`
* Binary operation
   * `SomeProperty >= 0`
   * `SomeProperty + 1`
   * `SomeProperty != OtherProperty`
* Ternary conditional operator
   * `SomeProperty ? "some string" : "other string"`
* Method invocation
   * `SomeMethod(argument)`
* Block expression
   * `(expression1; expression2; expression3)`
   * *Note*: This is a composition of supported expressions within one data-binding. DotVVM uses parentheses `( ... )` to enclose expressions as compared to C#, which uses curly braces `{ ... }`. Result type of any composite expression is determined by the last child expression.
* Lambda function (**new in version 3.0**)
   * `(int intArg, string strArg) => SomeMethod(intArg, strArg)`
   * *Note*: Type-inference for lambda parameters is not available in version 3.0, therefore type information needs to be explicitly supplied together with lambda parameters definition. Type-inference is an upcomming feature in DotVVM 3.1.
* Local variable (**new in version 3.0**)
   * `(var myVariable = SomeFunction(arg1, arg2); SomeMethod(myVariable))`
   * *Note*: Variables are by design single-assignable (immutable). Variables may shadow property names and previous variables.

## .NET Methods Supported in Value Bindings

DotVVM can translate several .NET methods on basic types or collections to JavaScript, so you can safely use them in value bindings.  

### String Methods and ToString Overrides
* `String.Format(format, arg1 [, arg2, [ arg3]])`
* `String.Format(format, argumentArray)`
* `Object.ToString()`
* `Convert.ToString()`
* `DateTime.ToString()` and `DateTime.ToString(format)`
* <code><em>numericType</em>.ToString()</code> and <code><em>numericType</em>.ToString(format)</code>

### Nullable Methods
* `Nullable<T>.HasValue`
* `Nullable<T>.Value`

### Enumerable Methods
* `Enumerable.Select<T,U>(IEnumerable<T> collection, Func<T,U> selector)` (**new in version 3.0**)
* `Enumerable.Where<T>(IEnumerable<T> collection, Func<T,bool> predicate)` (**new in version 3.0**)

### REST API Bindings Methods
* `Api.RefreshOnChange`
* `Api.RefreshOnEvent`
* `Api.PushEvent` 
* *Note*: for more information about REST API bindings visit this [link](/docs/tutorials/basics-rest-api-bindings/{branch}).

### Other Methods
* `ICollection.Count` and `Array.Length`
* `String.Length`
* `Enums.GetNames<TEnum>()`
* `Task<T>.Result`

> It is possible to register custom translators for any .NET API. See [Providing Custom JavaScript Translators](/docs/tutorials/control-development-providing-custom-javascript-translators/{branch}) for more information.  

## Upcomming Support for .NET Methods in DotVVM 3.1

### Enumerable Methods
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

### String Methods
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

### Math Methods
* Basic: `Math.Abs`, `Math.Exp`, `Math.Max`, `Math.Min`, `Math.Pow` `Math.Sign`, `Math.Sqrt`
* Rounding: `Math.Ceiling`, `Math.Floor`, `Math.Round`, `Math.Trunc`
* Logarithmic: `Math.Log`, `Math.Log10`
* Trigonometric: `Math.Acos`, `Math.Asin`, `Math.Atan`, `Math.Atan2`, `Math.Cos`, `Math.Cosh`, `Math.Sin`, `Math.Sinh`, `Math.Tan`, `Math.Tanh`

## Null Handling in Value Bindings

You don't have to be afraid of `null` values. If some part of the expression evaluates to null, the whole expression will return null. 

Internally, DotVVM treats every `.` as `.?` in C# 6.

## Double and Single Quotes

Because the bindings in HTML attributes are often wrapped in double quotes, DotVVM allows to use single quotes (apostrophes) for strings as well.

```DOTHTML
<a class="{value: Active ? 'active' : 'not-active' }"></a>
```

## Enums

If you have a property of an enum type in your viewmodel, you may need to work with that value in the binding. 

In DotVVM, the enum values are converted to strings on the client side, so you can compare the value with strings.

```CSHARP
public class MyViewModel {
    ...
    public ButtonColor Color { get; set; }    // ButtonColor is enum
    ...
}
```

Use strings for enum value literals:

```DOTHTML
<a class="{value: Color == 'Red' ? 'button-red' : 'button-normal'}">button</a>
```

In **DotVVM 1.1** and newer, you can also use the `ButtonColor.Red` syntax, provided that you import the namespace using the `@import` directive.

```DOTHTML
@import DotvvmDemo.DAL.Enums

<a class="{value: Color == ButtonColor.Red ? 'button-red' : 'button-normal'}">button</a>
```
