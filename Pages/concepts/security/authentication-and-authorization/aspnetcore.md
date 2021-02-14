# ASP.NET Core Authentication

> This section is applicable if your application uses ASP.NET Core. 
> For OWIN stack, visit the [Using OWIN Security for Authentication](/docs/tutorials/advanced-owin-security/{branch}).

To set up the standard cookie authentication, just add this snippet in the `Startup.cs` file.

```CSHARP
public void ConfigureServices(IServiceCollection services)
{
    services.AddAuthentication(sharedOptions =>
    {
        sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => 
    {
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToReturnUrl = c => DotvvmAuthenticationHelper.ApplyRedirectResponse(c.HttpContext, c.RedirectUri),
            OnRedirectToAccessDenied = c => DotvvmAuthenticationHelper.ApplyStatusCodeResponse(c.HttpContext, 403),
            OnRedirectToLogin = c => DotvvmAuthenticationHelper.ApplyRedirectResponse(c.HttpContext, c.RedirectUri),
            OnRedirectToLogout = c => DotvvmAuthenticationHelper.ApplyRedirectResponse(c.HttpContext, c.RedirectUri)
        };
        options.LoginPath = "/login";
    });
	// ...
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    app.UseAuthentication();
    // ...
}
```

> Please note that authentication middlewares should be always registered **before DotVVM**. The authentication middleware needs to determine the current user (e.g. by parsing the authentication token from the cookie) before DotVVM takes control of the HTTP request. 

> The `DotvvmAuthenticationHelper.ApplyRedirectResponse` method is used to perform the redirect because DotVVM uses a different way to handle redirects. Because the HTTP requests invoked by the command bindings are done using AJAX, DotVVM cannot return the HTTP 302 code. Instead, it returns HTTP 200 with a JSON object which instructs DotVVM to load the new URL.

## Login Page with ASP.NET Core Cookie Authentication

In the login page, you need to verify the user credentials and create the `ClaimsIdentity` object that represents the logged user's identity. Then, you need to pass the identity to the `Context.GetAuthentication().SignInAsync` method:

```CSHARP
public class LoginViewModel : DotvvmViewModelBase
{
    public string UserName { get; set; }
    public string Password { get; set; }        

    public async Task Login()
    {
        if (VerifyCredentials(UserName, Password)) 
        {
            // the CreateIdentity is your own method which creates the IIdentity representing the user
            var identity = CreateIdentity(UserName);
            await Context.GetAuthentication().SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            Context.RedirectToRoute("Default");        
        }
    }

    private bool VerifyCredentials(string username, string password) 
    {
        // verify credentials and return true or false
    }

    private ClaimsIdentity CreateIdentity(string username) 
    {
        var identity = new ClaimsIdentity(
            new[]
            {
                new Claim(ClaimTypes.Name, username),

                // add claims for each user role
                new Claim(ClaimTypes.Role, "administrator"),
            },
            CookieAuthenticationDefaults.AuthenticationScheme);
        return identity;
    }
}
```

## Using Azure Active Directory

In order to use Azure Active Directory as the identity provider, you can use the Open ID Connect middleware using the `Microsoft.AspNetCore.Authentication.OpenIdConnect` package.

For the details, visit the [DotVVM with Azure AD Authentication Sample](https://github.com/riganti/dotvvm-samples-azuread-auth).
