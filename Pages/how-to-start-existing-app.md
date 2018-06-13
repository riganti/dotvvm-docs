# Adding DotVVM in Existing Web App

**DotVVM** can be easily combined with other ASP.NET frameworks, like SignalR, Web API or MVC. 
DotVVM is yet another **OWIN middleware** that can be added in the request processing pipeline.


## Installing the Nuget Package (OWIN)

To add **DotVVM** in an existing ASP.NET project, simply install **DotVVM** Nuget package using Package Manager Console:

    Install-Package DotVVM -Pre

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

> Please note that in **DotVVM 1.0**, the NuGet package name is `DotVVM`. This package contains the OWIN hosting infrastructure.
> If you use **[DotVVM 1.1 or higher](/docs/tutorials/how-to-start-existing-app/1-1)**, you need to install either `DotVVM.Owin` or `DotVVM.AspNetCore` package, depending on the project type you are using. The `DotVVM` package contains the framework classes only. 
