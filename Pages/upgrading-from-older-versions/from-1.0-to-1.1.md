# Upgrading to DotVVM 1.1

Because **DotVVM 1.1** brought support for ASP.NET Core, we had to do many fundamental changes inside the framework, and there are several breaking changes.

If your app is built on **DotVVM 1.0**, you need to take the following steps to upgrade to **DotVVM 1.1 on OWIN**.



## 1. Install the DotVVM.Owin NuGet Package

In the 1.0 version, the **DotVVM** NuGet package included the framework and the hosting infrastructure together in one library.

In the 1.1 version, we have moved the OWIN hosting infrastructure to a separate package, and that's why you need to add it to your project.



## 2. Change OwinContext to GetOwinContext() or to HttpContext

The `Context` object which is available in the viewmodel, had the `OwinContext` property which could access the information about the current HTTP request
and manipulate with the response.

Because the API changed on ASP.NET Core, we have added the `HttpContext` property which provides a unified API for OWIN and ASP.NET Core. 
However, there are some differences and there are not all features available in the original `OwinContext` property.

If you want to get the real OWIN context, you can call the `GetOwinContext()` extension method and get the same API. The `GetOwinContext()` method is
in the `Dotvvm.Framework.Hosting` namespace.



## 3. Change OwinContext.Authentication to GetAuthentication()

The same thing applies to `OwinContext.Authentication` property. You can use the `GetAuthentication()` extension method to get this API quickly.



## 4. Change DotvvmRequestContext to IDotvvmRequestContext in Custom Presenters

In the 1.0 version, the `IDotvvmRequestContext` declared the `ProcessRequest` method with an argument of type `DotvvmRequestContext`, which was wrong.

In the 1.1 version, we fixed this, so the argument is `IDotvvmRequestContext`, which allows to mock the context easily when testing the presenter.


## 5. DefaultViewModelLoader requires IServiceProvider

If you derived from the `DefaultViewModelLoader` class to do the dependency injection in the viewmodels, you need to request the `IServiceProvider` argument
in the constructor of your class, and pass it to the constructor of `DefaultViewModelLoader`. 

```CSHARP
public class WindsorViewModelLoader : DefaultViewModelLoader
{
    private readonly WindsorContainer container;

    public WindsorViewModelLoader(WindsorContainer container)
    {
        this.container = container;
    }

    ...
}
```


## 6. ServiceLocator changes to IServiceProvider

Since Microsoft has brought their own dependency injection mechanisms in the ASP.NET Core, to make the things unified, we have used this infrastructure 
in DotVVM too.

If you registered custom services in the `DotvvmConfiguration.ServiceLocator`, you have to change the registrations to the following ones:

```CSHARP
var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(applicationPhysicalPath, options: options =>
{
    options.Services.AddSingleton<IViewModelLoader>(serviceProvider => new WindsorViewModelLoader(container));
});
```

You will need to import the `Microsoft.Extensions.DependencyInjection` namespace to do so.

Also, you can only register services in the `UseDotVVM` method by passing a method to the `options` parameter. 

If you try to add services in the `IServiceProvider` later, it may fail because the object denies changes to its configuration after the first service is resolved.



## 7. Make ActionFilter and ExceptionFilter Methods Async

We needed to change all methods on the `ActionFilterAttribute` and `ExceptionFilterAttribute` to async versions. 

```CSHARP
protected override Task OnCommandExceptionAsync(IDotvvmRequestContext context, ActionInfo actionInfo, Exception exception) 
{
    ...
}
```

Also, if you want to intercept only some of the methods, you don't need to derive from the `ActionFilterAttribute`, but you can only implement one of the 
following interfaces:

* `IPageActionFilter` contains page-level events - `OnPageLoadingAsync`, `OnPageLoadedAsync` and `OnPageExceptionAsync`. 

* `ICommandActionFilter` contains command-related events - `OnCommandExecutingAsync` and `OnCommandExecutedAsync`.

* `IViewModelActionFilter` contains viewmodel-related events - `OnViewModelCreatedAsync` and `OnViewModelDeserializedAsync` and `OnViewModelSerializingAsync` (which replaces the `OnResponseRendering`).


## 8. Rename the HideNonAuthenticatedUsers to HideForAnonymousUsers on RoleView

If you are using the `<dot:RoleView>` control, please rename the `HideNonAuthenticatedUsers` to `HideForAnonymousUsers` property which makes better sense.


## 9. Resource Registrations Changed

To support advanced scenarios, we had to change the way how resources are registered.

The `ScriptResource` and `StylesheetResource` classes don't have the `Url` property any more, they got the `Location` property instead. Additionally, the `Location` is not a `string`, but it can be of the following classes:

* `UrlResourceLocation` specifies just the URL where the resource can be found. You can use either absolute URL (e.g. to point to some CDN), a relative URL to your server, or even a data URI. DotVVM will render the `<script>` or `<link>` element with the exact URL you have specified.

* `LocalFileResourceLocation` expects the app-relative filesystem path to the script or stylesheet file. This path should not start with `/` - it would point to the root of the filesystem. DotVVM will render the `<script>` or `<link>` element which points to a DotVVM resource handler (`~/dotvvmResource/checksum/resourceName`) that will serve the resource. This is useful for bundling or advanced scenarios.

* `EmbeddedResourceLocation` can extract the embedded resource from an assembly. This is very useful if you need to pack some DotVVM controls in a library and embed the resources in the DLL file.

Also, we have dropped the following properties from the `ScriptResource` class:

* `CdnUrl` is replaced with `LocationFallback` property and supports multiple fallback locations.

* `EmbeddedResourceAssembly` property which switched the resource to the embedded resource mode, was replaced with the `EmbeddedResourceLocation` object.

* `GlobalObjectName` was moved to `LocationFallback.JavascriptCondition`.

In basic scenarios, you just need to replace the `Url` with `Location` and wrap the string URL in the `UrlResourceLocation`:

```CSHARP
config.Resources.Register("bootstrap", new ScriptResource()
{
    // Url = "~/Scripts/bootstrap.min.js",
    Location =new UrlResourceLocation("~/Scripts/bootstrap.min.js"),
    
    Dependencies = new[] { "bootstrap-css", "jquery" }
});
```

If you have used the embedded resources, you should use the following way of working with `CdnUrl`, `GlobalObjectName` and `EmbeddedResourceAssembly`:

```CSHARP
configuration.Resources.Register(ResourceConstants.JQueryResourceName,
    new ScriptResource()
    {
        // CdnUrl = "https://code.jquery.com/jquery-2.1.1.min.js",
        // Url = "DotVVM.Framework.Resources.Scripts.jquery-2.1.1.min.js",
        // EmbeddedResourceAssembly = typeof (DotvvmConfiguration).Assembly.GetName().Name,
        // GlobalObjectName = "window.jQuery"

        Location = new UrlResourceLocation("https://code.jquery.com/jquery-2.1.1.min.js"))
        {
            LocationFallback = new ResourceLocationFallback(
                "window.jQuery",
                new EmbeddedResourceLocation(typeof(DotvvmConfiguration).GetTypeInfo().Assembly, "DotVVM.Framework.Resources.Scripts.jquery-2.1.1.min.js"))
        }
    });
```