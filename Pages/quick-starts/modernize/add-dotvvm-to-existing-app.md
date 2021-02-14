# Adding DotVVM in Existing Web App (ASP.NET Core)

If you use the new ASP.NET Core stack, you need the ASP.NET Core version of DotVVM.

To add **DotVVM** in an existing ASP.NET Core project, simply install `DotVVM.AspNetCore` Nuget package using Package Manager Console:

    Install-Package DotVVM.AspNetCore

This command will also reference the dependent packages **DotVVM.Framework** and **DotVVM.Core**.


## Initialization

To register DotVVM in the request pipeline, you have to do two things in the `Startup` class:

* Add DotVVM services in the `IServiceCollection` object.

* Add the DotVVM middlewares in the ASP.NET Core request pipeline.

First, add the following code snippet in the `ConfigureServices` method:

```CSHARP
services.AddDotVVM<DotvvmStartup>();
```

Second, add the following code snippet in the `Configure` method. If you are using some authentication middlewares, remember that these should be registered first.

```CSHARP
var config = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);
```

The `config.Debug` property is set automatically based on [IHostingEnvironment.IsDevelopment()](https://docs.microsoft.com/en-us/aspnet/core/api/microsoft.aspnetcore.hosting.hostingenvironmentextensions#Microsoft_AspNetCore_Hosting_HostingEnvironmentExtensions_IsDevelopment_Microsoft_AspNetCore_Hosting_IHostingEnvironment_). 

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