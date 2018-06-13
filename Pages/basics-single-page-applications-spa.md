# Single Page Applications (SPA)

DotVVM supports single page applications (SPA) with minimum effort. The SPAs integrate with the [master pages](/docs/tutorials/basics-master-pages/{branch}) mechanism pretty well.

You just need to replace the [ContentPlaceHolder](/docs/controls/builtin/ContentPlaceHolder/{branch}) with the 
[SpaContentPlaceHolder](/docs/controls/builtin/SpaContentPlaceHolder/{branch}). 

The content pages will then load asynchronously and the URL of the current page will be persisted using the URL fragment (e.g. http://myapp.local/#/ContentPage1).

To navigate between the pages in the SPA, we recommend to use the [RouteLink](/docs/controls/builtin/RouteLink/{branch}) control. It composes the correct URLs
with support of route parameters. Actually, we recommend to use the [RouteLink](/docs/controls/builtin/RouteLink/{branch})s everywhere, even if you are not using SPAs. You can always change the URLs for individual routes without the need to modify dozens of pages in your application. 

## Using RouteLinks

Let's have the following route registrations in the `DotvvmStartup.cs` file:

```CSHARP
config.RouteTable.Add("ArticleDetail", "Article/{Id}/{Title}", "article.dothtml");
```

The `RouteLink` control is used this way:

```DOTHTML
<dot:RouteLink RouteName="ArticleDetail" Param-Id="{value: CurrentArticleId}" Param-Title="{value: CurrentArticleTitle}" />
```

The route parameters can be specified using properties starting with `Param-`. These won't appear in the page HTML, but they will used to compose the final URL.

If the parameter is not specified here and the current page has a parameter with the same name, the value from the current page will be used. 
If the current page doesn't have this parameter, the default value from the route is used. If it is not specified, an empty string will be substituted for this parameter.

In order to redirect to another page from the viewmodel command, you can call `Context.RedirectToUrl("url")` or 
`Context.RedirectToRoute("routeName", new { Param1 = param... })`. 

It will generate a correct URL, no matter whether you run inside SPA or not.

## Restrictions

There can be only one [SpaContentPlaceHolder](/docs/controls/builtin/SpaContentPlaceHolder/{branch}) in the page. Otherwise, the framework wouldn't be able to remember
the URL of currently loaded page in the URL fragment.