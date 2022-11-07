# Supported expressions

DotVVM translates [value](value-binding) binding expressions into JavaScript so they can be evaluated in the browsed. Therefore, not all expressions can be used in the bindings.

The [static command](~/pages/concepts/respond-to-user-actions/static-commands) expressions are also translated to JavaScript. If there is any method that cannot be translated to JavaScript, the static command will make a request to the server to invoke the method and obtain its return value. The method must be marked with the `[AllowStaticCommand]` attribute.

The [resource](resource-binding) and [command](~/pages/concepts/respond-to-user-actions/commands) bindings are not translated into JavaScript, but they are limited to the syntax constructs that can be used in binding expressions. 

## Syntax constructs

* Member access
   * `SomeProperty`
   * `SomeProperty.OtherProperty`
* Access the parent [binding contexts](binding-context)
  * `_root.SomeProperty`
  * `_parent.SomeProperty`
* Collections and array elements access
   * `SomeCollection[6]`
   * `SomeCollection[6].OtherProperty`
* Access the collection metadata (if the current [binding context](binding-context) is a collection)
   * `_collection.Index`
   * `_collection.IsOdd`, `_collection.IsEven`...
* Binary operations
   * `SomeProperty >= 0`
   * `SomeProperty + 1`
   * `SomeProperty != OtherProperty`
* Ternary conditional operator
   * `SomeProperty ? "some string" : "other string"`
* Method invocation (only supported methods)
   * Regular methods: `SomeMethod(argument)`
   * Extension methods: `argument.SomeExtensionMethod()` (**new in version 3.0**)
* Block expression
   * `(expression1; expression2; expression3)`
   * *Note*: This is a composition of supported expressions within one data-binding. DotVVM uses parentheses `( ... )` to enclose expressions as compared to C#, which uses curly braces `{ ... }`. Result type of any composite expression is determined by the last child expression.
* Lambda functions (**new in version 3.0**)
   * `(int intArg, string strArg) => SomeMethod(intArg, strArg)`
   * *Note*: Type inference for lambda parameters is not available in version 3.0, therefore type information needs to be explicitly supplied together with lambda parameters definition. Type inference is an upcoming feature in DotVVM 3.1.
* Local variables (**new in version 3.0**)
   * `var myVariable = SomeFunction(arg1, arg2); SomeMethod(myVariable)`
   * *Note*: Variables are by design single-assignable (immutable). Variables may shadow property names and previously defined variables.
* String interpolation (**new in version 3.1**)
   * `$"Hello {NameProperty}!"`
   * `$"Date: {DateProperty:dd/MM/yyyy}"`
   * *Note*: Interpolation expressions and formatting component are supported. Regarding the formatting component, see [Formatting dates and numbers](~/pages/concepts/localization-and-cultures/formatting-dates-and-numbers) for more info about supported formats.

### .NET methods supported in value bindings

DotVVM can translate several .NET methods on basic types or collections to JavaScript, so you can safely use them in value bindings.

#### String methods
* `String.Contains(string value)` and `String.Contains(char value)`
* `String.EndsWith(string value)` and `String.EndsWith(char value)`
* `String.IndexOf(string value)` and `String.IndexOf(char value)`
* `String.IndexOf(string value, int startIndex)` and `String.IndexOf(char value, int startIndex)`
* `String.IsNullOrEmpty(string value)`
* `String.IsNullOrWhiteSpace(string value)`
* `String.Join(string separator, IEnumerable<string> values)` and `String.Join(char separator, IEnumerable<string> values)`
* `String.LastIndexOf(string value)` and `String.LastIndexOf(char value)`
* `String.LastIndexOf(string value, int startIndex)` and `String.LastIndexOf(char value, int startIndex)`
* `String.Length`
* `String.PadLeft(int length)`
* `String.PadLeft(int length, char c)`
* `String.PadRight(int length)`
* `String.PadRight(int length, char c)`
* `String.Replace(char oChar, char nChar)` and `String.Replace(string oStr, string nStr)`
* `String.Split(params char[] separators)`
* `String.Split(char separator, StringSplitOptions = StringSplitOptions.None)` and `String.Split(string separator, StringSplitOptions = StringSplitOptions.None)`
* `String.StartsWith(string value)` and `String.StartsWith(char value)`
* `String.ToLower()` and `String.ToLowerInvariant()`
* `String.ToUpper()` and `String.ToUpperInvariant()`
* `String.Trim()`
* `String.Trim(char c)`
* `String.TrimStart()`
* `String.TrimStart(char c)`
* `String.TrimEnd()`
* `String.TrimEnd(char c)`	

> DotVVM supports only `InvariantCulture` and `InvariantCultureIgnoreCase` values from the `StringComparison` enum. The default string comparing strategy is `InvariantCulture`. This is different as compared to behavior in .NET, where the default behavior is `CurrentCulture`.

> DotVVM supports `None` and `RemoveEmptyEntries` options from the `StringSplitOptions` enum.

> Missing overloads in some frameworks (e.g. .NET Framework) are exposed as extension methods. Therefore, all methods listed on this page can be used by all supported frameworks.

#### Collection methods
* `ICollection.Count` and `Array.Length`

#### List methods
* `List<T>.Add(T element)`
* `List<T>.AddRange(IEnumerable<T> elements)`
* `List<T>.Clear()`
* `List<T>.Contains(T element)` (**new in version 4.1**)
   * *Note*: this method is restricted to primitive types
