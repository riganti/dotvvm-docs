# Localization presenter

> If you are using ASP.NET Core, consider using [Request localization middleware](~/pages/concepts/localization-and-cultures/multi-language-applications?tabs=aspnetcore#switching-cultures) instead of the localization presenter.

Localization presenter is a mechanism which detects and sets the correct culture before the HTTP request is processed, so all methods (including `async`/`await` calls which may use different threads) use the same culture consistently.

The `LocalizablePresenter` is a class which accepts two functions - the first one can detect the culture from the [Request context](~/pages/concepts/viewmodels/request-context), the second one invokes the presenter which will process the page while settings the correct culture.

The class provides two factory methods which create an instance of the localization presenter:

* `BasedOnParameter` uses a route parameter to persist the culture name.
* `BasedOnQuery` uses a query string parameter to persist the culture name.

## Usage

Localization presenters are configured as part of [route registration](~/pages/concepts/routing/overview). 

In order to use route parameter as culture identifier, use the following registration:

```CSHARP
config.RouteTable.Add("Default", "{Lang:length(2)}", "Views/Default.dothtml", new { Language = "en" }, 
    presenterFactory: LocalizablePresenter.BasedOnParameter("Lang"));
```

To use a query string parameter to specify a language, use the following registration:

```CSHARP
config.RouteTable.Add("Default", "", "Views/Default.dothtml", 
    presenterFactory: LocalizablePresenter.BasedOnQuery("lang"));
```

If the language is not specified, the default culture from [DotVVM Configuration](~/pages/concepts/configuration/overview) is used for the specific request.

The `LocalizablePresenter` factory methods have also the second optional argument which tells the presenter to automatically redirect to default culture if no culture is specified.

The easiest way is to use route parameters to persist the current culture. The URL format will be `/en/Home`.

```CSHARP
config.RouteTable.Add("Home", "{Lang}/home", "Views/Home.dothtml", new { Lang = "en" }, 
    presenterFactory: LocalizablePresenter.BasedOnParameter("Lang"));
```

Alternatively, you can use a query string parameter - the URL format will be `/Home?lang=en` in this case:

```CSHARP
config.RouteTable.Add("Home", "home", "Views/Home.dothtml", 
    presenterFactory: LocalizablePresenter.BasedOnQuery("lang"));
```

The localizable presenter will use the culture from the route or query string parameter and sets the `Thread.CurrentThread.CurrentCulture` to this culture for the HTTP request. The same culture is set for all async calls and is used even if the part of the method after an awaited call is executed on a different thread.

The `BasedOnParameter` and `BasedOnQuery` methods have a second optional parameter which specifies whether a redirect should be performed when the specified culture was not found. It is `true` by default.

## See also

* [Multi-language applications](multi-language-applications)
* [Routing](~/pages/concepts/routing/overview)
* [RESX files](resx-files)
* [Formatting dates and numbers](formatting-dates-and-numbers)
