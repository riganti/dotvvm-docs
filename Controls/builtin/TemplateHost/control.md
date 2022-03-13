This control allows to host a template (an `ITemplate` object) and build its contents using this template.

> The control is not meant to be used in DotHTML markup - it helps the control developers to place template inside another controls at a later time.

```CSHARP
public class MyMenu : CompositeControl
{
    public static DotvvmControl GetContents(
        // please note that appropriate DataContextChange attributes need to be applied here as the template is used inside Repeater
        ITemplate itemTemplate,
        ...
    )
    {
        return new Repeater()
            ...
            .SetProperty(r => r.ItemTemplate, new DelegateTemplate(serviceProvider => 
                new HtmlGenericControl("li")
                    .AppendChildren(new TemplateHost(itemTemplate))
            ));
    }
}
```

## See also

* [Composite controls](~/pages/concepts/control-development/composite-controls)