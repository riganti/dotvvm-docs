# Application Insights

> This feature is available in DotVVM 1.1.5 and higher.

[Application Insights](https://azure.microsoft.com/en-us/services/application-insights/) is a service for web developers which helps to monitor a live web application. It automatically collects issues and telemetry of the application and sends them to the Microsoft Azure portal where the information can be analyzed.

**DotVVM** supports this service and tracks some additional telemetry which it collects during the processing of a HTTP request.

<img src="{imageDir}advanced-application-insights_img1.png" alt="DotVVM metrics in Microsoft Azure portal" class="img-responsive" />

You can display the collected **DotVVM** telemetry by editing any chart in Server request tab. You can also choose which metrics you want to track in the metrics tab under Custom drop down menu. You can also aggregate the data.

It is also possible to filter this data by the name of the operation:

<img src="{imageDir}advanced-application-insights_img2.png" alt="Filter metrics in Microsoft Azure portal" class="img-responsive" />

Additionally, you can analyze exceptions that were thrown in your web application:

<img src="{imageDir}advanced-application-insights_img3.png" alt="Exceptions in Microsoft Azure portal" class="img-responsive" />



## Getting started

In order to view telemetry data in Microsoft Azure portal you need to create a Microsoft Azure resource. This can be done either through
IDE or manually.

>The process of creating a new Microsoft Azure resource is described in
[Create an Application Insights resource](https://docs.microsoft.com/en-us/azure/application-insights/app-insights-create-new-resource) page.

In both ASP.NET Core and OWIN, you can use the `ApplicationInsightsJavascript` control, which renders the Application Insight javascript snippet to do the tracing on client side. According to the [official documentation](https://docs.microsoft.com/en-us/azure/application-insights/app-insights-javascript), it should be placed just before `</head>` tag:
```HTML
<dot:ApplicationInsightsJavascript />
```
OWIN version additionally contains the `EnableAuthSnippet` property. In ASP.NET Core web apps, this value is taken from `ApplicationInsightsServiceOptions`.



### ASP.NET Core

You can add *Application Insights* to an ASP.NET Core project using the Visual Studio Solution Explorer window, or manually.

>Follow this guide: [Application Insights for ASP.NET Core](https://docs.microsoft.com/en-us/azure/application-insights/app-insights-asp-net-core) to add Application Insights in Visual Studio Solution Explorer.

To install Application Insights in your ASP.NET Core project manually, you need to add `Microsoft.ApplicationInsights.AspNetCore` NuGet package dependency. In order to see the telemetry data in Microsoft Azure resource, you need to add the Instrumentation key to the `appsettings.json` file. You can find this key in the Overview tab of your Microsoft Azure resource. After that, you need to add instrumentation code to the `Startup.cs` class in you project.

>To access more information about the Application Insights configuration in ASP.NET Core project see the
[Getting Started with Application Insights for ASP.NET Core](https://github.com/Microsoft/ApplicationInsights-aspnetcore/wiki/Getting-Started-with-Application-Insights-for-ASP.NET-Core) page.

Now run the following command in the Package Manager Console window:

```
Install-Package DotVVM.Tracing.ApplicationInsights.AspNetCore
```

And finally, register the DotVVM tracking reporter in the `IDotvvmServiceConfigurator` this way:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    options.AddApplicationInsightsTracing();
}
```



### OWIN

First, you need to add Application Insights into your ASP.NET project. You can do this in the Visual Studio Solution Explorer.

>Follow this guide: [Set up Application Insights for your ASP.NET website](https://docs.microsoft.com/en-us/azure/application-insights/app-insights-asp-net) to add Applitaction Insights into your project.

After that, run the following command in the Package Manager Console:

```
Install-Package ApplicationInsights.OwinExtensions
```

Then, follow the steps from the [Application Insights OWIN extensions documentation](https://github.com/marcinbudny/applicationinsights-owinextensions) to make the Application Insights tracking working.

After you have updated the `ApplicationInsights.config` file, run the following command in the Package Manager Console window:

```
Install-Package DotVVM.Tracing.ApplicationInsights.Owin
```

And finally, register the DotVVM tracking reporter in the `IDotvvmServiceConfigurator` this way:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    options.AddApplicationInsightsTracing();
}
```

Thanks to this, you will be able to see more detailed metrics for DotVVM requests.
