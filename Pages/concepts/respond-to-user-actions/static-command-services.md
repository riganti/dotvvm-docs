# Static command services

**Static command services** is a feature which allows injecting a C# class in the page using the `@service` directive, and calling its methods from [static commands](static-commands). This allows to use static command binding on instance methods and taking advantage of advanced techniques like a [dependency injection](~/pages/concepts/viewmodels/dependency-injection/overview).

## Register the service

A static command service is just a regular C# class exposing public methods that will be called from the static commands. Make sure that the method arguments and return values are JSON-serializable.

First, you need to register the service in the `IServiceCollection`. 

# [ASP.NET Core](#tab/register-aspnetcore)

In ASP.NET Core, this can be done in `Startup.cs` in the `ConfigureServices` method:

```CSHARP
public void ConfigureServices(IServiceCollection services)
{
    ...
        
    services.AddScoped<CalculatorService>();
    // use AddSingleton if the service is thread-safe and its instance can be reused,
    // or use AddTransient if you want a separate instance for each usage of the service (even in the same HTTP request)
}
```

# [OWIN](#tab/register-owin)

In OWIN environment, this can be done in `DotvvmStartup.cs` in the `ConfigureServices` method:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    ...
        
    options.Services.AddScoped<CalculatorService>();
    // use AddSingleton if the service is thread-safe and its instance can be reused,
    // or use AddTransient if you want a separate instance for each usage of the service (even in the same HTTP request)
}
```

***

If the service has any dependencies in the constructor, make sure they are registered in the `IServiceCollection` too.

## Import the service in the page

To import a static command service in the DotHTML file, add the following directive at the top of the page:

```DOTHTML
@service _calculator = MyApp.Services.CalculatorService
```

This will create a variable called `_calculator` which you can use in binding expressions in the page. 

## Use static command service

You can call any method marked with `[AllowStaticCommand]` attribute using static command binding. Optionally, you can assign the return value to any property in the viewmodel:

```DOTHTML
<dot:Button Text="Calculate" 
            Click="{staticCommand: Result = _calculator.Calculate(Number1, Number2)}" />
```

The `Calculate` method is defined like this:

```CSHARP
public class CalculatorService
{

    public CalculatorService(/* you can specify any dependencies - they will be resolved using IServiceProvider */ )
    {
    }

    [AllowStaticCommand]
    public int Calculate(int number1, int number2) 
    {
        return number1 + number2;
    }

}
```

When the button is clicked, DotVVM will send only the identification of the static command and values of `Number1` and `Number2` properties in the viewmodel. The server will respond with the return value of the method. 

The viewmodel itself is not transferred to the server.

> Be careful. There is no way for DotVVM to determine whether the arguments passed to the command weren't tampered with. Always validate that the values are correct and that the user has appropriate permissions to perform the operation.

## See also

* [Static commands](static-commands)
* [Commands](commands)
* [Optimize command performance](optimize-command-performance)
* [Concurrency mode](concurrency-mode)
