# Javascript and CSS Resources

DotVVM has a built-in mechanism for managing resources. It supports JavaScript files, inline JavaScript snippets and CSS files. It is extensible so it can be used to work with fonts, icons and other kinds of static files.

The resources are named and stored in a global repository which is configured in [DotVVM configuration](/docs/tutorials/basics-configuration/{branch}). 

Each resource can also specify its dependencies. Thanks to this, DotVVM can include all required resources in the page in the correct order. 

And finally, if any DotVVM control needs a particular resource, it can request the resource to be included in the page. DotVVM keeps track of the resources needed by controls in the page and renders only the those which are really needed.

We have the following types of resources:

* `ScriptResource` renders the `<script>` element and is used to include JavaScript files.

* `StylesheetResource` renders the `<link rel="stylesheet">` element and is used to include CSS files.

* `InlineScriptResource` renders the `<script>` element with JavaScript code snippet.

* `NullResource` is a special type of resource that doesn't render anything. It is used when some control requests the resource to be included in the page, however you have included the resource itself (e.g. in the master page).

## Resource Repository

> The resource registration has changed in DotVVM 1.1. Visit the [Upgrading to DotVVM 1.1](/docs/tutorials/how-to-start-upgrade-from-1-0/1-1) for more details.

All resources are registered in resource respository found in the `config.Resources` collection.

You can register a new resource with the `Register` method. This method can also replace existing resources if they exists.
The resources should be registered in the `DotvvmStartup.cs` file.

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

In the code, you can retrieve the resource by its name using `FindResource` method. If you need to change the path for the `jquery` resource, you can do it like this:

```CSHARP
var jquery = config.Resources.FindResource("jquery") as ScriptResource;
jquery.Url = "~/Scripts/jquery.2.1.1.min.js";
```

## CDN Fallbacks

If you want to use CDN for script files, it is often a good idea to have a local fallback for the case that CDN is down, or if you are debugging the app without the Internet connection. 

There is a property called `CdnUrl`. If it is set, the framework will try to load the script from the primary location (the CDN) first and will use the `GlobalObjectName` to check whether the resource has loaded successfully. The `GlobalObjectName` property should contain a JavaScript expression which evaluates to `true` when used in the `if` statement. For jQuery, you can use `window.jQuery`. 

If the resource could not be loaded from the CDN, it would fall back to the `Url`.

If the `EmbeddedResourceAssembly` property is set, then the `Url` is not considered to be the `<script>` or `<link>` URL, and is replaced with the `~/dotvvmEmbeddedResource...` URL that can extract an embedded resource from an assembly. This is very useful if you need to pack some DotVVM controls in a library and embed the resources in the DLL file. 

```CSHARP
configuration.Resources.Register(ResourceConstants.JQueryResourceName,
    new ScriptResource()
    {
        CdnUrl = "https://code.jquery.com/jquery-2.1.1.min.js",
        Url = "DotVVM.Framework.Resources.Scripts.jquery-2.1.1.min.js",
        EmbeddedResourceAssembly = typeof (DotvvmConfiguration).Assembly.GetName().Name,
        GlobalObjectName = "$"
    });
```

> The resource registration has changed in DotVVM 1.1. Visit the [Upgrading to DotVVM 1.1](/docs/tutorials/how-to-start-upgrade-from-1-0/1-1) for more details.