* `List<T>.Insert(int index, T element)`
* `List<T>.InsertRange(int index, IEnumerable<T> elements)`
* `List<T>.RemoveAt(int index)`
* `List<T>.RemoveAll(Predicate<T> predicate)`
* `List<T>.RemoveRange(int index, int count)`
* `List<T>.Reverse()`
* `ListExtensions.AddOrUpdate<T>(this List<T> list, T element, Func<T,bool> matcher, Func<T,T> updater)`
   * *Note*: this method tries to update an element using `updater`. Element for updating can be selected using `matcher`. If no element matched the predicate, `element` is added to list.
* `ListExtensions.RemoveFirst<T>(this List<T> list, Func<T,bool> predicate)`
* `ListExtensions.RemoveLast<T>(this List<T> list, Func<T,bool> predicate)`

#### Dictionary methods
* `Dictionary<K, V>.Clear()`
* `Dictionary<K, V>.ContainsKey(K key)`
* `Dictionary<K, V>.Remove(K key)`

#### Enum methods
* `Enums.GetNames<TEnum>()`

#### Task methods
* `Task<T>.Result`

#### Formatting
* `String.Format(format, arg1 [, arg2, [ arg3]])`
* `String.Format(format, argumentArray)`
* `Object.ToString(object value)`
* `Convert.ToString(object value)`
* `Convert.ToBoolean(object value)`
* `Convert.ToByte(object value)`
* `Convert.ToSByte(object value)`
* `Convert.ToInt16(object value)`
* `Convert.ToUInt16(object value)`
* `Convert.ToInt32(object value)`
* `Convert.ToUInt32(object value)`
* `Convert.ToInt64(object value)`
* `Convert.ToUInt64(object value)`
* `Convert.ToSingle(object value)`
* `Convert.ToDouble(object value)`
* `Convert.ToDecimal(object value)`
* `DateTime.ToString()`
* `DateTime.ToString(format)`
* <code><em>numericType</em>.ToString()</code> and <code><em>numericType</em>.ToString(format)</code>

#### Nullable types
* `Nullable<T>.HasValue`
* `Nullable<T>.Value`

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
   * *Note*: internally, the sorting algorithm is stable. Therefore, if you need to sort, for example using multiple keys, you write something like: `Collection.OrderBy(e => e.SecondaryKey).OrderBy(e => e.PrimaryKey)`
* `Enumerable.OrderByDescending<T,U>(IEnumerable<T> collection, Func<T,U> selector)`
   * *Note*: this method is restricted to primitive types
   * *Note*: internally, the sorting algorithm is stable. Therefore, if you need to sort, for example using multiple keys, you write something like: `Collection.OrderByDescending(e => e.SecondaryKey).OrderByDescending(e => e.PrimaryKey)`
* `Enumerable.Select<T, U>(IEnumerable<T> collection, Func<T, U> selector)`
* `Enumerable.Skip<T>(IEnumerable<T> collection, int count)`
* `Enumerable.Take<T>(IEnumerable<T> collection, int count)`
* `Enumerable.ToArray<T>(IEnumerable<T> collection)`
* `Enumerable.ToList<T>(IEnumerable<T> collection)`
* `Enumerable.Where<T>(IEnumerable<T> collection, Func<T, bool> predicate)`

### DateTime property getters
* `DateTime.Year`
* `DateTime.Month`
* `DateTime.Day`
* `DateTime.Hour`
* `DateTime.Minute`
* `DateTime.Second`
* `DateTime.Millisecond`

#### Math methods
* Basic: `Math.Abs`, `Math.Exp`, `Math.Max`, `Math.Min`, `Math.Pow` `Math.Sign`, `Math.Sqrt`
* Rounding: `Math.Ceiling`, `Math.Floor`, `Math.Round`, `Math.Trunc`
* Logarithmic: `Math.Log`, `Math.Log10`
* Trigonometric: `Math.Acos`, `Math.Asin`, `Math.Atan`, `Math.Atan2`, `Math.Cos`, `Math.Cosh`, `Math.Sin`, `Math.Sinh`, `Math.Tan`, `Math.Tanh`

#### REST API binding methods
* `Api.RefreshOnChange`
* `Api.RefreshOnEvent`
* `Api.PushEvent` 

> See [REST API bindings](~/pages/concepts/respond-to-user-actions/rest-api-bindings/overview) for more info

### Provide custom method translators

It is possible to register custom translators for any method. See [Provide custom JavaScript translators](~/pages/concepts/control-development/custom-javascript-translators) for more information.

### Use custom .NET extension methods

Whenever you need to use custom .NET extension methods, you need to provide information about where should DotVVM search for these methods. This can be achieved using the `@import` directive, using which it is possible provide namespaces that should be searched for extension methods. Furthermore, you can also provide method translators for custom extension methods. That way it is possible to use custom extension methods safely inside value bindings.

Since we are adding a lot of methods from `System.Linq` namespace, we decided to include this namespace to the default DotVVM namespaces for extension methods lookup. Therefore, it is not necessary to write `@import System.Linq` in your `DotHTML` files.

### WebUtility methods
* `WebUtility.UrlEncode(string value)`
* `WebUtility.UrlDecode(string value)`

> In order to use methods above, you need to specify `@import System.Net`.

## See also

* [Data-binding overview](~/pages/concepts/data-binding/overview)
* [Respond to user actions](~/pages/concepts/respond-to-user-actions/overview)
* [Binding context](~/pages/concepts/data-binding/binding-context)
* [REST API bindings](~/pages/concepts/respond-to-user-actions/rest-api/bindings/overview)
* [Formatting dates and numbers](~/pages/concepts/localization-and-cultures/formatting-dates-and-numbers)
* [Custom JavaScript translators](~/pages/concepts/control-development/custom-javascript-translators)
