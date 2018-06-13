# Filters

> The API of action filters has been changed in DotVVM 1.1. See the [Upgrading from DotVVM 1.0](/docs/tutorials/how-to-start-upgrade-from-1-0/1-1) page for more information. 

In large apps and sites, you need to do apply global actions e.g. for each button click
on a specific page, section or even on all pages in the app.

You might need to do global exception handling and logging, or to switch the culture based on a value from cookies etc

In **DotVVM**, we have a concept of filters for this purpose. If you know Action Filters
in ASP.NET MVC or ASP.NET Web API, it is the same.


## Action Filters

If you want to apply a common logic to one or more viewmodels, viewmodel commands or a whole application, you have to create a class that derives from `ActionFilterAttribute`.

You can then override any of the method listed below.

+ **Page-level Events**
    - `OnPageLoadingAsync` is executed immediately after the URL is mapped to a specific route and the viewmodel instance is created.
    - `OnPageLoadedAsync` is executed after the response is rendered completely.
    - `OnPageExceptionAsync` is executed when an unhandled exception occurs during the HTTP request processing.

+ **ViewModel-level Events**
    - `OnViewModelCreatedAsync` is executed when the viewmodel instance is created.
    - `OnViewModelSerializingAsync` is executed before the viemwodel is serialized to JSON and sent to the client.
    - `OnViewModelDeserializedAsync` is executed on postbacks, after the viewmodel from the client was deserialized.

+ **Command-level Events**
    - `OnCommandExecutingAsync` is executed on postbacks, before the command referenced from a command binding is called.
    - `OnCommandExecutedAsync` is executed on postbacks, after the command referenced from a command binding is called.

There is also a class called `ExceptionFilterAttribute` which adds another event:

- `OnCommandException` is executed on postbacks, when the command referenced from a command binding throws an exception.

If you only need to target specific events, you don't need to inherit from these attributes. You can implement the `IPageActionFilter`, `ICommandActionFilter` or `IViewModelActionFilter` interface instead.


## Model Validation using Filters

If you want to perform the model validation before every command, it is not difficult. Actually, **DotVVM** already include 
such filter and registers it for all requests by default. 

Here is how it's implemented:

```CSHARP
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.Framework.Runtime.Filters
{
    /// <summary>
    /// Runs the model validation and returns the errors if the viewModel is not valid.
    /// </summary>
    public class ModelValidationFilterAttribute : ActionFilterAttribute
    {

        protected internal override Task OnCommandExecutingAsync(IDotvvmRequestContext context, ActionInfo actionInfo)
        {
            if (!string.IsNullOrEmpty(context.ModelState.ValidationTargetPath))
            {
                var validator = context.Services.GetService<IViewModelValidator>();
                context.ModelState.Errors.AddRange(validator.ValidateViewModel(context.ModelState.ValidationTarget));
                context.FailOnInvalidModelState();
            }

            return Task.FromResult(0);
        }
    }
}
```

In the `OnCommandExecutingAsync` method we have to check whether the `ValidationTargetPath` is set or not. If not, then
the validation was disabled on the control which fired the postback.

We need to perform the validation and call `FailOnInvalidModelState`, which throws an exception and return
the model errors to the client, if there are any. If the viewmodel is valid, it doesn't do anything.

**DotVVM** serializes the validation errors and displays the validation errors in the page.
