# Auto-Discovery of Routes

If your app is large, you don't want to register your routes one by one. Also, the names and hierarchy of pages in your app typically correspond 
with the URLs you want to use. In this case, you can use the auto-discovery mechanism.

Consider the following files in the project and the URLs you want to map:

<table class="table table-condensed" style="font-family: monospace">
    <tr>
        <td>Views/home.dothtml</td>
        <td>www.mydomain.com/home</td>
    </tr>
    <tr>
        <td>Views/contact-us.dothtml</td>
        <td>www.mydomain.com/contact-us</td>
    </tr>
    <tr>
        <td>Views/login.dothtml</td>
        <td>www.mydomain.com/login</td>
    </tr>
    <tr>
        <td>Views/about.dothtml</td>
        <td>www.mydomain.com/about</td>
    </tr>
    <tr>
        <td>Views/client-area/profile.dothtml</td>
        <td>www.mydomain.com/client-area/profile</td>
    </tr>
</table>

In this case the auto-discovery will be registered like this:

```CSHARP
config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));
```

The `DefaultRouteStrategy` browses the `Views` folder and looks for all `.dothtml` files (including the subfolders). When it finds a file, it generates
the route name and URL from the relative path of the file in the `Views` folder.

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