# Script and style resources overview

DotVVM has a built-in mechanism for managing resources. It supports JavaScript files, inline JavaScript snippets, and CSS files. It is extensible so it can be used to work with fonts, icons, and other kinds of static files.

The resources are named and stored in a global repository which is configured in [DotVVM configuration](~/pages/concepts/configuration/overview). 

Each resource can also specify its dependencies. Thanks to this, DotVVM can include all required resources in the correct order. 

Finally, if any DotVVM control needs a particular resource to be included in the page, it can just request it to be included in the page. DotVVM keeps track of the resources required by controls in the page, and renders only the those which are really needed.

## Resource types

We have the following types of resources:

* `ScriptResource` renders the `<script>` element and is used to include JavaScript files.

* `ScriptModuleResource` renders the `<script type="module">` element and is used to include JavaScript files in the ES module format.

* `StylesheetResource` renders the `<link rel="stylesheet">` element and is used to include CSS files.

* `InlineScriptResource` renders the `<script>` element with JavaScript code snippet.

* `InlineStylesheetResource` renders the `<style>` element with CSS code snippet.

* `TemplateResource` renders the `<script type="text/html">` template used by some controls (e. g. [Repeater](~/controls/builtin/Repeater)).

* `NullResource` is a special type of resource that doesn't render anything. It is used when some control requests the resource to be included in the page, however you have included the resource itself (e.g. in the master page).

There are also `ViewModuleImportResource`, `ViewModuleInitResource`, and other types of resources that server internal purposes of DotVVM, and should not be used directly.

> In DotVVM 3.0, all script resource are using the [defer](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/script) option by default, because the client-side part of DotVVM was rewritten to use ES modules (with a System polyfill for IE11). Because of that, DotVVM scripts are deferred, and thus, all custom scripts (which in most cases depend on DotVVM) have this option enabled by default. You can set `Defer` to `false` on custom scripts.

## Register resources

All resources are registered in resource repository found in the `config.Resources` collection.

You can register a new resource with the `Register` method. This method can also replace existing resources if they exists. The resources should be registered in the `DotvvmStartup.cs` file.

```CSHARP
config.Resources.Register("bootstrap-css", new StylesheetResource()
{
    Location = new UrlResourceLocation("~/Content/bootstrap.min.css")
});
config.Resources.Register("bootstrap", new ScriptResource()
{
    Location = new UrlResourceLocation("~/Scripts/bootstrap.min.js"),
    Dependencies = new[] { "bootstrap-css", "jquery" }
});
```

In the code, you can retrieve the resource by its name using `FindResource` method. If you need to change the path for the `jquery` resource, you can do it like this:

```CSHARP
var jquery = config.Resources.FindResource("jquery") as ScriptResource;
jquery.Location = new UrlResourceLocation("~/Scripts/jquery.2.1.1.min.js");
jquery.LocationFallback = null;
```

### Resource locations

Most resources have the `Location` property of type `IResourceLocation` which defines, how the resource file is obtained. You can use one of the following implementations:

* `UrlResourceLocation` specifies just the URL where the resource can be found. You can use either absolute URL (e.g. to point to some CDN), a relative URL to your server, or even a data URI. DotVVM will render the `<script>` or `<link>` element with the exact URL you have specified.

* `LocalFileResourceLocation` expects the app-relative filesystem path to the script or stylesheet file. This path should not start with `/` - it would point to the root of the filesystem. DotVVM will render the `<script>` or `<link>` element which points to a DotVVM resource handler (`~/dotvvmResource/checksum/resourceName`) that will serve the resource. This is useful for bundling or advanced scenarios.

* `EmbeddedResourceLocation` can extract the embedded resource from an assembly. This is very useful if you need to pack some DotVVM controls in a library and embed the resources in the DLL file.

You can of course implement custom resource types and resource location implementations. 

### CDN fallbacks

If you want to use CDN for script files, it is often a good idea to have a local fallback for the case that CDN is down, or if you are debugging the app without the Internet connection. 

There is a property called `LocationFallback`. If it is set, the framework will try to load the script from the primary location (the CDN) first and will use the `ResourceLocationFallback.JavasciptCondition` to check whether the resource has loaded successfully. The `JavascriptCondition` property should contain a JavaScript expression which evaluates to `true` when used in the `if` statement. For jQuery, you can use `window.jQuery`. 

If the resource could not be loaded from the CDN, it would fall back to the `AlternativeLocations` and use the first one that works.

```CSHARP
configuration.Resources.Register(ResourceConstants.JQueryResourceName,
	new ScriptResource()
	{
		Location = new UrlResourceLocation("https://code.jquery.com/jquery-2.1.1.min.js"),
		LocationFallback = new ResourceLocationFallback("window.jQuery", 
		    new EmbeddedResourceLocation(typeof(DotvvmConfiguration).GetType().Assembly, "DotVVM.Framework.Resources.Scripts.jquery-2.1.1.min.js")),
		VerifyResourceIntegrity = true
	});
```

If the `VerifyResourceIntegrity` property on the `ScriptResource` is set to true, then it will use the `LocationFallback` to automatically compute the subresource integrity hash, for an extra guarantee that the remote resource being downloaded is the one intended.

## Registering jQuery

If you application uses jQuery and if it is not included with another library (like [Bootstrap for DotVVM](~/pages/bootstrap-for-dotvvm/v4/getting-started) or [DotVVM Business Pack](~/pages/business-pack/getting-started)), add the following code into `ConfigureResources` method in `DotvvmStartup.cs`:

```CSHARP
config.Resources.Register("jquery", new ScriptResource()
{
    // use relative URL if you ship jQuery with your application
    Location = new UrlResourceLocation("https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js")
});
```

## Built-in resources

DotVVM already includes the following built-in resources:

* `dotvvm` - a fundamental set of function required by DotVVM to work correctly.

* `dotvvm.debug` - a helper that displays exception details from the commands. It is only included in the page in [debug mode](~/pages/concepts/configuration/overview).

* `dotvvm.fileUpload-css` - a CSS styles for the [FileUpload](~/controls/builtin/FileUpload) control.

* `knockout` - Knockout JS 3.5.0 (with a few tweaks).

* `globalize` - a modified version of the globalize.js library.

To support client-side number and date formats, there are also automatically generated resources for every culture:

* `globalize:en-US` - a globalization resources for en-US culture. All cultures in .NET Framework are supported, 
however only a subset of the number and datetime formats are supported.

## See also

* [Use resources in pages](use-resources-in-pages)
* [Bundling & minification](bundling-minification)
