# Static Command Services

> This feature is new in **DotVVM 2.0**. 

**Static Command Services** is a feature which allows injecting of a C# class in the page using `@service` directive and calling its methods using [Static Command Binding](/docs/tutorials/basics-static-command-binding/{branch}). This allows to use static command binding on instance methods and using dependency injection.

## Service Registration

First, make sure that the service is registered in `IServiceCollection`. This can be done in `DotvvmStartup.cs`:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    ...

    options.Services.AddSingleton<CalculatorService>();
}
```

To register a static command service, add the following directive at the top of the page:

```DOTHTML
@service _calculator = MyApp.Services.CalculatorService
```

This will create a variable called `_calculator` which you can use in binding expressions in the page. 

If you prefer to use an interface in the view, it is possible - just make sure that it is correctly registered in `IServiceCollection` and that the implementation type or a factory method is provided:

```CSHARP
options.Services.AddScoped<ICalculationService, CalculationService>();
```

You can use any lifetime policy supported by `IServiceProvider`: `Singleton`, `Scoped` and `Transient`.

## Using Static Command Services

You can call any method marked with `[AllowStaticCommand]` attribute using static command binding. Optionally, you can assign the return value to any property in the viewmodel:

```DOTHTML
<dot:Button Text="Calculate" Click="{staticCommand: Result = _calculator.Calculate(Number1, Number2)}" />
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

When the button is clicked, DotVVM will send only the identification of the static command and values of `Number1` and `Number2` properties in the viewmodel. The viewmodel itself is not transferred to the server.