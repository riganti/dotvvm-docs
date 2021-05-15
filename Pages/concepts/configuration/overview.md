# Configuration overview

DotVVM uses C# code to configure its features and settings. A typical DotVVM app needs to configure the following areas:

+ **Routes** (see more information in the [Routing](~/pages/concepts/routing/overview) chapter)

+ **Scripts and styles** (see more in the [Script & style resources](~/pages/concepts/script-and-style-resources/overview) chapter)

+ **Custom controls** (see more in the [Control development](~/pages/concepts/control-development/overview) chapter)

+ **Services** that handle [File uploads](~/pages/concepts/upload-and-download-files/upload-files), [Returned files](~/pages/concepts/upload-and-download-files/return-file-from-viewmodel) or [Dependency injection](dependency-injection/overview)

## Configuration files

In the default project template, there are 2 files - `Startup.cs` and `DotvvmStartup.cs`:

* `Startup.cs` is a main configuration of ASP.NET Core and OWIN applications. In this file, we need to register DotVVM services and middleware in the request pipeline. 

* `DotvvmStartup.cs` file contains DotVVM-specific configuration - routes, resources, and controls.

### Startup.cs

# [ASP.NET Core](#tab/aspnetcore)

In ASP.NET Core, the registration of all frameworks is split to registration of services, and registration of middlewares. 

In the `ConfigureServices` method, we need to register DotVVM services:

```CSHARP
services.AddDotVVM<DotvvmStartup>();
```

In the `Configure` method, we have to register the DotVVM middleware.

```CSHARP
var config = app.UseDotVVM<DotvvmStartup>();
```

This extension method initializes the middlewares required by DotVVM. 

The `DotvvmStartup` type argument of the `AddDotVVM` and `UseDotVVM` methods represents the class which contains DotVVM configuration.

# [OWIN](#tab/owin)

In OWIN, the `Startup.cs` contains the OWIN startup class. DotVVM is just an OWIN middleware - you can easily combine it with ASP.NET MVC or any other OWIN middlewares in one application. 

All you have to do is to register the DotVVM middleware in the `IAppBuilder` object:

```CSHARP
var config = app.UseDotVVM<DotvvmStartup>(HostingEnvironment.ApplicationPhysicalPath);
```

This extension method initializes the middlewares required by DotVVM. The `DotvvmStartup` type argument of the `UseDotVVM` method represents the class which contains DotVVM configuration.

***

### DotvvmStartup.cs

The `DotvvmStartup` class must implement the `IDotvvmStartup` interface and contains the `Configure` method. There should be only one class implementing the `IDotvvmStartup` interface in the assembly.

`DotvvmStartup` also commonly implements the `IDotvvmServiceConfigurator` interface and has the `ConfigureServices` method which is responsible for registering DotVVM-related services. You can move the implementation into a separate class.

The default project template prepares this class in the structure below. We have also included examples of how to configure a route, custom control namespace and script resource.

```CSHARP
public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
{
    // IDotvvmStartup implementation

    public void Configure(DotvvmConfiguration config, string applicationPath)
    {
        ConfigureRoutes(config, applicationPath);
        ConfigureControls(config, applicationPath);
        ConfigureResources(config, applicationPath);
    }

    private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
    {
        config.RouteTable.Add("Default", "", "Views/default.dothtml");

        // Uncomment the following line to auto-register all dothtml files in the Views folder
        // config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));    
    }

    private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
    {
        // register code-only controls and markup controls
        config.Markup.AddCodeControls("cc", typeof(MyCustomControl));
    }

    private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
    {
        // register custom resources and adjust paths to the built-in resources
        config.Resources.Register("myscript", new ScriptResource()
        {
            Location = new LocalFileResourceLocation("~/wwwroot/Scripts/myscript.js")
        });
    }


    // IDotvvmServiceConfigurator implementation

    public void ConfigureServices(IDotvvmServiceCollection services)
    {
        // configure all DotVVM-related services
        services.AddDefaultTempStorages("Temp");
    }

}
```

Please note that the [Visual Studio Extension](https://www.dotvvm.com/products/visual-studio-extensions) runs the `Configure` and `ConfigureServices` methods in the `DotvvmStartup` class during the project build process - this is needed so the IntelliSense can retrieve control, route, and resource names from your app.

> Avoid registering any other things than routes, custom resources and custom controls in the `Configure` method. This method is executed during the project build in Visual Studio, so please don't launch rockets into Space in these methods. If you need to register or initialize anything else (e.g. initialize the database, create default users), do it in the `Startup.cs`, or anywhere else.

## Debug mode

The `DotvvmConfiguration` object contains the `Debug` property which should be set to `true` in the development environment, and turned off in production.

If the `Debug` mode is enabled,

* DotVVM displays a developer-friendly error page for all unhandled exceptions that occur
* a non-minified versions of scripts is used, so the debugging is easier
* validation errors are indicated using a red popup that appears in the top right corner of the page

The property is automatically set in ASP.NET Core based on [IWebHostEnvironment.IsProduction()](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.hostingenvironmentextensions?view=dotnet-plat-ext-5.0). 

In OWIN, you need to set the value yourself. 

The typical setup that is present in default DotVVM OWIN project, looks like this:

```CSHARP
private bool IsDebug()
{
#if DEBUG
    return true;
#endif
    return false;
}

...

app.UseDotVVM<DotvvmStartup>(applicationPhysicalPath, debug: IsDebug());
```

## See also

* [Routing](~/pages/concepts/routing/overview)
* [Script & style resources](~/pages/concepts/script-and-style-resources/overview)
* [Control development](~/pages/concepts/control-development/overview)
* [Dependency injection](dependency-injection/overview)
* [View compilation modes](view-compilation-modes)
* [Explicit assembly loading](explicit-assembly-loading)
