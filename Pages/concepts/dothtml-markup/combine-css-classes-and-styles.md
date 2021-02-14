# Class and Style Attribute

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

DotVVM includes a built-in property group with prefix `Class-`. It can be used to combine multiple CSS classes.

Instead of the long and nasty expression, you can do the same thing like this:

```DOTHTML
<div Class-bold="{value: IsBold}"
     Class-italic="{value: IsItalic}"
     Class-underline="{value: IsUnderline}">
</div>
```

The `div` element will get the CSS classes of all properties that evaluate to `true`. All these classes will be joined and appended to the `class` attribute. If the value of any expression changes, the CSS class will be recalculated.

You can even combine the `Class-something` properties with the `class` attribute itself (e.g. when you have some classes you need to include in all cases).

```DOTHTML
<div Class-bold="{value: IsBold}"
     Class-italic="{value: IsItalic}"
     Class-underline="{value: IsUnderline}"
     class="mydiv">
     <!-- the "mydiv" class will be included every time -->
</div>
```

> This feature is available in DotVVM 1.1 and newer.

## Style Property Group

DotVVM includes a built-in property group with prefix `Style-`. It can be used to combine multiple CSS properties.

The number of use-cases for the `Style-` is rather limited when compared to `Class-`, since styling should be done using primarily CSS classes instead.

However, when the `Style` property is being set dynamically using JavaScript usage of the `Style` property group ensures that current `style` attributes are preserved. This is not the case when using a regular value binding, which replaces the entire `style` attribute with an evaluated value.

```DOTHTML
<div Style-background-color="{value: Color}"
     Style-width="{value: WidthInPx}"
     Style="margin-top: 20px">
</div>
```

The `div` element will get all CSS attributes combined in single `style` attribute.

> This feature is available in DotVVM 2.0 and newer.
