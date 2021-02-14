# IoC/DI Containers (ASP.NET Core)

ASP.NET Core contains a built-in dependency injection mechanism. In the `Startup.cs` file, there is a method called `ConfigureServices` which registers all application services in the `IServiceCollection` parameter. 

The collection is managed by the `Microsoft.Extensions.DependencyInjection` library.

When you call `app.UseDotVVM<DotvvmStartup>(...)`, it registers several internal services which DotVVM uses the `IServiceCollection`, for example the view compiler, viewmodel serializer and so on.  

## Registering Services

To register services unrelated to DotVVM infrastructure, you can just call one of the following methods:

```CSHARP
public void ConfigureServices(IServiceCollection services)
{
    ...
    services.AddSingleton<ICustomService, CustomService>();
    services.AddTransient<ICustomService2, CustomService2>();
    services.AddScoped<ICustomService3, CustomService3>();
}
```

To register DotVVM-related services (`IUploadedFileStorage` for example), use the `ConfigureServices` method in `DotvvmStartup`:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection services)
{
    services.Services.AddSingleton(...);
}
```

DotVVM will be able to inject these services if they are specified as parameters of the viewmodel constructor. 

## Using Alternative Container

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
