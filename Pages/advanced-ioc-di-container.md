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

> The way of handling dependency injection has been changed in DotVVM 1.1. 

DotVVM 1.1 uses the `Microsoft.Extensions.DependencyInjection` library to configure and resolve services.

## OWIN

In the `Startup.cs` file, every DotVVM application calls the following method:

```CSHARP
app.UseDotVVM<DotvvmStartup>(applicationPhysicalPath, options: options =>
{
    options.Services.AddSingleton...
});
```

The lambda method `options => ...` can be used to configure additional services. The `options` parameter exposes the property called `Services` of type `IServiceCollection` which can be used to register services.

You can register custom services using `options.Services.AddTransient`, `options.Services.AddSingleton` or `options.Services.AddScoped`. 

One of the services that is already present in the collection, is the viewmodel loader represented by the `IViewModelLoader` interface. This class is responsible for locating of the class specified by the `@viewModel` directive in the page and creating an instance of the viewmodel. 

DotVVM uses the `DefaultViewModelLoader` class which locates the class and calls its default constructor. If you need to plug a dependency injection container in, you can create a class that inherits `DefaultViewModelLoader` and override the `CreateViewModelInstance`.

### Custom ViewModelLoader for Castle Windsor

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

### Registering Custom ViewModelLoader

The last thing is to replace the default viewmodel loader with the one you have just created.
We should do this in the `Startup.cs` class:

```CSHARP
app.UseDotVVM<DotvvmStartup>(applicationPhysicalPath, options: options =>
{
    options.Services.AddSingleton<IViewModelLoader>(serviceProvider => new WindsorViewModelLoader(container));
});
```


## ASP.NET Core

ASP.NET Core contains a built-in dependency injection mechanism. In the `Startup.cs` file, there is a method called `ConfigureServices` which registers all application services in the `IServiceCollection` parameter. The collection is managed by the `Microsoft.Extensions.DependencyInjection` library.

When you call `app.UseDotVVM<DotvvmStartup>(...)`, it registers several internal services which DotVVM uses the `IServiceCollection`, for example the view compiler, viewmodel serializer and so on.  

### Registering Services

To register services, you can just call one of the following methods:

```CSHARP
public void ConfigureServices(IServiceCollection services)
{
    ...
    services.AddSingleton<ICustomService, CustomService>();
    services.AddTransient<ICustomService2, CustomService2>();
    services.AddScoped<ICustomService3, CustomService3>();
}
```

DotVVM will be able to inject these services if they are specified as parameters of the viewmodel constructor.

### Using Alternative Container

Optionally, the `ConfigureServices` method can return its own `IServiceProvider` which will be used instead of the default one. This is useful if you want to use an alternative IoC/DI container. 

For example, the default `Microsoft.Extensions.DependencyInjection` library doesn't support advanced scenarios like registering services by conventions or working with open generic types. This can be a reason to use an alternative dependency injection container.

Autofac is one of the popular DI containers which works with ASP.NET Core. You can use this code to use Autofac:

```CSHARP
public IServiceProvider ConfigureServices(IServiceCollection services)
{
    ...

    // create the Autofac container builder
    var builder = new ContainerBuilder();

    // find all modules with container configuration in current assembly
    builder.RegisterAssemblyModules(typeof(Startup).GetTypeInfo().Assembly);

    // combine the rules with the services already registered in the IServiceCollection
    builder.Populate(services);

    // create and return the container
    var applicationContainer = builder.Build();
    return new AutofacServiceProvider(applicationContainer);
}
```