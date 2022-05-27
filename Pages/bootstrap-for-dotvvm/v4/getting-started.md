# Getting started with Bootstrap 4 for DotVVM

To use the [Bootstrap for DotVVM](https://www.dotvvm.com/products/bootstrap-for-dotvvm) controls, you have use the [DotVVM Private Nuget Feed](~/pages/dotvvm-for-visual-studio/dotvvm-private-nuget-feed).

1. Install the `DotVVM.Controls.Bootstrap4` package from the DotVVM Private Nuget Feed.

2. Open your `DotvvmStartup.cs` file and add the following line at the beginning of the `Configure` method.

```CSHARP
config.AddBootstrap4Configuration();
``` 

You might need to add the following `using` at the beginning of the file.

```CSHARP
using DotVVM.Framework.Controls.Bootstrap4;
```

This will register all Bootstrap controls under the `<bs:*` tag prefix, and it also registers several Bootstrap resources. 



## Configuration

The [Bootstrap for DotVVM](https://www.dotvvm.com/products/bootstrap-for-dotvvm) package includes Bootstrap CSS and JavaScript libraries. You can replace them with your own ones.
 
If you have already included the Bootstrap script and styles using the `<script>` and `<style>` elements in the page header (e.g. in the master page), you can tell 
DotVVM that it should not render the default Bootstrap resources. Add this in the `DotvvmStartup.cs`:

```CSHARP
config.AddBootstrap4Configuration(new DotvvmBootstrapOptions() 
{
    IncludeBootstrapResourcesInPage = false
});
```

Optionally, you can also tell DotVVM not to include jQuery. 

```CSHARP
config.AddBootstrap4Configuration(new DotvvmBootstrapOptions() 
{
    IncludeJQueryResourceInPage = false
});
```

Or specify your own IResources.
```CSHARP
            var options = new DotvvmBootstrapOptions()
            {
                BootstrapJsResource = new ScriptResource(new UrlResourceLocation("PATH TO CUSTOM BOOTSTRAP.JS")),
                BootstrapCssResource = new StylesheetResource(new UrlResourceLocation("PATH OT CUSTOM BOOTSTRAP.CSS")),
                JQueryResource = new ScriptResource(new UrlResourceLocation("PATH TO CUSTOM JQUERY.JS")),
                IncludeBootstrapResourcesInPage = true,
                IncludeJQueryResourceInPage = true
            };

            //var options = DotvvmBootstrapOptions.CreateDefaultSettings();
            //options.BootstrapJsResource = new ScriptResource(new UrlResourceLocation("PATH TO CUSTOM BOOTSTRAP.JS"));


            // bootstrap configuration
            config.AddBootstrap4Configuration(options);
```

## Usage
The usage of each control is shown in the [controls documentation](~/controls/bootstrap4/Accordion).
