# Adding DotVVM in Existing Web App (OWIN)

To add **DotVVM** in an existing ASP.NET project, simply install **DotVVM.Owin** Nuget package using Package Manager Console:

    Install-Package DotVVM.Owin

This command will also reference the dependent packages **DotVVM.Framework** and **DotVVM.Core**.

## Initialization

The next thing you have to do, is to register the DotVVM middleware in the OWIN pipeline. In your OWIN startup class, you have to register the DotVVM middleware. 

If your project doesn't use OWIN, you will need to add the following NuGet package:

    Install-Package Microsoft.Owin.Host.SystemWeb

If you don't have an OWIN startup class in your projects, right click the project in the __Solution Explorer__ window and add a new __OWIN Startup Class__ in the project. It should look like this:

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
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(applicationPhysicalPath, debug: IsDebug());
        }

        private bool IsDebug()
        {
#if DEBUG
            return true;
#endif
            return false;
        }
    }
}
```

## Adding the DotvvmStartup class

Notice that the code references the `DotvvmStartup` class. It is a class you have to add in your project too. 
This class contains the configuration of DotVVM itself, e.g. the registration of routes in your app.

```CSHARP
using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;

namespace DotvvmDemo
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        public void ConfigureServices(IDotvvmServiceCollection services)
        {
            services.AddDefaultTempStorages("Temp");
        }

        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            // register your routes, controls and resources here
        }        
    }
}
```