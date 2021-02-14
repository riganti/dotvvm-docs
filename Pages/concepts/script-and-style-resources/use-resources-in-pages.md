# Using Resources

You can use the `RequiredResource` control to indicate that you need to include the specific resource in the page.

```DOTHTML
<dot:RequiredResource Name="bootstrap" />
```

By default, the `StyleResource`s are placed in the `head` section, the `ScriptResource`s are placed at the end of the `body` element, but you can change it by setting `RenderPosition` property on the resource instance.

Also, each control in the page can request any resources so it can work properly. 

For example, if you add the [FileUpload](/docs/controls/builtin/FileUpload/{branch}) control in the page, the control will call `context.ResourceManager.AddRequiredResource()` method to indicate that it needs the `dotvvm.fileUpload-css` resource.

If you set the `FormatString` property on a `TextBox`, it will request the globalization resource for the culture of the HTTP request.

When a page is about to be rendered, the resource manager will put all required resources together, sort them to satisfy all dependency constraints, and render them in the page in correct order.

## Built-in Resources

DotVVM already includes the following built-in resources:

* `dotvvm` - a fundamental set of function required by DotVVM to work correctly.

* `dotvvm.debug` - a helper that displays exception details from the commands. It is only included in the page in [debug mode](/docs/tutorials/basics-configuration/{branch}).

* `dotvvm.fileUpload-css` - a CSS styles for the [FileUpload](/docs/controls/builtin/FileUpload/{branch}) control.

* `knockout` - Knockout JS 3.5.0 (with a few tweaks).

* `jquery` - jQuery 2.1.1. It is used only in `dotvvm.debug`, the core DotVVM doesn't depend on jQuery at all.

* `globalize` - a modified version of the globalize.js library.

To support client-side number and date formats, there are also automatically generated resources for every culture:

* `globalize:en-US` - a globalization resources for en-US culture. All cultures in .NET Framework are supported, 
however only a subset of the number and datetime formats are supported.
