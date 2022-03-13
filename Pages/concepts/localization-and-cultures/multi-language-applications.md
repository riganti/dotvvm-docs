# Multi-language applications

DotVVM supports localization and comes with many features that help building multi-language applications.

* The [Resource binding](~/pages/concepts/data-binding/resource-binding) can be used to obtain values from RESX files. See [RESX files](resx-files) chapter for more info.
* You can use [format strings and functions](formatting-dates-and-numbers) to format dates and numbers according to the current culture.

## Request culture

When DotVVM serializes the viewmodel, it includes an information about the current thread culture which was used to process the request. This information is then used on the client so the formatting and other features work the same as on the server.

If you use any control which works with numeric or date values (e.g. [Literal](~/controls/builtin/Literal) with its `FormatString` property), or use the `ToString` method in a [value binding](~/pages/concepts/data-binding/value-binding), the page needs to know which culture should be used in order to apply the correct format.

### Default application culture

In the [configuration](~/pages/concepts/configuration/overview) of DotVVM, you can specify the default culture which is used for all requests. The best way 
is to set this value in the `DotvvmStartup.cs` file using the following code:

```CSHARP
config.DefaultCulture = "en-US";
```

### Switching cultures

If your website supports multiple languages and cultures, you need to store the language the user has selected somewhere. It is common to detect the language from the `Accept-Language` header, and when the user chooses a different language, store the selection in cookies or include it in the URL.

# [ASP.NET Core](#tab/aspnetcore)

In ASP.NET Core, the preferred way for handling this is using the [Request localization middleware](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-5.0#localization-middleware-2). You can configure the supported languages, the storage of the selected language, and so on.

First, configure the request localization in `ConfigureServices` in `Startup.cs`:

```CSHARP
public void ConfigureServices(IServiceCollection services)
{
    ... 

    services.Configure<RequestLocalizationOptions>(options =>
    {
        var supportedCultures = new[]
        {
            new CultureInfo("en"),
            new CultureInfo("cs")
        };
        options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });
}
```

Then, register the request localization middleware before the call to `UseDotVVM`:

```CSHARP
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseRequestLocalization();   // must be before UseDotVVM
    ...
    app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);
}
```

Now, the request localization middleware will try to detect the language from the `Accept-Language` HTTP header, or from a cookie. 

To switch the language, you need to store the language preference in the cookie:

```CSHARP
public void SwitchLanguage(string language)
{
    Context.GetAspNetCoreContext().Response.Cookies
        .Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(language))
        );
    Context.RedirectToLocalUrl(Context.HttpContext.Request.Url.PathAndQuery);
}
```

See the [DotVVM request localization sample](https://github.com/riganti/dotvvm-samples-request-localization) for more info.


# [OWIN](#tab/owin)

In OWIN, a mechanism similar to ASP.NET Core request localization is not supported. 

Instead, you can use the [Localizable presenter](localizable-presenter) which can define a route parameter to persist the language chosen by the user. 

***

## See also

* [Formatting dates and numbers](formatting-dates-and-numbers)
* [Local vs UTC dates](local-vs-utc-dates)
* [RESX files](resx-files)
* [Sample: DotVVM request localization](https://github.com/riganti/dotvvm-samples-request-localization)
* [Localizable presenter](localizable-presenter) (recommended for OWIN only)
