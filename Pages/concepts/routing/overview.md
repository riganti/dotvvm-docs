# Routing overview

Every page in **DotVVM** needs to be registered in the **route table**. DotVVM doesn't allow visiting a page just by putting its path in the URL. 

The routes are configured as part of the [configuration](~/pages/concepts/configuration/overview) in the `DotvvmStartup` class.

> To separate the configuration options, the default project templates contain `ConfigureControls`, `ConfigureRoutes` and `ConfigureResources` methods, which are called from the `Configure` method. You can use any structure you like - the only requirement is that the `Configure` method performs all the configuration actions.

## Register routes one by one

In simple web applications, you can register each route individually using the following code snippet:

```CSHARP
config.RouteTable.Add("Page", "my/page/url", "Views/page.dothtml", new { });
```

+ The first argument is the **name of the route**. You'll need it when you do redirects, or generate a hyperlink that navigates the user to this page (e. g. using the [RouteLink](~/controls/builtin/RouteLink) control). This name is not displayed to the user, it is only a string which identifies the route in the application code.

+ The second argument is the **route URL**. It can contain route parameters (e.g. `"product-detail/{ProductId}"`) which you can retrieve in the viewmodel when the page is loaded. For the default page, you can use `""` as the route URL. 

+ The third argument is **the location of the `.dothtml` file** which will be used to handle the request.
Because the file doesn't have to be in the **Views** folder, you need to pass an application relative path including the `Views` folder name: `Views/page.dothtml`.

+ The fourth argument (optional) specifies **default values for [route parameters](parameters)**. If the parameter value is not specified in the URL, the value from this object will be used.
You can pass an anonymous object with property names that correspond with the route parameter names, or `IDictionary<string, object>`. 

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

Route name | Route URL | Location
-----------|-----------|----------
`Admin_Customers` | `admin/customers` | `Views/Admin/Customers.dothtml`
`Admin_Customer` | `admin/customer/{id}` | `Views/Admin/Customer.dothtml`

As you can see, the route name, route URL, and `dothtml` file location are composed from the `AddGroup` method parameters and the parameters of the particular route. Notice that the `_` character is added between the group name and route name. Route URL and file location are treated like paths and joined by `/`.

## Other route types

If you need to register a route which should not be treated as `.dothtml` file, e.g. if you need a handler that serves files, generates RSS feeds or anything like that, you can declare a [custom presenter](~/pages/concepts/routing/custom-presenters) and specify a method, that creates an instance of it, as the fifth parameter.

If you change the URLs in your app, you can use [redirection routes](~/pages/concepts/routing/route-redirection) to preserve the old URLs.

> If you have a larger project, you may want to use conventions to [auto-discover routes](~/pages/concepts/routing/auto-discover-routes) instead of registering them one by one.

## See also

* [Parameters](~/pages/concepts/routing/parameters)
* [Route redirection](~/pages/concepts/routing/route-redirection)
* [Auto-discover routes](~/pages/concepts/routing/auto-discover-routes)
* [Custom presenter](~/pages/concepts/routing/custom-presenters)
