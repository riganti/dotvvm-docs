# Compilation status page

**Compilation status page** is diagnostic tool for DotVVM which allows to easily check all [DotHTML pages](~/pages/concepts/dothtml-markup/overview), [master pages](~/pages/concepts/layout/master-pages), and [markup controls](~/pages/concepts/control-development/markup-controls), and detect compilation errors in them. It can help while upgrading DotVVM packages, you can quickly ensure that all markup files are valid.

> Prior to DotVVM 4.0, the Compilation status page was distributed as a separate NuGet package. From DotVVM 4.0, it is included in the main `DotVVM` NuGet package. **The package `DotVVM.Diagnostics.StatusPage` should be uninstalled.**

## How it works

Visit `_dotvvm/diagnostics/compilation` to see the list of all `.dothtml` pages, controls and master pages.

DotVVM views are compiled on demand when the page requests a DotHTML file, so you probably won't see any status for most pages initially. Press the **Compile All** button to initiate compilation of all pages and controls, so see all errors in the application.

![Compilation status page](https://raw.githubusercontent.com/riganti/dotvvm-samples-compilation-status-page/42184142d7905be3d2e23661dbb1905c3ed4ba80/docs/sample.PNG)


## Enable the Compilation status page

By default, compilation page is enabled only in Debug environment. Note that the page is only accessible from localhost. You can however enable the page even in production environment, or enable the page 

1. To enable the status page, register it in your `DotvvmStartup.cs` file:

```CSHARP
public void Configure(DotvvmConfiguration config)
{
    // enable even in production.
    config.Diagnostics.CompilationPage.IsEnabled = true;
    // Will be accessible only from localhost
}
```


## Security

Having status page publicly available is not recommended because that would allow potential attackers to list all views and trigger page recompilation which could slow down the entire application.  
Due to this concern, there is an option to specify an authorization function which decides whether user will be allowed to access status page or not.  

The authorization function can be specified during status page registration. Examples of such, the authorization function can check the source IP address, or check whether the user identity contains some specific claim.

This example only checks if the user has a specific name:

```CSHARP
config.Diagnostics.CompilationPage.AuthorizationPredicate =
    async context => context.HttpContext.User.Identity.Name == "Honza";
```


## Accessing compilation results via API

If manual checking using status page is not an option, then the status page API can be used. This can be especially useful for automated checking of the compilation status before/after deployment to the production.

The API can be found (in default configuration) at After registration using `_dotvvm/diagnostics/status/api`.  

The API is enabled using the `IsApiEnabled` option.

```CSHARP
public void Configure(DotvvmConfiguration config)
{
    config.Diagnostics.CompilationPage.IsApiEnabled = true;
    ...
}
```

The API is secured in the same way as the status page is - using the AuthorizationPredicate.

> Note that DotVVM also implements the [ASP.NET Core IHealthCheck](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks) interface.
> When health checks are enabled, DotVVM reports application as healthy when all views are compiled without errors.

## See also

* [Routing](~/pages/concepts/routing/overview)
* [Markup controls](~/pages/concepts/control-development/markup-controls)
* [ASP.NET Core health checks](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks)

