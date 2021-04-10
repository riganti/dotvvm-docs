# Auto-discover routes

If your app is large, you don't want to register your routes one by one. Also, the names and hierarchy of pages in your app typically correspond 
with the URLs you want to use. In this case, you can use the auto-discovery mechanism.

Consider the following files in the project and the URLs you want to map:

View            | URL
----------------|-------------------
`Views/home.dothtml` | `www.mydomain.com/home`
`Views/contact-us.dothtml` | `www.mydomain.com/contact-us`
`Views/login.dothtml` | `www.mydomain.com/login`
`Views/about.dothtml` | `www.mydomain.com/about`
`Views/client-area/profile.dothtml` | `www.mydomain.com/client-area/profile`

In this case, the auto-discovery can be registered like this:

```CSHARP
config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));
```

The `DefaultRouteStrategy` will browse the `Views` folder and look for all `.dothtml` files (including the subfolders). When it finds a file, it generates
the route name and URL from the relative path of the file within the `Views` folder.

## Build a custom strategy

If you need to do some changes to the default strategy, you can create your own class and override one of the following methods: `GetRouteName`, `GetRouteUrl`
and `GetRouteDefaultParameters`. Each of these methods is called for every discovered `.dothtml` file before the route is registered.

```CSHARP
public class MyRouteStrategy : DefaultRouteStrategy
{

    protected override string GetRouteUrl(RouteStrategyMarkupFileInfo file)
    {
        var url = base.GetRouteUrl(file);
        if (url == "contact-us") 
        {
            // the contact-us route should have a parameter
            return url + "/{ContactReason}";
        }
        else if (url == "home") 
        {
            // instead of /home, we need the route to be directly in the website root /
            return "";
        }
    }

}
```

One of the interesting scenarios is building your own naming conventions. For example, if the page name ends with `List`, it won't have any route parameters. If it ends with `Detail`, it will have one route parameter called `id`.

```CSHARP
public class MyRouteStrategy : DefaultRouteStrategy
{

    protected override string GetRouteUrl(RouteStrategyMarkupFileInfo file)
    {
        var url = base.GetRouteUrl(file);
        if (url.EndsWith("detail", StringComparison.CurrentCultureIgnoreCase)) 
        {
            // add "id" parameter to the route
            return url + "/{id}";
        }
    }

}
```

## Order of registration

The auto-discovery strategy ignores route names which are already registered. If you want to create exceptions for some pages, register them **before** calling `AutoDiscoverRoutes`. 

If you use the same route name that corresponds to the relative path (e. g. `Admin_CustomerList` for `Admin/CustomerList.dothtml` page), the strategy will ignore this route.

## See also

* [Route redirection](~/pages/concepts/routing/route-redirection)
* [Custom presenter](~/pages/concepts/routing/custom-presenters)