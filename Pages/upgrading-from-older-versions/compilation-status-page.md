# Compilation status page

**Compilation status page** is an extension for DotVVM which allows to easily check all [DotHTML page](~/pages/concepts/dothtml-markup/overview), [master page](~/pages/concepts/layout/master-pages), or [markup control](~/pages/concepts/control-development/markup-controls), and detect compilation errors in them. 

It is a very useful tool which can help while you upgrade DotVVM packages in your app. It can quickly ensure that all markup files are valid.

## How it works

DotVVM views are compiled on demand when the page requests a DotHTML file. This package adds you one diagnostics page to you dotvvm application. When you access this status page by default on route `_diagnostics/status`, all DotHTML files registered in `DotvvmStartup.cs` will be compiled, and all errors will be reported.

![Compilation Status Page](https://raw.githubusercontent.com/riganti/dotvvm-samples-compilation-status-page/42184142d7905be3d2e23661dbb1905c3ed4ba80/docs/sample.PNG)

## Install Compilation Status Page

1. Install the `DotVVM.Diagnostics.StatusPage` NuGet package in the project

2. Register the extension in your `DotvvmStartup.cs`

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection services)
{
    services.AddStatusPage();
    ...
}
```

3. Run the app and visit `_diagnostics/status` to see the status.

## See also

* [Routing](~/pages/concepts/routing/overview)
* [Markup controls](~/pages/concepts/control-development/markup-controls)

