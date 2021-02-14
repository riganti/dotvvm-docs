# Single Page Applications (SPA)

DotVVM supports single page applications (SPA) with minimum effort. The SPAs integrate with the [master pages](/docs/tutorials/basics-master-pages/{branch}) mechanism pretty well.

You just need to replace the [ContentPlaceHolder](/docs/controls/builtin/ContentPlaceHolder/{branch}) with the [SpaContentPlaceHolder][2].

The content pages load asynchronously. Navigation between pages uses [History API](https://developer.mozilla.org/en-US/docs/Web/API/History_API).

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

There can be only one [SpaContentPlaceHolder][2] in the page. Otherwise, the framework wouldn't be able to remember
the URL of currently loaded page in the URL.

## Migrating from version 1.x

In previous versions, DotVVM has been using URL fragments (the part of URL after '#') to store the current content page (eg. http://myapp.local/#/ContentPage1).
Since DotVVM 2.0 this is done using [History API][1].

There are quite a few advantages:

* Navigation between pages works even without JavaScript (correct URL is rendered into href attribute)
* Navigating from blank page to SPA URL will trigger only one HTTP request instead of two
* User can refresh currently loaded page by clicking at a link pointing at the current page (this was not possible with URL fragments)
* User gets to see prettier URLs

The new navigation mode utilizing [History API][1] is enabled by default.
The *old* URLs (eg. http://myapp.local/#/ContentPage1) will still work, but will "redirected" to the new [History API][1].

If you still want to use the old navigation mode, add this line to DotVVM configuration:

```CSHARP
config.UseHistoryApiSpaNavigation = false;
```

Or if you want to configure only single [SpaContentPlaceHolder][2] to use the old navigation mode, set the `UseHistoryApi` parameter:

```DOTHTML
<dot:SpaContentPlaceHolder ID="placeholder-id" UseHistoryApi="false"/>
```

[1]: https://developer.mozilla.org/en-US/docs/Web/API/History_API
[2]:/docs/controls/builtin/SpaContentPlaceHolder/{branch}