# Action filters

In large apps and sites, you need to do apply global actions e.g. for each button click on a specific page, section or even on all pages in the app. Additionally, you may want to do global exception handling and logging, or switch the culture based on a value from cookies etc.

In **DotVVM**, we have a concept of [filters](overview). If you know Action filters in ASP.NET MVC, it is the same thing.

## Filter events

If you want to apply a common logic to one or more viewmodels, viewmodel commands or a whole application, you need to create a class that derives from `ActionFilterAttribute`.

You can then override any of the method listed below.

+ **Presenter-level events** (applicable to DotVVM pages and custom presenters)
    - `OnPresenterExecutingAsync` is executed immediately after the URL is mapped to a specific route and presenter is resolved. This method is called also for DotVVM pages as they are handled by `DotvvmPresenter` class.
    - `OnPresenterExecutedAsync` is executed immediately after the presenter completes processing of the request. This method is called also for DotVVM pages as they are handled by `DotvvmPresenter` class.
    - `OnPresenterExceptionAsync` is executed when an unhandled exception is thrown from the presenter. This method is called also for DotVVM pages as they are handled by `DotvvmPresenter` class.

+ **Page-level events** (applicable to DotVVM pages)
    - `OnPageInitializedAsync` is executed after the page control tree is built and viewmodel instance is initialized.
    - `OnPageRenderedAsync` is executed after the response is rendered completely and before the viewmodel instance is disposed.
    - `OnPageExceptionAsync` is executed when an unhandled exception occurs during the processing of the DotVVM page.

+ **ViewModel-level events** (applicable to DotVVM pages)
    - `OnViewModelCreatedAsync` is executed after the viewmodel instance is created and assigned to the root of the control tree, and the `PreInit` phase is completed.
    - `OnViewModelSerializingAsync` is executed after the `PreRenderComplete` phase is completed and before the viemwodel is serialized to JSON.
    - `OnViewModelDeserializedAsync` is executed on postbacks, after the viewmodel from the client was deserialized, before the `Load` phase is initiated.

+ **Command-level events** (applicable to postbacks on DotVVM pages)
    - `OnCommandExecutingAsync` is executed on postbacks, before the command referenced from a command binding is called.
    - `OnCommandExecutedAsync` is executed on postbacks, after the command referenced from a command binding is called.

There is also a class called `ExceptionFilterAttribute` which adds another event:

- `OnCommandException` is executed on postbacks, when the command referenced from a command binding throws an exception.

If you only need to target specific events, you don't need to inherit from these attributes. You can implement the `IPresenterActionFilter`, `IPageActionFilter`, `ICommandActionFilter` or `IViewModelActionFilter` interface instead.

## See also

* [Filters](overview)
* [Exception filters](exception-filters)
* [Authentication and authorization](~/pages/concepts/security/authentication-and-authorization/overview)