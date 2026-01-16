# Route localization

In some scenarios, you may want to use a different route URL for each website language. This can help the page to get a better ranking with the search engines, and provide cleaner URLs to your users.

Culture | Route URL
-----------|-----------
`en` (default) | `en/contact-us`
`fr` | `fr/contactez-nous`
`cs` | `cs/kontaktujte-nas`

## Declare localized route variants

Usually, there is one default culture that is used as a fallback, but it is possible to provide alternative route URLs for other cultures:

```CSHARP
config.RouteTable.Add("Contact", "en/contact-us", "Views/Contact.dothtml",
    localizedUrls: new LocalizedRouteUrl[] {
        new("fr", "fr/contactez-nous"),
        new("cs", "cs/kontaktujte-nas")
    });
```

If a HTTP request matches either of the provided route URLs, the specified page will be used.

> If the route contains parameters, all its localized versions must define the same parameters with equal constraints.

## Handling the request culture

Localized routes are responsible **merely for matching multiple variants of route URL**. The localization of the page content depends on the culture of the thread assigned to process the HTTP request (`Thread.CurrentThread.CurrentCulture` and `Thread.CurrentThread.CurrentUICulture`).

You need to configure [Localization](~/pages/concepts/localization-and-cultures/multi-language-applicationse) to set the culture for threads that process HTTP requests. The exact process can vary in different applications and scenarios.

### Option 1: Use the culture specified in the route

One common pattern is to configure the routes like in the example above, with the culture identifier present in the URL. In such case, we need DotVVM to switch the request culture to reflect the prefix in the URL

This can be done by implementing a simple handler, and configuring **ASP.NET Core Request Localization** as such:

```CSHARP
// Program.cs (or Startup.cs)

...
builder.Services.AddRequestLocalization(options =>
{
    var cultures = new[] { "en", "cs" };
    options
		.AddSupportedCultures(cultures)
		.AddSupportedUICultures(cultures)
		.SetDefaultCulture(cultures[0]);

    options.RequestCultureProviders.Clear();
    options.RequestCultureProviders.Add(new LanguagePrefixRequestCultureProvider(options));
});
...


// LanguagePrefixRequestCultureProvider.cs

public class LanguagePrefixRequestCultureProvider(RequestLocalizationOptions options) : RequestCultureProvider
{
	public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
	{
		if (options.SupportedUICultures!.FirstOrDefault(c => httpContext.Request.Path.StartsWithSegments("/" + c.TwoLetterISOLanguageName)) is { } culture)
		{
			return Task.FromResult(new ProviderCultureResult(culture.TwoLetterISOLanguageName));
		}
		return NullProviderCultureResult;
	}
}
```

When an incoming HTTP request starts with a URL segment equal to two-letter culture code contained in supported UI cultures (such as `en` or `cs`), the specified culture will be used. Otherwise, the provider returns `NullProviderCultureResult`, which will fallback to the next provider in the chain (if present).

### Option 2: Redirect to the correct route based on request culture

Another pattern is to remember the culture somewhere else.

By default, ASP.NET Core Request Localization middleware looks for a special cookie, which is used when the user has already selected the preferred language at the website. If such cookie is not present, the middleware looks at the request's `Accept-Language` header which contains a list of cultures preferred by the user that come from their browser settings.

Instead of extracting the culture from the matched route URL, we want to use the culture the user had already selected on the site. This can be done by configuring a **partial match handler** in `DotvvmStartup.cs`. This handler is triggered when a route is matched, but its culture is different to the culture that is actually used in the website. The `CanonicalRedirectPartialMatchRouteHandler` will redirect to the URL that corresponds with the current culture of the application.

```CSHARP
config.RouteTable.AddPartialMatchHandler(new CanonicalRedirectPartialMatchRouteHandler());
```

For example, if the user has set the culture cookie to English and visit a link corresponding to a Czech version of the page (`cs/kontaktujte-nas`), this handler will redirect them to `en/contact-us`, as it is the URL for the English culture.

## Linking to specific culture

When you use the [RouteLink](~/controls/builtin/RouteLink) control to create links to another page, DotVVM will automatically use the URL corresponding with the current culture of the page (for example, a page displayed in the Czech language will link to the Czech version of the other page). The same applies to [redirects to other routes](/pages/concepts/routing/overview#redirect-to-another-route) - DotVVM will preserve the current culture, if a localized version for that culture is defined.

Multi-language applications usually provide the users with some way to change the language. You can use the `Culture` property of the `RouteLink` control to specify the explicit culture for which the URL will be generated. A language switch buttons in the menu may look like this:

```DOTHTML
<dot:RouteLink RouteName="{resource: Context.Route.RouteName}" Culture="cs" Text="CS" />
<dot:RouteLink RouteName="{resource: Context.Route.RouteName}" Culture="en" Text="EN" />
```

The `Context.RedirectToRoute` method also offers an overload with the `culture` parameter to allow redirecting to a specific language version of the route.

## Alternate culture links

If some page is available in multiple languages, it may be useful to specify the `<link rel=alternate ... />` elements in the page header to advise browsers and search engines that the content is available in other languages.

If you use localized routes, you can have these links generated automatically by using the [AlternateCultureLinks](~/controls/builtin/AlternateCultureLinks) control.

```DOTHTML
...
<head>
    ...
    <dot:AlternateCultureLinks />
</head>
...
```

This will produce the following HTML:

```DOTHTML
    <link href="https://yoursite/en/contact-us" rel="alternate" hreflang="en" />
    <link href="https://yoursite/cs/kontaktujte-nas" rel="alternate" hreflang="cs" />
    <link href="https://yoursite/fr/contactez-nous" rel="alternate" hreflang="fr" />
```

## See also

* [Routing](overview)
* [Multi-language applications](~/pages/concepts/localization-and-cultures/multi-language-applications)
* [Localizable presenter](~/pages/concepts/localization-and-cultures/localizable-presenter)
