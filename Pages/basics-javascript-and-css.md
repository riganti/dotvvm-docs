## Javascript and CSS Resource Management

**DotVVM** has a built-in mechanism for managing reasources. It supports javascript files, inline javascript snippets and CSS files by default and is extensible.

The resources are named and stored in a global repository in [DotVVM configuration](/docs/tutorials/basics-configuration/{branch}). 

Each resource can also specify its dependencies. Thanks to this, DotVVM can include all required resources in the page in the correct order.


### Resource Repository

All resources are registered in resource respository found in the `DotvvmConfiguration.Resources` collection. 
You can register a new resource with the `Register` method, or you can replace existing resource with your own one. Typically, you want to
do this in the `DotvvmStartup.cs` file, but you can do it anytime as the DotvvmResourceRepository is thread-safe.

```CSHARP
config.Resources.Register("bootstrap-css", new StylesheetResource()
{
    Url = "~/Content/bootstrap.min.css"
});
config.Resources.Register("bootstrap", new ScriptResource()
{
    Url = "~/Scripts/bootstrap.min.js",
    Dependencies = new[] { "bootstrap-css", "jquery" }
});
```

In the code, you can retrieve the resource by its name using `FindResource` method.


### Built-in Resources

DotVVM already includes the following built-in resources. 

* **dotvvm** - a fundamental set of function required by DotVVM to work correctly.

* **dotvvm.debug** - a helper that displays exception details from the commands.

* **dotvvm.fileUpload-css** - a CSS styles for the [FileUpload](/docs/controls/builtin/FileUpload/{branch}) control.

* **knockout** - Knockout JS 3.5.0 (custom modified version)

* **jquery** - jQuery 2.1.1

* **globalize** - a modified version of the globalize.js library

To support client-side number and date formats, there are also automatically generated resources:

* **globalize:en-US** - a globalization resources for en-US culture. All cultures in .NET Framework are supported, 
however only a subset of the number and datetime formats are supported.


### Requesting Resources

In the page, you can use the RequiredResource control to manually link a resource.

```DOTHTML
<dot:RequiredResource Name="bootstrap" />
```

By default, the `StyleResource`s are placed in the `head` section, the `ScriptResource`s are placed at the end of the `body` element, but you can change it by setting `RenderPosition` property on the resource instance.

Also, each control in the page can request any resources, so it can work properly. For example, if you add the [FileUpload](/docs/controls/builtin/FileUpload/{branch}) control in the page,
the control will call `DotvvmRequestContext.AddRequiredResource` method to tell the DotVVM resource manager, that it needs some specific resource or when you apply set `FormatString` property on `TextBox` it will load globalization resource.

When the page is about to be rendered, DotVVM resource manager will get all required resources, sort them to satisfy the dependency constraints, and render them into the HTML.


### Resource Options

Typical resources have a `Location` property of type `IResourceLocation` which determines where the resource file can be located. You can use on of the following, or write your own implementation if you want:
* `RemoteResourceLocation` - specifies a url where the resource can be found. It can be any link - either to CDN, relative link to your server or even a data uri.
* `LocalFileResourceLocation` - resource file is located in a file on filesystem. Remember that you are specifying file path not url and `/` on the start means root of filesystem.
* `EmbeddedResourceLocation` - resource file is included in the assembly as embedded resource. This one is very useful for delivering a control library as a single dll and all dotvvm internal (dotvvm.js, knockout.js, ...) resource are of this type.

If you want to use CDN for script files, it is often a good idea to have a local fallback for the case that CDN is down or you are debugging the app without internet connection. There is an property called `LocationFallback`. If it is set, the framework will try to load the script from the primary location (the CDN) first and will use the `ResourceLocationFallback.JavasciptCondition` to check if resource has succesfully loaded. If not, it will fall back succesively to the `AlternativeLocations`.

You can of course implement custom resource types and resource location, check out source codes of the existing ones to see how to do that. There is also an `InlineScriptResource` which renders the inline javascript.


### Resource Processing

You can register `IResourceProcessor` in the resource repository which can perform additional action with the collection of resources before it's rendered. It can be used e.g. for bundling - if you find, that the client request 5 resources and there is a bundle which contain all of them, you can modify the collection and make the client to download the bundle. We have a BundlingResourceProcessor implementation:

```CSHARP
var bundling = new BundlingResourceProcessor();
bundles.RegisterBundle(dotvvmConfiguration.Resources.FindNamedResource("myBundle"), "script1", "script2");
dotvvmConfiguration.Resources.DefaultResourceProcessors.Add(bundling);
```
