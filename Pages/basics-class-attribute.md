# Class Attribute

The `class` attribute in HTML can reference multiple CSS classes separated by a space (e.g. `<a class="btn btn-default">`).

If the CSS classes on some element depend on the viewmodel properties, you can use [value binding](/docs/tutorials/basics-value-binding/{branch}) expressions to calculate the `class` attribute value.

However, when you need to combine multiple CSS classes, the expression gets quite complicated. Imagine you have three properties in the viewmodel - `IsBold`, `IsItalic` and `IsUnderline`, and you need to apply the `bold`, `italic` and `underline` CSS classes to some element when these properties are `true`.

The expression would look like this:

```DOTHTML
<div class="{value: (IsBold ? 'bold ' : ' ') + (IsItalic ? 'italic ' : ' ') + (IsUnderline ? 'underline ' : ' ')}">
</div>
```

## Class Property Group

In DotVVM, you may see a mechanism called _property group_. Basically, it is a group of properties with a common prefix (e.g. `MyGroup-`).

For example, you can see this feature in the [RouteLink](/docs/controls/builtin/RouteLink/{branch}) where the `Param-` property group is used to define values of route parameters.

DotVVM includes a built-in property group with prefix `class-`. It can be used to combine multiple CSS classes.

Instead of the long and nasty expression, you can do the same thing like this:

```DOTHTML
<div class-bold="{value: IsBold}"
     class-italic="{value: IsItalic}"
     class-underline="{value: IsUnderline}">
</div>
```

The `div` element will get the CSS classes of all properties that evaluate to `true`. All these classes will be joined and appended to the `class` attribute. If the value of any expression changes, the CSS class will be recalculated.

You can even combine the `class-something` properties with the `class` attribute itself (e.g. when you have some classes you need to include in all cases).

```DOTHTML
<div class-bold="{value: IsBold}"
     class-italic="{value: IsItalic}"
     class-underline="{value: IsUnderline}"
     class="mydiv">
     <!-- the "mydiv" class will be included every time -->
</div>
```

> This feature is available in DotVVM 1.1 and newer.