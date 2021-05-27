# Dependency injection in OWIN

The `DotvvmStartup` class implements `IDotvvmServiceConfigurator` interface with the following method:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    options.Services.AddSingleton(...);
}
```

In this method, you can register any services in the `IServiceCollection`. You can register custom services using `options.Services.AddTransient`, `options.Services.AddSingleton` or `options.Services.AddScoped`. 

The `IServiceCollection` uses the implementation from the `Microsoft.Extensions.DependencyInjection` library. 

If you are fine with using this library, you can register all services in the `ConfigureServices` mentioned above. Then, everything will work as described in the [Dependency injection](overview) chapter.

## Custom viewmodel loader

If you are using a different dependency injection container, you'll want to resolve viewmodel dependencies using this container.

By default, when DotVVM needs to create a viewmodel, it looks in the `IServiceCollection` if the viewmodel is registered. If not, it then inspects its constructor and tries to obtain all the dependencies from the `IServiceCollection`.

This is done in the `DefaultViewModelLoader` class, which is a default implementation of the `IViewModelLoader` interface. If you need to plug a dependency injection container in, you can create a class that inherits `DefaultViewModelLoader`, and override the `CreateViewModelInstance` method.

### Castle Windsor

[Castle Windsor](https://github.com/castleproject/Windsor) is one of the favorite DI containers in .NET. 

Here is how to create the viewmodel loader using this container. Notice that we call `container.Resolve` in the `CreateViewModelInstance`, and `container.Release` in the `DisposeViewModel`.

```CSHARP
using System;
using Castle.Windsor;
using DotVVM.Framework.ViewModel.Serialization;

namespace DotvvmDemo.Web
{
    public class WindsorViewModelLoader : DefaultViewModelLoader
    {
        private readonly WindsorContainer container;

        public WindsorViewModelLoader(WindsorContainer container)
        {
            this.container = container;
        }

        protected override object CreateViewModelInstance(Type viewModelType, IDotvvmRequestContext context)
        {
            return container.Resolve(viewModelType);
        }

        public override void DisposeViewModel(object instance)
        {
            container.Release(instance);
            base.DisposeViewModel(instance);
        }
    }
}
```

The last thing is to replace the default viewmodel loader with the one you have just created. We should do this in the `DotvvmStartup.cs` class:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{  
    // obtain the WindsorContainer instance from the place where you initialize it
    var windsorContainer = ...

    options.Services.AddSingleton<IViewModelLoader>(serviceProvider => new WindsorViewModelLoader(windsorContainer));
}
```

### Other containers

If you use another container, the implementation will be very similar. Don't forget to tell the container to release the instances in the `DisposeViewModel` method. This method is called when the HTTP request ends and DotVVM no longer needs the viewmodel object.

Some containers do this by creating a "scope" in the `CreateViewModelInstance` method and disposing the scope in the `DisposeViewModel` method.

## Static command services

If you are using a different dependency injection container, registering all components in `IServiceCollection` in `DotvvmStartup` can be problematic and can lead to registration of the same services in the two containers.

Instead, you might use a custom `IStaticCommandServiceLoader` to have your service instances resolved directly from your container.

```CSHARP
using System;
using Castle.Windsor;
using DotVVM.Framework.ViewModel.Serialization;

namespace DotvvmDemo.Web
{
    public class WindsorStaticCommandServiceLoader : DefaultStaticCommandServiceLoader
    {
        private readonly WindsorContainer container;

        public WindsorStaticCommandServiceLoader(WindsorContainer container)
        {
            this.container = container;
        }

        public override object GetStaticCommandService(Type serviceType, IDotvvmRequestContext context)
        {
            return container.Resolve(serviceType);
        }

    }
}
```

To register the alternative loader, replace the default one using the following code in `DotvvmStartup.cs`:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection services)
{
    services.Services.AddSingleton<IStaticCommandServiceLoader>(serviceProvider => new WindsorStaticCommandServiceLoader(container));
}
```

## See also

* [Dependency injection overview](overview)
* [Configuration overview](../overview)