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

    // this service can be injected too if the container supports property injection
    [Bind(Direction.None)]
    public IAdditionalService AdditionalService { get; set; }
}
```

Basically, if you need any services in your viewmodel, you can request them in the constructor as parameters, or you can declare a public property in the viewmodel. In that case, don't forget to use the `[Bind(Direction.None)]` attribute to tell the serializer that it should not care about this property.

DotVVM uses the `Microsoft.Extensions.DependencyInjection` library to configure and resolve viewmodels and services.

* [IoC/DI Containers (OWIN)](/docs/tutorials/advanced-ioc-di-container-owin/{branch})
* [IoC/DI Containers (ASP.NET Core)](/docs/tutorials/advanced-ioc-di-container-aspnetcore/{branch})


## Static Command Services

DotVVM 2.0 added the [Static Command Services](/docs/tutorials/basics-static-command-services/{branch}). 

You can inject a service using the `@service` directive in the view and use it in binding expressions. 


## Dependency Injection in Controls

You can also use the dependency injection in custom controls - simply put the dependencies into contructor:

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

Note that you can use to inject your own services, but also services of the DotVVM framework.

* `ResourceManager` - you can simply register a DotVVM resource for that request in control contructor
* `IDotvvmRequestContext` - although in controls you get the request context in every lifecycle event, you can use constuctor injection in `DotvvmBindableObjects` that are not `DotvvmControl`, for example postback handlers or grid view columns.

All services injected using `@service` directive must be registered in the `IServiceCollection`.