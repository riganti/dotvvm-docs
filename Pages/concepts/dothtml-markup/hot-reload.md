# Hot reload

When running **in the development mode**, DotVVM watches markup files for changes, and recompiles them without the need to restart the web application process.

Additionally, there are `DotVVM.HotReload.AspNetCore` and `DotVVM.HotReload.Owin` packages that will inject a simple JavaScript in the page that will reload the page automatically while preserving the viewmodel state.

Thus, when working on your DotVVM pages, you can just run the DotVVM application once and edit the markup files in your IDE. When you save the file, the page loaded in the browser will reflect the changes immediately.

## Configuration

When you create a DotVVM application, Hot reload is already configured. To add it manually, you need to perform the following steps:

# [ASP.NET Core](#tab/aspnetcore)

1. Install `DotVVM.HotReload.AspNetCore` NuGet package.

1. In `Program.cs` (or `Startup.cs` file), add the following line after the call to `UseDotVVM`:

    ```CSHARP
    app.UseDotvvmHotReload();
    ```

1. In the `ConfigureServices` in `DotvvmStartup.cs`, add the following line:

    ```CSHARP
    public void ConfigureServices(IDotvvmServiceCollection services)
    {
        services.AddHotReload();
        ...
    }
    ```

1. Ensure you run the application with `ASPNETCORE_ENVIRONMENT=Development`. You can check this in `Properties/launchSettings.json`, or you can see it in the console output of the application. See the [Debug mode](~/pages/concepts/configuration/overview?tabs=aspnetcore#debug-mode) section for more details.

# [OWIN](#tab/owin)

1. Install `DotVVM.HotReload.Owin` NuGet package.

1. In `Startup.cs` file, add the following line after the call to `UseDotVVM`:

    ```CSHARP
    app.UseDotvvmHotReload();
    ```

1. In the `ConfigureServices` in `DotvvmStartup.cs`, add the following line:

    ```CSHARP
    public void ConfigureServices(IDotvvmServiceCollection services)
    {
        services.AddHotReload();
        ...
    }
    ```

1. Ensure you run the application in the debug mode. See the [Debug mode](~/pages/concepts/configuration/overview?tabs=aspnetcore#debug-mode) section for more details.

***

## See also

* [DotHTML markup](~/pages/concepts/dothtml-markup/overview)
* [Common control properties](~/pages/concepts/dothtml-markup/common-control-properties)
* [Data-binding](~/pages/concepts/data-binding/overview)
* [Compilation test](~/pages/concepts/dothtml-markup/compilation-test)

