# Filters

> The API of action filters has been changed in DotVVM 1.1. See the [Upgrading from DotVVM 1.0](/docs/tutorials/how-to-start-upgrade-from-1-0/1-1) page for more information.

In large apps and sites, you need to do apply global actions e.g. for each button click
on a specific page, section or even on all pages in the app.

You might need to do global exception handling and logging, or you just need to check
`ModelState.IsValid` on each postback.

In **DotVVM**, we have a concept of filters for this purpose. If you know Action Filters
in ASP.NET MVC or ASP.NET Web API, it is the same concept.


## Action Filters

If you want to apply a common logic to one or more viewmodels, viewmodel commands or 
a whole application, you have to create a class that derives from `ActionFilterAttribute`.

You have 4 methods you can override in this class:

+ `OnViewModelCreated` - this method is called after the viewmodel was created. 
You can use it to set additional viewmodel properties.

+ `OnCommandExecuting` - this method is called right before the method referenced in
the command binding is invoked. This method is only invoked during the postback.

+ `OnCommandExecuted` - this method is called right after the method referenced in
the command binding was finished. This method is only invoked during the postback.

+ `OnResponseRendering` - this method is called right before the viewmodel is serialized
and the response is rendered.



## Model Validation using Filters

If you want to perform the model validation before every command, it is not difficult. Actually, **DotVVM** already include 
such filter and registers it for all requests by default. 

```CSHARP
using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Framework.Runtime.Filters
{

    /// <summary>
    /// Runs the model validation and returns the errors if the viewModel is not valid.
    /// </summary>
    public class ModelValidationFilterAttribute : ActionFilterAttribute
    {

        private ViewModelValidator viewModelValidator = new ViewModelValidator();

        /// <summary>
        /// Called before the command is executed.
        /// </summary>
        protected internal override void OnCommandExecuting(IDotvvmRequestContext context, ActionInfo actionInfo)
        {
            if (!string.IsNullOrEmpty(context.ModelState.ValidationTargetPath))
            {
                // perform the validation
                context.ModelState.Errors.AddRange(viewModelValidator.ValidateViewModel(context.ModelState.ValidationTarget));

                // return the model state when error occurs
                context.FailOnInvalidModelState();
            }

            base.OnCommandExecuting(context, actionInfo);
        }
    }
}
```

In the `OnCommandExecuting` method we have to check whether the `ValidationTargetPath` is set or not. If not, then
the validation was disabled on the control which fired the postback.

We need to perform the validation and call `FailOnInvalidModelState`, which throws an exception and return
the model errors to the client, if there are any. If the viewmodel is valid, it doesn't do anything.

**DotVVM** serializes the validation errors and displays the validation errors in the page.