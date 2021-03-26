# SPA (Single-page Apps)

DotVVM supports single page applications (SPA) with minimum effort. The SPAs integrate with the [master pages](/docs/tutorials/basics-master-pages/{branch}) mechanism pretty well. Content pages load asynchronously.

You just need to replace the [ContentPlaceHolder](/docs/controls/builtin/ContentPlaceHolder/{branch}) with the [SpaContentPlaceHolder][1].

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

## Using multiple SPAs on a page

Previously, there was a restriction that users could define only a single [SpaContentPlaceHolder][1] per page. Since version 3.0 this restriction has been lifted and it is now possible to use multiple SPAs per page.

[1]:/docs/controls/builtin/SpaContentPlaceHolder/{branch}