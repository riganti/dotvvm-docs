# IoC/DI Containers (OWIN)

The `DotvvmStartup` class implements `IDotvvmServiceConfigurator` interface with the following method:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection services)
{
    services.Services.AddSingleton(...);
}
```

In this method, you can register any services in the `IServiceCollection`. 

You can register custom services using `options.Services.AddTransient`, `options.Services.AddSingleton` or `options.Services.AddScoped`. 

One of the services that is already present in the collection, is the viewmodel loader represented by the `IViewModelLoader` interface. This class is responsible for locating of the class specified by the `@viewModel` directive in the page and creating an instance of the viewmodel. 

DotVVM uses the `DefaultViewModelLoader` class which locates the class and calls its default constructor. If you need to plug a dependency injection container in, you can create a class that inherits `DefaultViewModelLoader` and override the `CreateViewModelInstance`.

## Custom ViewModelLoader for Castle Windsor

**Castle Windsor** is one of the favorite IoC/DI containers in .NET. Here is how to create the viewmodel loader using this container. Notice that we call `container.Resolve` in the `CreateViewModelInstance` and `container.Release` in the `DisposeViewModel`.

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

If you use another container, the implementation will be very similar. Don't forget to tell the container to release the instances in the `DisposeViewModel` method. This method is called when the HTTP request ends and DotVVM no longer needs the viewmodel object.

Some containers do this by initiating a "scope" in the `CreateViewModelInstance` method and disposing the scope in the `DisposeViewModel` method.

## Registering Custom ViewModelLoader

The last thing is to replace the default viewmodel loader with the one you have just created.
We should do this in the `DotvvmStartup.cs` class:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection services)
{
    services.Services.AddSingleton<IViewModelLoader>(serviceProvider => new WindsorViewModelLoader(container));
}
```

## Static Command Services

Since registering all components in `IServiceCollection` on DotVVM startup can be problemmatic, you might use a custom `IStaticCommandServiceLoader` to have your service instances resolved directly from your container.

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
