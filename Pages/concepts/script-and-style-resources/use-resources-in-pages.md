# Use resources in pages

To include the specific resource in the page, you can use the `RequiredResource` control. 

```DOTHTML
<dot:RequiredResource Name="bootstrap" />
```

The control doesn't render anything, and can be used on any place in the page. 

By default, DotVVM places stylesheet resource in the `head` section, and the script resource at the end of the `body` element. 

The exact order of the resources is guided by the `Dependencies` and `RenderPosition` properties of each resource specified during the [resource registration](overview).

## Request a resource from a control

Any control can request a resource to be included in the page. For example, if you add the [FileUpload](~/controls/builtin/FileUpload) control in the page, the control will call `context.ResourceManager.AddRequiredResource()` method to indicate that it needs the `dotvvm.fileUpload-css` resource.

If you set the `FormatString` property on a `TextBox`, it will request the globalization resource for the culture of the HTTP request.

When a page is about to be rendered, the resource manager will put all required resources together, sort them to satisfy all dependency constraints, and render them in the page in correct order.

If you are [building custom controls](~/pages/concepts/control-development/introduction), you can use `context.ResourceManager.AddRequiredResource` to request any resource.

## See also

* [Resources overview](overview)
* [Bundling & minification](bundling-minification)