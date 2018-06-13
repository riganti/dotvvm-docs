# Routing

Every page in **DotVVM** needs to be registered in the **route table**. DotVVM doesn't allow to visit the page by just putting its path in the URL. 

The routes are configured in the `DotvvmStartup` class in the `Configure` method.

> To separate the configuration options, the default project templates contain the `ConfigureControls`, `ConfigureRoutes` and the `ConfigureResources`, which are called from the `Configure` methods. But you can use any structure you like, the only requirement is that the `Configure` method performs all the configuration actions.


## Registering Routes One By One

In simple web apps, you can register each route individually using the following code snippet:

```CSHARP
config.RouteTable.Add("Page", "my/page/url", "Views/page.dothtml", new { });
```

+ The first argument is the **name of the route**. You'll need it when you do redirects or generate a hyperlink that navigates the user to this page. This name is not displayed anywhere, it is only a string constant which identifies the route in your code.

+ The second argument is the **route URL**. It can contain route parameters (e.g. `"product-detail/{ProductId}"`) which you can retrieve in the viewmodel when the page is loaded. For default page, you can use the `""` as the route URL. 

+ The third argument is **the location of the `.dothtml` file** which will be used to handle the request.
Because the file is in the **Views** folder, we must pass the app-relative path - `Views/page.dothtml`.

+ The fourth argument (optional) specifies **default values for route parameters**. If the parameter value is not specified in the URL, the value from this object will be used.
You can pass an anonymous object with property names that correspond with the route parameter names, or you can pass the `IDictionary<string, object>`. 

If you need to register a route which should not be treated as `.dothtml` file, e.g. if you need a handler that serves files, generates RSS feeds or anything like that, you can
declare a [custom presenter](/docs/tutorials/advanced-custom-presenters/{branch}) and specify a method, that creates an instance of it, as the fifth parameter.

> If you have a larger project, you may want to use conventions to [auto-discover routes](/docs/tutorials/advanced-route-autodiscovery/{branch}) instead of registering them one by one. 

## Route Parameters

Consider the following example. We need to create a page that can create a new customer, or display its details and allow to modify them.

In the first case, the URL is `admin/customer`. There is no customer ID, because we are creating a new one. 

In the second case, the URL contains the ID of the customer we want to edit, e.g. `admin/customer/3`. 

We can cover both cases using a single route. Its registration will look like this:

```CSHARP
config.RouteTable.Add("AdminCustomer", "admin/customer/{Id}", "Views/admin/customer.dothtml", new { Id = 0 });
```

Notice that the route contains the parameter `{Id}`. Also, the fourth argument says that if the parameter is not present, its value is `0`. 

In the viewmodel of the page, we can then access the value of the route parameter using the followin code:

```CSHARP
var customerId = Convert.ToInt32(Context.Parameters["Id"]);
if (customerId == 0) 
{
    // new customer
} 
else 
{
    // existing customer
}
```

We can also make the parameter optional (without the default value):

```CSHARP
config.RouteTable.Add("AdminCustomer", "admin/customer/{Id?}", "Views/admin/customer.dothtml");
```

In that case, we need to check, whether the parameter is present in the URL:

```CSHARP
if (!Context.Parameters.ContainsKey("Id")) 
{
    // new customer
}
else 
{
    // existing customer
    var customerId = Convert.ToInt32(Context.Parameters["Id"]);
}
```

Finally, we can use the route parameter constraints:

```CSHARP
config.RouteTable.Add("AdminCustomer", "admin/customer/{Id:int}", "Views/admin/customer.dothtml", new { Id = 0 });
```

Notice the `{Id:int}` parameter, which says that the route will be matched only if the `Id` is an integer value.

<a id="ref-constraints"></a>

**DotVVM 1.0** supports the following route constraints:

* `int` - any integer number
* `posint`- a positive integer number (or zero)
* `float` - anything that can be parsed by `float.Parse` 
* `guid` - a Guid value

> DotVVM 1.1 supports [more route contraints](/docs/tutorials/basics-routing/1-1#ref-constraints). 
 
