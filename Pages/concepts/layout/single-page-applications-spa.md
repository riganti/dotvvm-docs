# SPA (Single-page applications)

DotVVM allows for building single page applications (SPA) with minimum effort. The SPAs integrate with the [master pages](~/pages/concepts/layout/master-pages) mechanism.

To create a SPA, you just need to replace the [ContentPlaceHolder](~/controls/builtin/ContentPlaceHolder) with the [SpaContentPlaceHolder](~/controls/builtin/SpaContentPlaceHolder). 

When navigated to another page which uses the same master page, the content inside `SpaContentPlaceHolder` will be replaced without unloading the entire page. If the target page uses a different master page, a full navigation will be performed (the entire page will be replaced with the new one).

## Navigation in SPA

To navigate between the pages in the SPA, you need to use the [RouteLink](~/controls/builtin/RouteLink) control. If the page contains the `SpaContentPlaceHolder` control, DotVVM will try to load the page in the SPA mode.

## Redirects in SPA

If you want to redirect the user to another page, you can use the `Context.RedirectToRoute` method. There are some optional parameters which can be used in the SPA applications:

* `replaceInHistory` can replace the History API stack, so the browser Back button won't go to the previous SPA state.
* `allowSpaRedirect` can be used to disable SPA redirects; in such case, DotVVM will unload the current page and perform a full navigation to the new page.

## Changes to SPAs in DotVVM 3.0

### Multiple `SpaContentPlaceHolder` controls

In previous versions of DotVVM, there could be only one `SpaContentPlaceHolder` control present in the page at the same time. Starting with DotVVM 3.0, this restriction has been lifted, and it is now possible to use multiple SPAs per page.

### Removed hashbang mode in favor of History API

In DotVVM 2.x, the default behavior of SPAs was using the hashbang format of the URL (e. g. `~/MySpaApp#!/Pages/Default`). When the user navigated to another page, the part of the URL after the URL fragment changed. It was possible to explicitly enable [History API](https://developer.mozilla.org/en-US/docs/Web/API/History_API) which was using the classic format of the URL which is not recognizeable from the non-SPA application. 

In DotVVM 3.0, the **History API mode is the one and only option** - the old way with hashbang URLs was removed from the framework. All requests pointing to the hashbang URL format will get redirected to the correct URLs, and History API will be used everywhere.

The following properties on `SpaContentPlaceHolder` were marked as obsolete:

* `DefaultRouteName`
* `PrefixRouteName`
* `UseHistoryApi`

## See also

* [Master pages](~/pages/concepts/layout/master-pages)
* [RouteLink](~/controls/builtin/RouteLink)
* [SpaContentPlaceHolder](~/controls/builtin/SpaContentPlaceHolder)