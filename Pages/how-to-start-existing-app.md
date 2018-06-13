# Adding DotVVM in Existing Web App

**DotVVM** can be easily combined with other ASP.NET frameworks, like ASP.NET Web Forms, SignalR, ASP.NET Web API or ASP.NET MVC.

DotVVM is yet another OWIN / ASP.NET Core middleware that can be added in the request processing pipeline.

## Sample Integration Projects

We have prepared the following sample projects that combine DotVVM with other frameworks in one application:

* [DotVVM with ASP.NET Web Forms](https://github.com/riganti/dotvvm-samples-combo-with-webforms)
* [DotVVM with ASP.NET MVC](https://github.com/riganti/dotvvm-samples-combo-with-mvc)
* [DotVVM with Nancy Fx](https://github.com/riganti/dotvvm-samples-combo-with-nancy)


## Installing the Nuget Package (OWIN)

To add **DotVVM** in an existing ASP.NET project, simply install **DotVVM** Nuget package using Package Manager Console:

    Install-Package DotVVM.Owin

The package will add a reference to **DotVVM.Framework** and **DotVVM.Core** libraries to the project.

### Initialization

The next thing you have to do, is to register the DotVVM middleware to the OWIN pipeline. In your OWIN startup class, you have to register the DotVVM middleware. 

If you don't have an OWIN startup class in your projects, right click the project in the __Solution Explorer__ window and 
add a new _OWIN Startup Class_ in the project. It should look like this:

```CSHARP
using System.Web.Hosting;
using Microsoft.Owin;
using Owin;
using DotVVM.Framework;
    
[assembly: OwinStartup(typeof(DotvvmDemo.Startup))]
namespace DotvvmDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // initialize DotVVM
            var applicationPhysicalPath = HostingEnvironment.ApplicationPhysicalPath;
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(applicationPhysicalPath);    
        }
    }
}
```

<br />

## Installing the Nuget Package (ASP.NET Core)

If you use the new ASP.NET Core stack, you need the ASP.NET Core version of DotVVM.

To add **DotVVM** in an existing ASP.NET Core project, simply install `DotVVM.AspNetCore` Nuget package using Package Manager Console:

    Install-Package DotVVM.AspNetCore

The package will reference **DotVVM.Framework** and **DotVVM.Framework.Hosting.AspNetCore** libraries in the project.


### Initialization

You have to do two things in the `Startup` class:

* Add DotVVM services in the `IServiceCollection` object.

* Add the DotVVM middlewares in the ASP.NET Core request pipeline.

First, add the following code snippet in the `ConfigureServices` method:

```CSHARP
services.AddDotVVM(options =>
{
    options.AddDefaultTempStorages("Temp");
});
```

Second, add the following code snippet in the `Configure` method. If you are using some authentication middlewares, remember that these should be registered first.

```CSHARP
var config = app.UseDotVVM<DotvvmStartup>();
```

## DotvvmStartup class (both OWIN and ASP.NET Core)

Notice that the code references the `DotvvmStartup` class. It is a class you have to add in your project too. 
This class contains the configuration of DotVVM itself, e.g. the registration of routes in your app.

```CSHARP
using DotVVM.Framework.Compilation.Parser;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Routing;

namespace DotvvmDemo
{
    public class DotvvmStartup : IDotvvmStartup
    {
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
#if !DEBUG
            config.Debug = false;
#endif

            // register your routes, controls and resources here
        }        
    }
}
```

The `Debug` property is automatically set in ASP.NET Core based on [IHostingEnvironment.IsDevelopment()](https://docs.microsoft.com/en-us/aspnet/core/api/microsoft.aspnetcore.hosting.hostingenvironmentextensions#Microsoft_AspNetCore_Hosting_HostingEnvironmentExtensions_IsDevelopment_Microsoft_AspNetCore_Hosting_IHostingEnvironment_). In OWIN you need to handle that yourself.

> Please note that in **DotVVM 1.0**, the NuGet package name is `DotVVM`. This package contains the OWIN hosting infrastructure.
> If you use **[DotVVM 1.1 or higher](/docs/tutorials/how-to-start-existing-app/1-1)**, you need to install either `DotVVM.Owin` or `DotVVM.AspNetCore` package, depending on the project type you are using. The `DotVVM` package contains the framework classes only. 
