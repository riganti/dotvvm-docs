# Configuration

DotVVM uses C# code to configure its features and settings. 

The typical DotVVM app needs to configure the following things:

+ **Routes** (see more information in the [Routing](/docs/tutorials/basics-routing/{branch}) chapter)

+ **Custom Resources** (see more in the [Javascript and CSS Resource Management](/docs/tutorials/basics-javascript-and-css/{branch}) chapter)

+ **Custom Controls** (see more in the [Control Development](/docs/tutorials/control-development-introduction/{branch}) chapter)

+ **Services** that handle [File Uploads](/docs/controls/builtin/FileUpload/{branch}), [Returned Files](/docs/tutorials/advanced-returning-files/{branch}) or [Dependency Injection](/docs/tutorials/advanced-ioc-di-container/{branch})

In the default project template, there are 2 files - `Startup.cs` and `DotvvmStartup.cs`. 

In `Startup.cs`, we configure DotVVM services and register DotVVM middlewares. In `DotvvmStartup.cs`, we configure routes, resources and controls.


## Startup.cs in OWIN

In OWIN, the `Startup.cs` contains the OWIN startup class. DotVVM is just an OWIN middleware and you can easily combine it with ASP.NET MVC or any other OWIN middlewares in one application. 

All you have to do is to register the DotVVM middleware in the `IAppBuilder` object.

```CSHARP
var config = app.UseDotVVM<DotvvmStartup>(ApplicationPhysicalPath);
```

> The configuration of DotVVM services has been changed in **DotVVM 2.0** - it was moved to `DotvvmStartup.cs` file. 

This extension method initializes the middlewares required by DotVVM. The `DotvvmStartup` type parameter of the `UseDotVVM` represents the class which contains DotVVM configuration.

## Startup.cs in ASP.NET Core

In ASP.NET Core, the registration of frameworks is splitted to the registration of services and middlewares. 

In the `ConfigureServices` method, we should register DotVVM services:

```CSHARP
services.AddDotVVM<DotvvmStartup>();
```

In the `Configure` method we have to register the DotVVM middleware.

```CSHARP
var config = app.UseDotVVM<DotvvmStartup>();
```

This extension method initializes the middlewares required by DotVVM. The `DotvvmStartup` type parameter of the `UseDotVVM` represents the class which contains DotVVM configuration.

> The configuration of DotVVM services has been changed in **DotVVM 2.0** - it was moved to `DotvvmStartup.cs` file. 

## DotvvmStartup.cs

The `DotvvmStartup` class must implement the `IDotvvmStartup` interface and contains the `Configure` method. There should be only one class implementing the `IDotvvmStartup` interface in the assembly.

This class configures resources, controls and routes. The default project template prepares this class in the following structure. 
We have also included examples of how to configure a route, custom control namespace and script resource.

`DotvvmStartup` also typically implements `IDotvvmServiceConfigurator` and declares the `ConfigureServices` method that is responsible for registering DotVVM-related services.

```CSHARP
public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
{
    public void ConfigureServices(IDotvvmServiceCollection services)
    {
        // configure all DotVVM-related services
        services.AddDefaultTempStorages("Temp");
    }

    // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
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
}
```

Please note that the [Visual Studio Extension](/landing/dotvvm-for-visual-studio-extension) executes the `Configure` and `ConfigureServices` methods in the `DotvvmStartup` class during the project build process so the IntelliSense can suggest custom controls, route and resource names.

> Avoid registering any other things than routes, custom resources and custom controls in the `Configure` method.
> This method is executed during the project build in Visual Studio, so please don't launch rockets in it.

If you need to register or initialize anything else (e.g. initialize the database, create default users), do it in the `Startup.cs`, or anywhere else.

## Debug Mode

The `DotvvmConfiguration` object contains the `Debug` property which should be turned in the development environment, and turned off in production.

In the `Debug` mode, DotVVM displays an error page for all unhandled exceptions that occur, it uses non-minified versions of scripts (where applicable) and reports validation errors using a red popup that appears in the top right corner of the page.

The property is automatically set in ASP.NET Core based on [IHostingEnvironment.IsDevelopment()](https://docs.microsoft.com/en-us/aspnet/core/api/microsoft.aspnetcore.hosting.hostingenvironmentextensions#Microsoft_AspNetCore_Hosting_HostingEnvironmentExtensions_IsDevelopment_Microsoft_AspNetCore_Hosting_IHostingEnvironment_). In OWIN you need to set the value yourself. 

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

## Static Files

In the default project template, the `Startup` class also registers a static files middleware. DotVVM doesn't need it itself, however in 99% cases you want to use it to serve static files like images to the user.
