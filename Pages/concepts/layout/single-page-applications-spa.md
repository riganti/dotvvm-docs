# SPA (Single-page applications)

DotVVM allows for building single page applications (SPA) with minimum effort. The SPAs integrate with the [master pages](/docs/tutorials/basics-master-pages/{branch}) mechanism.

To create a SPA, you just need to replace the [ContentPlaceHolder](/docs/controls/builtin/ContentPlaceHolder/{branch}) with the [SpaContentPlaceHolder](/docs/controls/builtin/SpaContentPlaceHolder/{branch}). When navigated to another page which uses the same master page, the content inside `SpaContentPlaceHolder` will be replaced without unloading the entire page.

To navigate between the pages in the SPA, we recommend to use the [RouteLink](/docs/controls/builtin/RouteLink/{branch}) control. It builds the correct URLs
with support of route parameters. Actually, we recommend to use the [RouteLink](/docs/controls/builtin/RouteLink/{branch})s everywhere, even if you are not using SPAs. You may want to change the URLs for individual routes without the need to modify dozens of pages in your application.

## Using RouteLink

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

In order to redirect to another page from the viewmodel code, you can call `Context.RedirectToUrl("url")` or
`Context.RedirectToRoute("routeName", new { Param1 = param... })`.

It will generate a correct URL, no matter whether you run inside SPA or not.

## Changes to SPAs in DotVVM 3.0

### Multiple `SpaContentPlaceHolder` controls

In previous versions of DotVVM, there could be only one `SpaContentPlaceHolder` control present in the page at the same time. Starting with DotVVM 3.0, this restriction has been lifted, and it is now possible to use multiple SPAs per page.

### Removed hashbang mode in favor of History API

In DotVVM 2.x, the default behavior of SPAs was using the hashbang format of the URL (e. g. `~/MySpaApp#!/Pages/Default`). When the user navigated to another page, the part of the URL after the URL fragment changed. It was possible to explicitly enable [History API](https://developer.mozilla.org/en-US/docs/Web/API/History_API) which was using the classic format of the URL which is not recognizeable from the non-SPA application. 

In DotVVM 3.0, the History API mode is the only option - the old way with hashbang URLs was removed from the framework. All requests pointing to the hashbang URL format will get redirected to the correct URLs, and History API will be used everywhere.

The following properties on `SpaContentPlaceHolder` were marked as obsolete:

* `DefaultRouteName`
* `PrefixRouteName`
* `UseHistoryApi`

## See also

* [Master pages](~/pages/concepts/layout/master-pages)
* [RouteLink](~/controls/builtin/RouteLink)
* [SpaContentPlaceHolder](~/controls/builtin/SpaContentPlaceHolder)