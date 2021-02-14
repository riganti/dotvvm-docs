# Globalization And Cultures

When DotVVM serializes the viewmodel, it includes an information about the current thread culture which was used to process the request.

If you use any control which works with numeric or date values (e.g. [Literal](/docs/controls/builtin/Literal/{branch}) with its `FormatString` property), 
the page needs to know which culture should be used in order to apply the correct format.

## Default Culture

In the [DotVVM configuration](/docs/tutorials/basics-configuration/{branch}), you can specify the default culture which is used for all requests. The best way 
is to set this value in the `DotvvmStartup.cs` file using the following code:

```CSHARP
config.DefaultCulture = "en-US";
```

## Switching Cultures

> The way how cultures are switched was different in [DotVVM 1.1](/docs/tutorials/basics-globalization/1-1) and didn't handle asynchronous methods correctly in some cases. 

If your website supports multiple languages and cultures, you need to store the language the user has selected somewhere. 
Whichever method you use (cookies, URL, database...), you need to tell DotVVM at the beginning of the request processing, which culture is used for the particular
HTTP request.

When you register the route, you can specify a custom presenter factory and use [Localization Presenter](/docs/tutorials/advanced-localization-presenter/{branch}).

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

If you need to implement another way of handling the culture, see [Localization Presenter](/docs/tutorials/advanced-localization-presenter/{branch}) chapter for more details.

> The `Context.ChangeCurrentCulture` method used in [DotVVM 1.1](/docs/tutorials/basics-globalization/1-1) and older versions was removed.
