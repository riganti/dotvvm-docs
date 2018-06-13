# Request Context

The `DotvvmViewModelBase` class contains the `Context` property which provides access to various DotVVM and ASP.NET resources, OWIN context etc.  
It can also perform redirects, return files to be downloaded, and other things.

The `DotvvmRequestContext` object contains the following properties and methods:

+ `IsPostBack` property determines whether the current request is postback, or whether the page is loaded for the first time.

+ `ModelState` property represents the state of the [model validation](/docs/tutorials/basics-validation/{branch}). You can report validation errors to the 
UI using this object's `AddModelError` method, or you can use the `IsValid` property to determine whether there are any validation errors in the page.

+ The `FailOnInvalidViewModel` method checks the validity of the viewmodel and throws an exception if the viewmodel is not valid. The request execution is interrupted and the validation errors are displayed in the client's browser (e.g. in the [ValidationSummary](/docs/controls/builtin/ValidationSummary/{branch}) control).

+ `Parameters` property is a dictionary which contains values of the [route parameters](/docs/tutorials/basics-routing/{branch}).

+ `Query` property is a dictionary which contains parameters from the URL query string.

+ `OwinContext` property gives you access to the OWIN context of the current request. It's useful when you need to work with cookies, authentication etc. Please note that from **DotVVM 1.1**, you should use the `HttpContext` property or `GetOwinContext()` method instead of this property. This property was removed because of the ASP.NET Core support.

+ `RedirectToUrl` and `RedirectPermanentToUrl` methods redirect the user to the specified URL. 
The request execution is interrupted by this call.

+ `RedirectToRoute` and `RedirectPermanentToRoute` methods redirect the user to the specified route and allows to supply route parameters. 
The request execution is interrupted by this call. 

+ `InterruptExecution` interrupts the execution of the HTTP request by DotVVM and pass it to the next OWIN middleware.  

+ `ChangeCurrentCulture` changes the culture of the thread that executes the request.

+ `ReturnFile` function is used when you need to [return a file](/docs/tutorials/advanced-returning-files/{branch}) which will be downloaded by the user.
