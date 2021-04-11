# Request context

The `DotvvmViewModelBase` class defines the `Context` property which provides access to various DotVVM and ASP.NET resources, like ASP.NET Core or OWIN HTTP context etc. You can also make redirects, return files, and more.

## Request information

+ `IsPostBack` property determines whether the current request is postback, or whether the page is loaded for the first time.
+ `Parameters` property is a dictionary which contains values of the [route parameters](~/pages/concepts/routing/parameters).
+ `Query` property is a dictionary which contains parameters from the URL query string.
+ `HttpContext` property gives you access to the OWIN or ASP.NET context of the current request. It's useful when you need to work with cookies, authentication etc. This property exposes a common interface over `OwinContext` and `HttpContext` from ASP.NET Core. Not all features are available in this abstraction because of the differences between the two platforms.
    + `GetAspNetCoreContext()` is an extension method which you can use in the ASP.NET Core version to access the ASP.NET Core  `HttpContext`.
    + `GetOwinContext()` is an extension method which you can use in the OWIN version. It returns the real `OwinContext`.

## Validation

+ `ModelState` property represents the state of the [model validation](~/pages/concepts/validation/overview). You can report validation errors to the 
UI using this object's `AddModelError` method, or you can use the `IsValid` property to determine whether there are any validation errors in the page.
+ The `FailOnInvalidModelState` method checks the validity of the viewmodel and throws an exception if the viewmodel is not valid. The request execution is interrupted and the validation errors are displayed in the client's browser (e.g. in the [ValidationSummary](~/controls/builtin/ValidationSummary) control).

## Redirects

+ `RedirectToRoute` and `RedirectToRoutePermanent` methods redirect the user to the specified route and allows to supply route parameters. 
The request execution is interrupted by this call. 
+ `RedirectToLocalUrl` method redirects the user to the specified URL and makes sure that the URL is not an absolute URL pointing to a different site. The request execution is interrupted by this call.
+ `RedirectToUrl` and `RedirectToUrlPermanent` methods redirect the user to the specified URL. The request execution is interrupted by this call.

## Returning files

+ `ReturnFile` function is used when you need to [return a file](~/pages/concepts/upload-and-download-files) which will be downloaded by the user.

## Asynchronous operations

+ `GetCancellationToken` returns a cancellation token for the current request. You can pass this token to a long-running operations so they can be cancelled if the user leaves the page before it is loaded.

## URL manipulation

+ `TranslateVirtualPath` method can translate URLs from the `~/some/path` notation to the domain-relative URLs (e. g. `/appVirtualDirectory/some/path`). This is useful if your application supports running in a virtual directory.

## See also

* [Viewmodels overview](overview)
* [Routing](~/pages/concepts/routing/overview)
* [Validation](~/pages/concepts/validation/overview)