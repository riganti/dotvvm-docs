# Request Context

The `DotvvmViewModelBase` class contains the `Context` property which provides access to various DotVVM and ASP.NET resources, like OWIN or ASP.NET Core context etc.  
It can also perform redirects, return files to be downloaded, and other things.

The `IDotvvmRequestContext` object contains the following properties and methods:

+ `IsPostBack` property determines whether the current request is postback, or whether the page is loaded for the first time.

+ `ModelState` property represents the state of the [model validation](/docs/tutorials/basics-validation/{branch}). You can report validation errors to the 
UI using this object's `AddModelError` method, or you can use the `IsValid` property to determine whether there are any validation errors in the page.

+ The `FailOnInvalidModelState` method checks the validity of the viewmodel and throws an exception if the viewmodel is not valid. The request execution is interrupted and the validation errors are displayed in the client's browser (e.g. in the [ValidationSummary](/docs/controls/builtin/ValidationSummary/{branch}) control).

+ `Parameters` property is a dictionary which contains values of the [route parameters](/docs/tutorials/basics-routing/{branch}).

+ `Query` property is a dictionary which contains parameters from the URL query string.

+ `HttpContext` property gives you access to the OWIN or ASP.NET context of the current request. It's useful when you need to work with cookies, authentication etc. This property exposes a common interface over `OwinContext` and `HttpContext` from ASP.NET Core. Not all features are available in this abstraction because of the differences of the platforms.
This property is available in **DotVVM 1.1** and newer. In the previous version, there was the `OwinContext` property instead.

+ `GetOwinContext()` is an extension method which you can use in the OWIN version. It returns the real `OwinContext`. This method is available in **DotVVM 1.1** and newer.

+ `GetAspNetCoreContext()` is an extension method which you can use in the ASP.NET Core version to access the ASP.NET Core  `HttpContext`. This method is available in **DotVVM 1.1** and newer.

+ `RedirectToUrl` and `RedirectToUrlPermanent` methods redirect the user to the specified URL. 
The request execution is interrupted by this call.

+ `RedirectToRoute` and `RedirectToRoutePermanent` methods redirect the user to the specified route and allows to supply route parameters. 
The request execution is interrupted by this call. 

+ `InterruptExecution` interrupts the execution of the HTTP request by DotVVM and pass it to the next OWIN middleware.  

+ `ReturnFile` function is used when you need to [return a file](/docs/tutorials/advanced-returning-files/{branch}) which will be downloaded by the user.

> In **DotVVM 2.0**, the methods from `DotvvmRequestContext` were changed to extension methods. The usage and their meaning remains the same.

> In **DotVVM 2.0**, the methods which return and change current culture were made obsolete as they don't work correctly in asynchronous calls. See [Globalization](/docs/tutorials/basics-globalization/{branch}) for more information.