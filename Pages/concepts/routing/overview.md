# Routing

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
Because the file doesn|t have to be in the **Views** folder, you need to pass an application relative path including the `Views` folder name: `Views/page.dothtml`.

+ The fourth argument (optional) specifies **default values for route parameters**. If the parameter value is not specified in the URL, the value from this object will be used.
You can pass an anonymous object with property names that correspond with the route parameter names, or `IDictionary<string, object>`. 

If you need to register a route which should not be treated as `.dothtml` file, e.g. if you need a handler that serves files, generates RSS feeds or anything like that, you can
declare a [custom presenter](/docs/tutorials/advanced-custom-presenters/{branch}) and specify a method, that creates an instance of it, as the fifth parameter.

The presenters can also be used in localized applications to detect the user language preferences and switching the culture of the HTTP request. See [Localization Presenter](/docs/tutorials/advanced-localization-presenter/{branch}) for more information.

> If you have a larger project, you may want to use conventions to [auto-discover routes](/docs/tutorials/advanced-route-autodiscovery/{branch}) instead of registering them one by one.  

## Route Parameters

Consider the following example. We need to create a page that can create a new customer, or display its details and allow to modify them.

In the first case, the URL is `admin/customer`. There is no customer ID, because we are creating a new one. 

In the second case, the URL contains the ID of the customer we want to edit, e.g. `admin/customer/3`. 

We can cover both cases using a single route. Its registration will look like this:

```CSHARP
config.RouteTable.Add("AdminCustomer", "admin/customer/{Id}", "Views/admin/customer.dothtml", new { Id = 0 });
```

Notice that the route contains the parameter `{Id}`. Also, the fourth argument says that if the parameter is not present, its value should be `0`. 

In the viewmodel of the page, we can then access the value of the route parameter using the following code:

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

Alternatively we can instruct DotVVM to inject route parameter value directly into viewmodel property by using `FromRoute` or `FromQuery` attribute.

```CSHARP
 [FromRoute("Id")]
 public int CustomerId { get; set; }
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

## Route Groups

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


<a id="ref-constraints"></a>

## Route Constraints

Additionally, we can use the route parameter constraints:

```CSHARP
config.RouteTable.Add("AdminCustomer", "admin/customer/{Id:int}", "Views/admin/customer.dothtml", new { Id = 0 });
```

Notice the `{Id:int}` parameter, which says that the route will be matched only if the `Id` is an integer value.

**DotVVM** supports the following route constraints:

* `alpha` - alphabetic characters
* `bool` - true / false value
* `decimal` - decimal values with `.` decimal point symbol
* `double` - double values with `.` decimal point symbol
* `float` - float values with `.` decimal point symbol
* `guid` - a Guid value
* `int` - any integer number
* `posint`- a positive integer number (or zero)
* `length(x)` - any value with length of `x` characters
* `long` - any long integer number
* `max(x)` - any double value with `.` decimal point symbol that is less than `x`
* `min(x)` - any double value with `.` decimal point symbol that is greater than `x`
* `range(x, y)` - any double value with `.` decimal point symbol that is between `x` and `y`
* `maxlength(x)` - any value with length of `x` characters or less
* `minlength(x)` - any value with length of `x` characters or more
* `regex(x)` - any value that matches the regular expression `x`

You can also define your own route constraints by creating a class that implements the `IRouteParameterConstraint` interface. 
These custom constraints can be registered using this syntax in the `DotvvmStartup` file:

```CSHARP
config.RouteConstraints.Add("customConstraint", new MyRouteConstraint());
```
 
