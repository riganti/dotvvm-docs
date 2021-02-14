# Localization Presenter

> This feature is new in **DotVVM 2.0**.

Localizable presenter is a mechanism which detects and sets the correct culture before the HTTP request is processed, so all methods (including async/await calls which may use different threads) use the same culture consistently.

The `LocalizablePresenter` is a class which accepts two functions - the first one can detect the culture from the [Request Context](/docs/tutorials/basics-request-context/{branch}), the second one invokes the presenter which will process the page.

The class provides two factory methods which create an instance of the localization presenter:

* `BasedOnParameter` uses a route parameter to persist the culture name.
* `BasedOnQuery` uses a query string parameter to persist the culture name.

## Usage

Localization presenters are configured as part of [route registration](/docs/tutorials/basics-routing/{branch}). 

In order to use route parameter as culture identifier, use the following registration:

```CSHARP
config.RouteTable.Add("Default", "{Language:length(2)}", "Views/Default.dothtml", new { Language = "en" }, presenterFactory: LocalizablePresenter.BasedOnParameter("Language"));
```

To use query string parameter, use the following registration:

```CSHARP
config.RouteTable.Add("Default", "", "Views/Default.dothtml", presenterFactory: LocalizablePresenter.BasedOnQuery("lang"));
```

If the language is not specified, the default culture from [DotVVM Configuration](/docs/tutorials/basics-globalization/{branch}) is used for the specific request.
The `LocalizablePresenter` factory methods have also the second optional argument which tells the presenter to automatically redirect to default culture if no culture is specified.
