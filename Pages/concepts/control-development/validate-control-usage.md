# Validate control usage

The controls in DotVVM can validate whether they are being used correctly. Simple checks can be done by using the `MarkupOptions` attribute, more complex validation logic is implemented in the `ValidateUsage` static method in the control code.

## MarkupOptions attribute

The `MarkupOptions` attribute can be applied on DotVVM control properties and has several parameters which control how the property can be used:

* `Required` indicates that the property must be set in the markup.
* `AllowBinding` indicates that the property can be bound-to on the client-side using [value binding](~/pages/concepts/data-binding/value-binding).
* `AllowHardCodedValue` indicates that the property can specify a value directly in the markup. This option also allows the [resource binding](~/pages/concepts/data-binding/resource-binding) which is not a client-side binding and is evaluated on the server.

## ValidateUsage method

When some combination of control properties is not supported by a control, you can implement the necessary checks in a specific way in order to detect all errors on Status Page (API) and in the Visual Studio extension.

In the example below, we check whether control has either the `Icon` or `Text` property set.

```C#
[ControlUsageValidator]
public static IEnumerable<ControlUsageError> ValidateUsage(ResolvedControl control)
{
    //Control usage checks
    if (!control.HasProperty(IconProperty) && !control.HasProperty(TextProperty))
    {
        yield return new ControlUsageError("Button requires Icon, Text or both properties set.", control.DothtmlNode);
    }
}
```

## See also

* [Control development overview](overview)
* [Markup controls](markup-controls)
* [Code-only controls](code-only-controls)
* [Adding interactivity using Knockout binding handlers](interactivity)