# Application Insights

[Application Insights](https://azure.microsoft.com/en-us/services/application-insights/) is a service for web developers which helps to monitor a live web application. It automatically collects issues and telemetry of the application and sends them to the Microsoft Azure portal where the information can be analyzed.

**DotVVM** supports this service and tracks some additional telemetry collected during the processing of a HTTP request.

## Create the Application Insights resource

In order to view telemetry data in Microsoft Azure portal you need to create a Microsoft Azure resource. This can be done either through
IDE or manually.

> The process of creating a new Microsoft Azure resource is described in
[Create an Application Insights resource](https://docs.microsoft.com/en-us/azure/application-insights/app-insights-create-new-resource) page.

After the Application Insights resource is created, you will get the **Instrumentation Key** which will be placed in the configuration file.

## Configure Application Insights

# [ASP.NET Core](#tab/aspnetcore)

You can add *Application Insights* to an ASP.NET Core project using the _Visual Studio Solution Explorer_ window, or manually.

> Follow this guide: [Application Insights for ASP.NET Core](https://docs.microsoft.com/en-us/azure/application-insights/app-insights-asp-net-core) to add Application Insights in Visual Studio Solution Explorer.

To install Application Insights in your ASP.NET Core project manually, you need to add `Microsoft.ApplicationInsights.AspNetCore` NuGet package dependency. 

In order to see the telemetry data in Microsoft Azure resource, you need to add the Instrumentation key to the `appsettings.json` file. You can find this key in the Overview tab of your Microsoft Azure resource. After that, you need to add instrumentation code to the `Startup.cs` class in you project.

> For more information about the Application Insights configuration in ASP.NET Core project see the
[Getting Started with Application Insights for ASP.NET Core](https://github.com/Microsoft/ApplicationInsights-aspnetcore/wiki/Getting-Started-with-Application-Insights-for-ASP.NET-Core) page.

After the Application Insights are installed, run the following command in the _Package Manager Console_ window:

```
Install-Package DotVVM.Tracing.ApplicationInsights.AspNetCore
```

Then, register the DotVVM tracking reporter in the `DotvvmStartup.cs` this way:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    options.AddApplicationInsightsTracing();
}
```

# [OWIN](#tab/owin)

First, you need to install *Application Insights* into your ASP.NET project. You can do this in the _Visual Studio Solution Explorer_ window.

> Follow this guide: [Set up Application Insights for your ASP.NET website](https://docs.microsoft.com/en-us/azure/application-insights/app-insights-asp-net) to add Applitaction Insights into your project.

After the Application Insights are installed, run the following command in the _Package Manager Console_ window:

```
Install-Package ApplicationInsights.OwinExtensions
```

Then, follow the steps from the [Application Insights OWIN extensions documentation](https://github.com/marcinbudny/applicationinsights-owinextensions) to make the Application Insights tracking working.

After you have updated the `ApplicationInsights.config` file, run the following command in the _Package Manager Console_ window:

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

---

## Collect the client-side telemetry

In both ASP.NET Core and OWIN version, you can use the `ApplicationInsightsJavascript` control, which renders the Application Insight JavaScript snippet to do the tracing on the client-side. 

According to the [official documentation](https://docs.microsoft.com/en-us/azure/application-insights/app-insights-javascript), it should be placed just before `</head>` tag:

```HTML
<head>
   ...
   <dot:ApplicationInsightsJavascript />
</head>
```

The OWIN version of the control contains the `EnableAuthSnippet` property which can collect [identifiers of authenticated users](https://github.com/Microsoft/ApplicationInsights-JS/blob/master/API-reference.md#setauthenticatedusercontext). 

In ASP.NET Core web apps, this value is taken from `ApplicationInsightsServiceOptions` configuration object.

## View telemetry data in Azure portal

You can display the collected DotVVM telemetry by editing any chart in the _Server_ request tab. You can also choose which metrics you want to track in the metrics tab under _Custom_ drop down menu. You can also aggregate the data.

![DotVVM metrics in Microsoft Azure portal](application-insights_img1.png)

It is also possible to filter this data by the name of the operation:

![Filter metrics in Microsoft Azure portal](application-insights_img2.png)

Additionally, you can analyze exceptions that were thrown in your web application:

![Exceptions in Microsoft Azure portal](application-insights_img3.png)

## See also

* [MiniProfiler](miniprofiler)
* [Custom tracing](custom-tracing)