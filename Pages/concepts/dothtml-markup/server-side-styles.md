# Server-side styles

Server-side styles lets you set properties or attributes of particular controls across the entire application.

The server-side styles are defined as part of [configuration](~/pages/concepts/configuration/overview) in the `DotvvmStartup.cs` file. 

## Register style for a control or element

You can manage the styles using the `StyleRepository` object, which can be accessed from `DotvvmConfiguration`. 

To register the style, you need to specify the type of the control, or use a parameter with tag name of HTML element:

```CSHARP
// Modify all controls derived from ButtonBase
config.Styles.Register<ButtonBase>()...

// Modify all HTML elements with tag name input
config.Styles.Register("input")...
```

> If you register the style by specifying the tag name, the behavior of DotVVM controls which use such elements internally will not be modified.

### Conditional rules

It is also possible to set a condition under which the styles will be applied. To do that, use `Register(Func<StyleMatchContext, bool>)`. 

```CSHARP
// This will hide every control derived from ButtonBase that does not have the Click property
config.Styles.Register<ButtonBase>(b => !b.HasProperty(ButtonBase.ClickProperty), allowDerived: true)
  .SetAttribute("style", "display:none;", StyleOverrideOptions.Overwrite);
```    

### Matching by tags

For easy matching of the controls, we've introduced a new property `Styles.Tag`. It can specify a value you can use when matching the control with styles:

```DOTHTML
<!-- separate tags by comma -->
<dot:Button Styles.Tag="delete-icon,other-tag" Text="Delete" />
```

```CSHARP
c.Styles.RegisterAnyControl(c => c.HasTag("delete-icon"))
  .Prepend(new HtmlGenericControl("img").SetAttribute("src", "~/icons/delete.png"));
```

You can also match by tags on ancestor controls in the hierarchy:

```CSHARP
c.Styles.Register<Literal>(c => c.HasAncestorWithTag("literal-spans"))
        .SetProperty(c => c.RenderSpanElement, true);
```

### Matching by view location or data context type

The styles are applied during the compilation of a view. The `StyleMatchContext` has methods for checking data contexts, ancestors, and other properties of the object. You can even check whether the view the object is included in is in a specific directory:

```CSHARP
config.Styles.Register<GridView>(c => 
        c.HasAncestor<Repeater>() &&
        c.HasDataContext<AccountInfo>() && 
        c.HasViewInDirectory("~/Views/Logs/")
    )
    .SetDotvvmProperty(GridView.VisibleProperty, StyleOverrideOptions.Ignore);
```

## Define the style properties

The `Register` method returns an instance of `StyleBuilder` that lets you modify the control. You can set default values of attributes and properties, or override them completely. These method calls can also be chained.

```CSHARP
// This will change the class on all buttons that do not have any
config.Styles.Register<Button>()
  .SetAttribute("class", "single-class");

//this will overwrite class of all textboxes
config.Styles.Register<TextBox>()
  .SetAttribute("class", "overwritten", StyleOverrideOptions.Overwrite);

//this will append class of all literals
config.Styles.Register<Literal>()
  .SetAttribute("class", "appended", StyleOverrideOptions.Append);

//this will overwrite the text of all buttons
config.Styles.Register<Button>()
  .SetDotvvmProperty(Button.TextProperty, "Button");
```

> The `SetAttribute` method has the default value of `StyleOverrideOptions.Ignore`, while the `SetDotvvmProperty` method has `StyleOverrideOptions.Overwrite`. The `StyleOverrideOptions.Append` is allowed only for attributes which supports multiple values, such as `class` or `style`, or for properties of a collection type (for example, the `PostBack.Handlers` attached property).

## Work with data-binding expressions

Prior to DotVVM 4.0, it was only possible to set constant values into control properties. From DotVVM 4.0, it's possible to compute the value for each control using a supplied lambda function:

```csharp
// this will apply a confirm postback handler to all commands and staticCommands on this control
c.Styles.RegisterAnyControl(c => c.HasTag("confirm"))
    .AppendDotvvmProperty(
        PostBack.HandlersProperty,
        c => new ConfirmPostBackHandler(c.GetHtmlAttribute(
            "data-confirm-msg") ?? "This is a default confirm message")
        );
```

It is also possible to process a binding in the API. For example, this transforms every usage of the `Visible` property into a CSS class regardless whether it's a static value or a value / resource binding.

```csharp
c.Styles.Register<HtmlGenericControl>(c => c.HasProperty(c => c.Visible))
        .SetPropertyGroupMember("Class-", "hide", c =>
            c.Property(c => c.Visible).Negate())
        .SetProperty(c => c.Visible, true); // reset the Visible property
```

The `.Negate()` is a extension method defined on the new `ValueOrBinding<bool>` type. There are many others including `.Select(t => ...)` where you can supply any lambda function which can be translated to a JavaScript binding.

## Modify content of styled controls

Starting with DotVVM 4.0, it is possible to create controls in normal C# code and then put them into children, control properties, or append and prepend them to the matched control. 

Use `.PrependContent`, `.AppendContent`, `.SetControlProperty`, `.Append` and `.Prepend`. You can also replace the entire control with a different one using `.ReplaceWith` which will also copy all properties onto the new control. 

Last but not least, you can use `.WrapWith` to place a wrapper control around it.

## See also

* [Common control properties](~/pages/concepts/dothtml-markup/common-control-properties)
* [Data-binding](~/pages/concepts/data-binding/overview)
* [Respond to user actions](~/pages/concepts/respond-to-user-actions/overview)
