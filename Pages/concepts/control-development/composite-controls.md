# Composite controls

Composite controls are a new way of declaring controls. The main use case for composite controls is to offer an easy method of combining existing controls into larger blocks, and being able to parameterize these blocks.

> Composite controls were introduced in DotVVM 4.0.

## Declare the composite control

The composite control must satisfy the following two requirements:

* Inherit from the `CompositeControl` class.
* Contain the static or instance method called `GetContents`. This method can have any parameters (they will be interpreted as control properties), and must return either `DotvvmControl` or `IEnumerable<DotvvmControl>`. 

The declaration can look like this:

```CSHARP
public class ImageButton : CompositeControl
{
    public static DotvvmControl GetContents(
        [DefaultValue("Button")] ValueOrBinding<string> text,
        ICommandBinding click,
        string imageUrl = "/icons/default-image.png"
    )
    {
        return new LinkButton()
            .SetProperty(b => b.Click, click)
            .AppendChildren(
                new HtmlGenericControl("img")
                    .SetAttribute("src", imageUrl),
                new Literal()
                    .SetProperty(text)
            );
    }
}
```

## Composite control properties

The code snippet above declared the `ImageButton` control with 3 properties:

* `Text` - the property is of type `ValueOrBinding<string>`. You can assign both [value binding](~/pages/concepts/data-binding/value-binding) or a hard-coded value to the control. Also, the property has a default value - if it is not specified, the text "Button" will be used.

* `Click` - the property specifies `ICommandBinding` as a type, so only the [command binding](~/pages/concepts/respond-to-user-actions/commands) can be assigned to the property. It doesn't have a default value, therefore it is required. 

* `ImageUrl` - the property is declared as `string`, so it will suppport only hard-coded values in markup (or a resource binding). It has a default value.

You can also use the following types of parameters:

* `ITemplate` for passing templates - you can then use the [TemplateHost](~/controls/builtin/TemplateHost) to instantiate the template in the generated controls.

* `DotvvmControl` or `IEnumerable<DotvvmControl>` for passing child elements - you can use for example the `AppendChildren` method to place the controls as children in the generated controls.

* `IValueBinding<T>` for properties that allow only value binding expressions.

You can use nullable types - in general, they tell DotVVM that the property is optional. You can specify the default value either as a default value of the parameter, or via the `[DefaultValue]` attribute. If the property is not nullable and doesn't provide a default value, it will be treated as a required property.

You can use the [MarkupOptions](control-properties#specify-markup-otpions) attributes or the DataContextChange attributes on the properties, same as on the properties in markup or code-only controls. 

See the [Control properties](control-properties) for more information about declaring control properties.

## Fluent API for building control hierarchies

In order to simplify building control hierarchy, DotVVM adds several extension methods. The most important are:

* `SetProperty` - can set a property to the control. It supports specifying the property via a lambda expression or by passing the `DotvvmProperty` object. It also supports the `ValueOrBinding<T>` types, so it will internally call either `SetValue` or `SetBinding`.

* `SetAttribute` - sets a HTML attribute. It is commonly used for `HtmlGenericControl`.

* `SetCapability` - sets a [control capability](control-capabilities) property. 

* `AddCssClass` - adds a CSS class. It can be called multiple times.

* `AppendChildren` - appends children to the control's children collection.

### Using Markup controls in code-only controls

If you plan to instantiate a markup control as a child inside a composite control, you should use the `MarkupControlContainer` class.

See the [Markup controls](markup-controls#using-markup-controls-in-code-only-controls) page for more info. 

## See also

* [Markup controls](markup-controls)
* [Code-only controls](code-only-controls)
* [Control properties](control-properties)
* [Control capabilities](control-capabilities)