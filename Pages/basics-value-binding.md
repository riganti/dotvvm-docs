# Value Binding

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

* `SomeProperty`
* `SomeProperty.OtherProperty`
* `SomeCollection[6]`
* `SomeCollection[6].OtherProperty`
* `SomeProperty >= 0`
* `SomeProperty + 1`
* `SomeProperty ? "some string" : "other string"`
* `SomeProperty != OtherProperty`

If you use the `ICollection.Count` or `Array.Length` property, they will be translated to JavaScript to use the `length` property on the JavaScript array.

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
