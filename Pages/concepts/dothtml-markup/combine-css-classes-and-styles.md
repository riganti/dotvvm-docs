# Combine CSS classes and styles

The `class` attribute in HTML can reference multiple CSS classes separated by a space (e.g. `<a class="btn btn-default">`).

If the CSS classes on some element depend on the viewmodel properties, you can use [value binding](~/pages/concepts/data-binding/value-binding) expressions to calculate the `class` attribute value.

However, when you need to combine multiple CSS classes, the expression gets quite complicated. Imagine you have three properties in the viewmodel - `IsBold`, `IsItalic` and `IsUnderline`, and you need to apply the `bold`, `italic` and `underline` CSS classes to some element when these properties are `true`.

The expression would look like this:

```DOTHTML
<div class="{value: (IsBold ? 'bold ' : ' ') + (IsItalic ? 'italic ' : ' ') + (IsUnderline ? 'underline ' : ' ')}">
</div>
```

## Class-* property group

In DotVVM, there is a concept called _property group_. Basically, it is a group of properties with a common prefix (e.g. `MyGroup-`).

For example, you can see this feature in the [RouteLink](/docs/controls/builtin/RouteLink/{branch}) where the `Param-` property group is used to define values of route parameters.

DotVVM includes a built-in property group with prefix `Class-` which can be used to combine multiple CSS classes.

Instead of the long expression, you can combine classes like this:

```DOTHTML
<div Class-bold="{value: IsBold}"
     Class-italic="{value: IsItalic}"
     Class-underline="{value: IsUnderline}">
</div>
```

The `div` element will get a combination of the classes for all properties which evaluate to `true`. All these classes will be joined and appended to the `class` attribute. If any of the expression changes, the CSS class will be recalculated.

You can even combine the `Class-something` properties with the `class` attribute itself (e.g. when you have some classes you need to include in all cases).

```DOTHTML
<div Class-bold="{value: IsBold}"
     Class-italic="{value: IsItalic}"
     Class-underline="{value: IsUnderline}"
     class="mydiv">
     <!-- the "mydiv" class will be included every time -->
</div>
```

## Style-* property group

DotVVM includes a built-in property group with prefix `Style-`. It can be used to combine multiple inline styles.

The number of use-cases for the `Style-` is rather limited when compared to `Class-`, since styling using CSS classes is preferred over styling using inline styles.

However, when the `Style` property is being set dynamically using JavaScript usage of the `Style` property group ensures that current `style` attributes are preserved. This is not the case when using a regular value binding, which replaces the entire `style` attribute with an evaluated value.

```DOTHTML
<div Style-background-color="{value: BackgroundColor}"
     Style-width="{value: Width + "px"}"
     Style="margin-top: 20px">
</div>
```

The `div` element will get all CSS attributes combined in single `style` attribute.

## See also

* [Data-binding](~/pages/concepts/data-binding/overview)
* [Respond to user actions](~/pages/concepts/respond-to-user-actions/overview)
* [Built-in controls](~/pages/concepts/dothtml-markup/builtin-controls)