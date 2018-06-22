# Installing Bootstrap for DotVVM

To use the [Bootstrap for DotVVM](/landing/bootstrap-for-dotvvm) controls, you have use the [DotVVM Private Nuget Feed](/docs/tutorials/commercial-dotvvm-private-nuget-feed).

1. Install the package from the DotVVM Private Nuget Feed.

2. Open your `DotvvmStartup.cs` file and add the following line at the beginning of the `Configure` method.

```CSHARP
config.AddBootstrapConfiguration();
``` 

You might need to add the following `using` at the beginning of the file.

```CSHARP
using DotVVM.Framework.Controls.Bootstrap;
```

This will register all Bootstrap controls under the `<bs:*` tag prefix, and it also registers several Bootstrap resources. 



## Configuration

The [Bootstrap for DotVVM](/landing/bootstrap-for-dotvvm) package doesn't include the bootstrap CSS and javascript libraries as they are too large and you might have 
your own compilation of Bootstrap (e.g. if you use some Bootstrap template).   

There are two ways how to work with Bootstrap resources.

**Option 1**: In most cases you want DotVVM to manage the resources for you. You don't need to include Bootstrap yourself in the page, you just let DotVVM do this for you.
Use this option if you don't reference the bootstrap CSS and JS files using the `<script>` or `<link>` tags in the page.

**Option 2**: Alternatively, you can turn the resource management for Bootstrap files off. This way is better e.g. if you use  some Bootstrap template which already includes 
Bootstrap scripts or styles in the page. If you turn the resource management off, DotVVM will assume that Bootstrap is already correctly included in the page.   


### Option 1: Let DotVVM Include the Bootstrap Files in the Page
**Step 1**: If you don't have Bootstrap scripts and styles in your project,

#### for .NET Framework
install the `Bootstrap` version 3 package from the official Nuget feed.
```
Install-Package Bootstrap -Version 3.3.7 
```
#### for .NET Core
install the `Bootstrap` package from the Bower.
```
bower install bootstrap
```
Default installation location is in `wwwroot\lib\bootstrap`. You have to change the default paths in `AddBootstrapConfiguration`, see below

**Step 2**: **Bootstrap for DotVVM** assumes the Bootstrap JS and CSS files are on the following path in your project:

* `/Content/bootstrap.min.css`
* `/Scripts/bootstrap.min.js`

If they are not, you can modify the default paths. In the `DotvvmStartup.cs` file, update the `AddBootstrapConfiguration` call this way::

```CSHARP
config.AddBootstrapConfiguration(new DotvvmBootstrapOptions() 
{
    BootstrapCssUrl = "your path to bootstrap.css or bootstrap.min.css",
    BootstrapJsUrl = "your path to bootstrap.js or bootstrap.min.js"
});
```

Please note that **DotVVM** includes jQuery in the page automatically because it is required by Bootstrap. You can change the path to jQuery using the following code:

```CSHARP
config.AddBootstrapConfiguration(new DotvvmBootstrapOptions() 
{
    ...
    JQueryUrl = "your path to jquery.x.x.x.js or jquery.x.x.x.min.js"
});
```
 


### Option 2: Include the Bootstrap Files in the Page Yourself

If you have already included the bootstrap script and styles using the `<script>` and `<style>` elements in the page header (e.g. in the master page), you can tell 
DotVVM that it should not render the default bootstrap resources. Add this in the master page:

```CSHARP
config.AddBootstrapConfiguration(new DotvvmBootstrapOptions() 
{
    IncludeBootstrapResourcesInPage = false
});
```

Optionally, you can also tell DotVVM not to include jQuery. 

```CSHARP
config.AddBootstrapConfiguration(new DotvvmBootstrapOptions() 
{
    IncludeJQueryResourceInPage = false
});
```



### Is It OK?

To verify that Bootstrap resources are included correctly, press F12 in your web browser. Verify that bootstrap.css or bootstrap.js is not loaded twice, and there are 
no errors in the developer console, especially some messages which say that some Bootstrap-related resource could not be found.





## Limitations

The current version of [Bootstrap for DotVVM](/landing/bootstrap-for-dotvvm) supports **Bootstrap 3**. For more information about Bootstrap, navigate into its [documentation](https://getbootstrap.com).
