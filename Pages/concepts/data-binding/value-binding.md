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

See the [supported expressions](supported-expressions) page for more details.

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