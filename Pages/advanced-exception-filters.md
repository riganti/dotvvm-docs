# Exception Filters

> The API of action filters has been changed in DotVVM 1.1. See the [Upgrading from DotVVM 1.0](/docs/tutorials/how-to-start-upgrade-from-1-0/1-1) page for more information. 

If you want to handle exceptions, there is a class called `ExceptionFilterAttribute`.
It derives from the `ActionFilterAttribute` and it adds the `OnCommandException` method. 

The `OnCommandException` is called whenever an error occurs in a method triggered by the `{command: ...}` or `{staticCommand: ...}` bindings.

The `ActionFilterAttribute` class then defines the `OnPageException` method. This method is called when any other exception occurs when the page is being processed. In most cases, it catches the exceptions in the `Init`, `Load` and `PreRender` in the viewmodel, and the exceptions that occur during the rendering or serialization phases. 

```CSHARP
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime.Filters;

namespace DotvvmDemo
{
    public class ErrorLoggingActionFilter : ExceptionFilterAttribute
    {
        protected override void OnCommandException(IDotvvmRequestContext context, ActionInfo actionInfo, Exception exception)
        {
            // Log exceptions from commands in the viewmodel
			LogService.LogException(exception.ToString());
        }
        
        protected override void OnPageException(IDotvvmRequestContext context, Exception exception)
        {
            // Log other exceptions that occur during the page execution
			LogService.LogException(exception.ToString());
        }              
    }
}
```



## Command Exception Handling

In many apps, the commands sometimes end with an exception. However you don't want to show the error page to the user in this case. Instead, you need to 
log the exception and display an error message to the user.

You can use something like this:

```CSHARP
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime.Filters;
namespace DotvvmDemo
{
    public class ErrorHandlingActionFilter : ExceptionFilterAttribute
    {
        protected override void OnCommandException(IDotvvmRequestContext context, ActionInfo actionInfo, Exception exception)
        {
			// AppViewModelBase declares the ErrorMessage property to display error messages
			// If it is set, the master page will display the error message alert.
            if (context.ViewModel is AppViewModelBase)
            {
				((AppViewModelBase) context.ViewModel).ErrorMessage = exception.Message;
                
				// We need the request to end normally, not with an error
                context.IsCommandExceptionHandled = true;
            }
        }       
    }
}
```

The `AppViewModelBase` is a base class for all viewmodels in the application and has the `ErrorMessage` property. If the request execution should 
continue like there was no error, we need to set `context.IsCommandExceptionHandled` to `true`.



## Custom Error Pages

If the exception occurs during the `Init`, `Load` and `PreRender` phase, you often need to redirect the user to an error page. 

You can do it in the `OnPageException` method like this:

```CSHARP
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime.Filters;
namespace DotvvmDemo
{
    public class ErrorHandlingActionFilter : ExceptionFilterAttribute
    {
        protected override void OnPageException(IDotvvmRequestContext context, Exception exception)
        {
            // Suppress the default DotVVM error page
			context.IsPageExceptionHandled = true;
            
            // TODO: Log the exception details
            
            // Redirect to the /error500 page 
            context.RedirectToUrl("/error500");
        }       
    }
}
```