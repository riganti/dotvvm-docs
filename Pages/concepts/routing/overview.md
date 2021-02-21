# Routing overview

Every page in **DotVVM** needs to be registered in the **route table**. DotVVM doesn't allow visiting a page just by putting its path in the URL. 

The routes are configured in the `DotvvmStartup` class in the `Configure` method.

> To separate the configuration options, the default project templates contain  `ConfigureControls`, `ConfigureRoutes` and `ConfigureResources` methods, which are called from the `Configure` method. But you can use any structure you like, the only requirement is that the `Configure` method performs all the configuration actions.


## Registering Routes One By One

In simple web apps, you can register each route individually using the following code snippet:

```CSHARP
config.RouteTable.Add("Page", "my/page/url", "Views/page.dothtml", new { });
```

+ The first argument is the **name of the route**. You'll need it when you do redirects or generate a hyperlink that navigates the user to this page. This name is not displayed anywhere, it is only a string which identifies the route in your code.

+ The second argument is the **route URL**. It can contain route parameters (e.g. `"product-detail/{ProductId}"`) which you can retrieve in the viewmodel when the page is loaded. For the default page, you can use `""` as the route URL. 

+ The third argument is **the location of the `.dothtml` file** which will be used to handle the request.
Because the file doesn't have to be in the **Views** folder, you need to pass an application relative path including the `Views` folder name: `Views/page.dothtml`.

+ The fourth argument (optional) specifies **default values for [route parameters](parameters)**. If the parameter value is not specified in the URL, the value from this object will be used.
You can pass an anonymous object with property names that correspond with the route parameter names, or `IDictionary<string, object>`. 

If you need to register a route which should not be treated as `.dothtml` file, e.g. if you need a handler that serves files, generates RSS feeds or anything like that, you can
declare a [custom presenter](/docs/tutorials/advanced-custom-presenters/{branch}) and specify a method, that creates an instance of it, as the fifth parameter.

The presenters can also be used in localized applications to detect the user language preferences and switching the culture of the HTTP request. See [Localization Presenter](/docs/tutorials/advanced-localization-presenter/{branch}) for more information.

> If you have a larger project, you may want to use conventions to [auto-discover routes](/docs/tutorials/advanced-route-autodiscovery/{branch}) instead of registering them one by one.  

## Route groups

If you have several similar routes, you can register them as a group:

```CSHARP
config.RouteTable.AddGroup("Admin", "admin", "Views/Admin", table =>
{
    table.Add("Customers", "customers", "Customers.dothtml");
    table.Add("Customer", "customer/{id}", "Customer.dothtml");
});
```

The `AddGroup` method allows specify a common prefix for all the routes. The routes will be registered according to the following table:

<table class="table table-condensed" style="font-family: monospace">
    <tr>
        <th>Route Name</th>
        <th>Route URL</th>
        <th>Location</th>
    </tr>
    <tr>
        <td>Admin_Customers</td>
        <td>admin/customers</td>
        <td>Views/Admin/Customers.dothtml</td>
    </tr>
    <tr>
        <td>Admin_Customer</td>
        <td>admin/customer/{id}</td>
        <td>Views/Admin/Customer.dothtml</td>
    </tr>
</table>

As you can see, the route name, route URL and `dothtml` file location are composed from the `AddGroup` method parameters and the parameters of the particular route. Notice that the `_` character is added between the group name and route name. Route URL and file location are treated like paths and joined by `/`.
