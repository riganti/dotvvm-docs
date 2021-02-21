# Route parameters

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

## Constraints

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

Every parameter can specify only one constraint.

## Custom contraints

You can also define your own route constraints by creating a class that implements the `IRouteParameterConstraint` interface. 
These custom constraints can be registered using this syntax in the `DotvvmStartup` file:

```CSHARP
config.RouteConstraints.Add("customConstraint", new MyRouteConstraint());
```

