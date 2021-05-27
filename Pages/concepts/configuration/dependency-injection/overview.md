# Dependency injection overview

Dependency injection is used widely in many large applications, and since it is built-in in ASP.NET Core, it is a preferred way of working with services.

**DotVVM** supports dependency injection in [viewmodels](~/pages/concepts/viewmodels/overview), [static command services](~/pages/concepts/respond-to-user-actions/static-command-services), and [custom controls](~/pages/concepts/control-development/overview).

## Register services

In ASP.NET Core, DotVVM uses the `Microsoft.Extensions.DependencyInjection` library to configure and resolve viewmodel and service dependencies.

In OWIN, this library is used by DotVVM too, but there are extensibility points so you can use other dependency injection container of your choice.

* [Register services in ASP.NET Core](aspnetcore)
* [Register services in OWIN](owin)

## Dependency injection in viewmodels

Once your services are registered, you can request them in the constructor as parameters. 

```CSHARP
public class CustomersViewModel 
{
    private readonly CustomerService customerService;
    private readonly ILogging log;

    // the parameters will be injected automatically by the DI container
    public CustomersViewModel(CustomerService customerService, ILogging log) 
    {
        this.customerService = customerService;
        this.log = log;
    }

}
```

You don't need to register the viewmodels in the `IServiceCollection` - if the viewmodel is not registered explicitly, DotVVM inspects its constructor, obtains all dependencies from the `IServiceCollection`, and creates an instance of the viewmodel. 

However, it is possible to register the viewmodels in the `IServiceCollection` explicitly - it can be useful if you want to request other viewmodels as dependencies (for example the child viewmodels).

> Some containers (like [Castle Windsor](https://github.com/castleproject/Windsor)) support also property injection. We recommend against using it in viewmodels as properties are used to represent the viewmodel state. If you want to inject into properties, don't forget to use the `[Bind(Direction.None)]` attribute to tell the serializer to ignore such properties.

## Static command services

[Static command services](~/pages/concepts/respond-to-user-actions/static-command-services) referenced in the page can use dependency injection.

You can inject a service using the `@service` directive in the view and use it in binding expressions. 

```CSHARP
public class MyService
{
    private readonly ILogging log;

    public MyService(ILogging log) 
    {
        this.log = log;
    }

    [AllowStaticCommand]
    public void MyMethod()
    {
        ...
    }
}
```

All services injected using `@service` directive must be registered in the `IServiceCollection` since the lifetime of the services can differ.

```CSHARP
services.AddScoped<MyService>();
```

## Dependency injection in controls

You can also use the dependency injection in custom controls. This is often used to retrieve `IBindingCompilationService` which controls commonly use.

```CSHARP
public class MyControl : HtmlGenericControl 
{
    private readonly IMyService service;

    public MyControl(IMyService service) 
    {
        this.service = service;
    }

    // ...
}
```

## DotVVM services to inject

Note that you can use to inject your own services, but also services of the DotVVM framework:

* `ResourceManager` - you can simply register a DotVVM resource for that request in control constructor
* `IDotvvmRequestContext` - although in controls you get the request context in every lifecycle event, you can use constructor injection in `DotvvmBindableObjects` that are not `DotvvmControl`, for example in postback handlers or in [GridView](~/controls/builtin/GridView) columns.

## See also

* [Register services in ASP.NET Core](aspnetcore)
* [Register services in OWIN](owin)
* [Configuration overview](../overview)