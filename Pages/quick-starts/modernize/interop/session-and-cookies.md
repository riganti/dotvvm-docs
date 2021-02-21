# Session and cookies

> This topic is related to the OWIN version of DotVVM only. It is relevant only if the application needs to interact with ASP.NET session, or if you have some code that modifies cookies through `System.Web.HttpContext`.

## Synchronization OWIN and ASP.NET cookies

OWIN offers its own extensible way of working with cookies. By default, the `ChunkingCookieManager` class is used. 

However, when the application interacts with cookies using through `System.Web.HttpContext` (the classic ASP.NET way), a conflict occurs and the changes made by the `ChunkingCookieManager` will be lost.

DotVVM needs to store [CSRF](https://en.wikipedia.org/wiki/Cross-site_request_forgery) token in a cookie to provide a secure way of executing postbacks. When the browser makes the first request to a DotVVM web application, it stores the CSRF token in the cookie. If some code uses the ASP.NET session in the request, or the `HttpContext.Current.Response.Cookies` collection is changed, the changes to the cookie made from DotVVM are overwritten by the `HttpContext`, and thus the CSRF token will be lost. When a postback occurs, DotVVM will throw the following exception: 

```
System.Security.SecurityException: SessionID cookie is missing, so can't verify CSRF token.
```

The preferred solution to this problem would be using a different way to store session-related data, or replace the default `ChunkingCookieManager` class by another implementation which will use `HttpContext` to interact with cookies.

## Enabling session in OWIN

In order to use session in OWIN, the following code should be placed in `Startup.cs` before any middleware is registered. Otherwise, `HttpContext.Current.Session` would be null.

```CSHARP
app.Use((context, next) =>
{
    var httpContext = context.Get<HttpContextBase>(typeof(HttpContextBase).FullName);
    httpContext.SetSessionStateBehavior(SessionStateBehavior.Required);
    return next();
});
app.UseStageMarker(PipelineStage.MapHandler);
```

## Replacing the default cookie manager

To prevent conflicts between OWIN and ASP.NET cookies, replace the default cookie manager with the following implementation:

```CSHARP
public class SystemWebCookieManager : ICookieManager
{
    public string GetRequestCookie(IOwinContext context, string key)
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }

        var webContext = context.Get<HttpContextBase>(typeof(HttpContextBase).FullName);
        var cookie = webContext.Request.Cookies[key];
        return cookie == null ? null : cookie.Value;
    }

    public void AppendResponseCookie(IOwinContext context, string key, string value, CookieOptions options)
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }
        if (options == null)
        {
            throw new ArgumentNullException("options");
        }

        var webContext = context.Get<HttpContextBase>(typeof(HttpContextBase).FullName);

        bool domainHasValue = !string.IsNullOrEmpty(options.Domain);
        bool pathHasValue = !string.IsNullOrEmpty(options.Path);
        bool expiresHasValue = options.Expires.HasValue;

        var cookie = new HttpCookie(key, value);
        if (domainHasValue)
        {
            cookie.Domain = options.Domain;
        }
        if (pathHasValue)
        {
            cookie.Path = options.Path;
        }
        if (expiresHasValue)
        {
            cookie.Expires = options.Expires.Value;
        }
        if (options.Secure)
        {
            cookie.Secure = true;
        }
        if (options.HttpOnly)
        {
            cookie.HttpOnly = true;
        }

        webContext.Response.AppendCookie(cookie);
    }

    public void DeleteCookie(IOwinContext context, string key, CookieOptions options)
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }
        if (options == null)
        {
            throw new ArgumentNullException("options");
        }

        AppendResponseCookie(
            context,
            key,
            string.Empty,
            new CookieOptions
            {
                Path = options.Path,
                Domain = options.Domain,
                Expires = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            });
    }
}
```

The `SystemWebCookieManager` can be registered in `DotvvmStartup.cs` using the `ConfigureServices` method:

```
public void ConfigureServices(IDotvvmServiceCollection services)
{
    ...
    services.Services.AddSingleton<ICookieManager, SystemWebCookieManager>();
}
```
