# IoC/DI Containers

Dependency Injection is used widely in many large applications. **DotVVM** allows you to have your services injected in the viewmodel constructor or properties.

```CSHARP
public class CustomersViewModel 
{
    // the parameters will be injected automatically by the DI container
    public CustomersViewModel(CustomerService customerService, ILogging log) 
    {
        ...        
    }

    // this service can be injected too
    [Bind(Direction.None)]
    public IAdditionalService AdditionalService { get; set; }
}
```

Basically, if you need any services in your viewmodel, you can request them in the constructor as parameters, or you can declare a public property in the viewmodel. In that case, don't forget to use the `[Bind(Direction.None)]` attribute to tell the serializer that it should not care about this property.

## Enabling the Dependency Injection

> The way of handling dependency injection has been changed in DotVVM 1.1 and the dependency injection is based on the `Microsoft.Extensions.DependencyInjection` library. 

Because there is no dependency injection mechanism built in .NET Framework and an external libraries have to be used for this purpose, you have to perform additional configuration steps to enable the dependency injection.

The `DotvvmConfiguration` object contains a property called `ServiceLocator`. This property contains a class that manages internal services of DotVVM, e.g. view compiler, viewmodel serializer etc.

One of the internal services is the viewmodel loader represented by the `IViewModelLoader` interface. This class is responsible for locating of the class specified by the `@viewModel` directive in the page and creating an instance of the viewmodel. 

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

        protected override object CreateViewModelInstance(Type viewModelType)
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
We should do this in the `Startup.cs` class:

```CSHARP
dotvvmConfiguration.ServiceLocator.RegisterSingleton<IViewModelLoader>(() => new WindsorViewModelLoader(container));
```

> Please note that the `ServiceLocator` property is no longer used in DotVVM 1.1.