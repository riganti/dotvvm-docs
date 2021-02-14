# Filters

> The API of action filters has been changed in DotVVM 2.0. See the [Upgrading to DotVVM 2.0](/docs/tutorials/how-to-start-upgrade-to-2-0/2-0#action-filters) page for more information. 

In large apps and sites, you need to do apply global actions e.g. for each button click
on a specific page, section or even on all pages in the app.

You might need to do global exception handling and logging, or to switch the culture based on a value from cookies etc

In **DotVVM**, we have a concept of filters for this purpose. If you know Action Filters
in ASP.NET MVC or ASP.NET Web API, it is the same.


## Action Filters

If you want to apply a common logic to one or more viewmodels, viewmodel commands or a whole application, you need to create a class that derives from `ActionFilterAttribute`.

You can then override any of the method listed below.

+ **Presenter-level Events** (applicable to DotVVM pages and custom presenters)
    - `OnPresenterExecutingAsync` is executed immediately after the URL is mapped to a specific route and presenter is resolved. This method is called also for DotVVM pages as they are handled by `DotvvmPresenter` class.
    - `OnPresenterExecutedAsync` is executed immediately after the presenter completes processing of the request. This method is called also for DotVVM pages as they are handled by `DotvvmPresenter` class.
    - `OnPresenterExceptionAsync` is executed when an unhandled exception is thrown from the presenter. This method is called also for DotVVM pages as they are handled by `DotvvmPresenter` class.

+ **Page-level Events** (applicable to DotVVM pages)
    - `OnPageInitializedAsync` is executed after the page control tree is built and viewmodel instance is initialized.
    - `OnPageRenderedAsync` is executed after the response is rendered completely and before the viewmodel instance is disposed.
    - `OnPageExceptionAsync` is executed when an unhandled exception occurs during the processing of the DotVVM page.

+ **ViewModel-level Events** (applicable to DotVVM pages)
    - `OnViewModelCreatedAsync` is executed after the viewmodel instance is created and assigned to the root of the control tree, and the `PreInit` phase is completed.
    - `OnViewModelSerializingAsync` is executed after the `PreRenderComplete` phase is completed and before the viemwodel is serialized to JSON.
    - `OnViewModelDeserializedAsync` is executed on postbacks, after the viewmodel from the client was deserialized, before the `Load` phase is initiated.

+ **Command-level Events** (applicable to postbacks on DotVVM pages)
    - `OnCommandExecutingAsync` is executed on postbacks, before the command referenced from a command binding is called.
    - `OnCommandExecutedAsync` is executed on postbacks, after the command referenced from a command binding is called.

There is also a class called `ExceptionFilterAttribute` which adds another event:

- `OnCommandException` is executed on postbacks, when the command referenced from a command binding throws an exception.

If you only need to target specific events, you don't need to inherit from these attributes. You can implement the `IPresenterActionFilter`, `IPageActionFilter`, `ICommandActionFilter` or `IViewModelActionFilter` interface instead.


## Example: Model Validation using Filters

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
