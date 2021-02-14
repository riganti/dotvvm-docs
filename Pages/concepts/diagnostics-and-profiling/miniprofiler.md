# MiniProfiler

> This feature is available in DotVVM 1.1.5 and higher.

[MiniProfiler](http://miniprofiler.com/dotnet/) is a library and UI for profiling your application. By letting you see where your time is spent, which queries are run, 
and any other custom timings you want to add. It helps you debug issues and optimize performance.

> MiniProfiler was designed by the team at [Stack Overflow](https://stackoverflow.com/) and is extensively used in production by their team.

**DotVVM** supports this library and tracks some additional metrics which it collects during the processing of an HTTP request.

You can display the collected **DotVVM** metrics by adding the MiniProfiler Widget to a DOTHTML page:

<img src="{imageDir}advanced-miniprofiler-widget.png" alt="DotVVM metrics in MiniProfiler" class="img-responsive" /> 

It is also possible to view the data traced in previous HTTP requests:

<img src="{imageDir}advanced-miniprofiler-page.png" alt="List of traced HTTP requests with details" class="img-responsive" />

> MiniProfiler is capable of profiling other 3rd party services, e.g. Entity Framework, Entity Framework Core and Redis.

## Getting Started

MiniProfiler installation and configuration differs for [ASP.NET Core](#AspNetCore) and [OWIN](#Owin). You can find the details for extended configuration 
in the **[MiniProfiler documentation](http://miniprofiler.com/dotnet/)**.

For both ASP.NET Core and OWIN, you can use the `MiniProfilerWidget`, which is a DotVVM control with many options like `MaxTraces`, `Position`, `StartHidden` etc.:

```DOTHTML
    <dot:MiniProfilerWidget Position="Right" ShowTrivial="true" StartHidden="true" />
```

You can easily profile your code. It can look like this (check [Profile Code](https://miniprofiler.com/dotnet/HowTo/ProfileCode) section in MiniProfiler documentation):
```CSHARP
using (MiniProfiler.Current.Step("GetOrder"))
{
    return orderRepository.Get();
}
```



### <a name="AspNetCore"></a>ASP.NET Core

1. Run the following commands in the _Package Manager Console_ window:

```
Install-Package MiniProfiler.AspNetCore.Mvc
Install-Package DotVVM.Tracing.MiniProfiler.AspNetCore
```

2. Next, you can register the MiniProfiler integration into DotVVM request tracing in the `IDotvvmServiceConfigurator` this way:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    options.AddMiniProfilerEventTracing();
}
```

3. As the last step, you need to add the MiniProfiler services in the `ConfigureServices` method and the Miniprofiler middleware before `app.UseDotVVM<DotvvmStartup>()`:

```CSHARP
public void ConfigureServices(IServiceCollection services)
{
    ...

    services.AddMemoryCache();

    services.AddMiniProfiler(options =>
    {
        options.RouteBasePath = "/profiler";
    });

    ...
}

public void Configure(IApplicationBuilder app)
{
    ...

    app.UseMiniProfiler();

    app.UseDotVVM<DotvvmStartup>();

    ...
}
```

To see it in action, you can simply navigate to `~/profiler/results-index` and view profiled HTTP requests.

> We have a [sample application](https://github.com/riganti/dotvvm-tracing/tree/master/samples/DotVVM.Samples.MiniProfiler.AspNetCore) to show how MiniProfiler can be used with **ASP.NET Core**.



### <a name="Owin"></a>OWIN

1. Either use the NuGet UI to install `DotVVM.Tracing.MiniProfiler.Owin`, or use the following commands in the _Package Manager Console_ window:

```
Install-Package DotVVM.Tracing.MiniProfiler.Owin
```

2. Next, you can register the MiniProfiler integration into DotVVM request tracing in the `IDotvvmServiceConfigurator` this way:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    options.AddMiniProfilerEventTracing();
}
```

To see it in action, you can simply navigate to `~/mini-profiler-resources/results-index` and view profiled HTTP requests.

You can change MiniProfiler default setting as you would do without DotVVM integration, for example:

```CSHARP
StackExchange.Profiling.MiniProfiler.Settings.RouteBasePath = "~/profiler";
StackExchange.Profiling.MiniProfiler.Settings.Results_List_Authorize = (r) => true;
```

> We have a [sample application](https://github.com/riganti/dotvvm-tracing/tree/master/samples/DotVVM.Samples.MiniProfiler.Owin) to show how MiniProfiler can be used with **Owin**. 
