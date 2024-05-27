# ASP.NET Core authentication

> This section is applicable if your application uses ASP.NET Core. For OWIN, visit the [Authentication in OWIN](owin) chapter.

First, you need to configure the authentication behavior in the `ConfigureServices` method in the `Startup.cs` file.

Second, you need to apply the authentication and authorization middlewares in the `Configure` method in `Startup.cs`. These should be registered before the DotVVM middleware.

## Configure the cookie authentication

The most popular way is to use the standard cookie authentication.

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
	...
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    app.UseAuthentication();
    app.UseAuthorization();
    ...
    app.UseDotVVM<DotvvmStartup>(...);
}
```

> Please note that authentication middlewares should be always registered **before DotVVM**. The authentication middleware needs to determine the current user (e.g. by parsing the authentication token from the cookie) before DotVVM takes control of the HTTP request. 

> The `DotvvmAuthenticationHelper.ApplyRedirectResponse` method is used to perform the redirect because DotVVM uses a different way to handle redirects. Because the HTTP requests invoked by the command bindings are done using AJAX, DotVVM cannot return the HTTP 302 code. Instead, it returns HTTP 200 with a JSON object which instructs DotVVM to load the new URL.

### Create the login page

In the login page, you need to verify the user credentials and create the `ClaimsIdentity` object that represents the logged user's identity. Then, you need to pass the identity to the `Context.GetAuthentication().SignInAsync` method:

```CSHARP
public class LoginViewModel : DotvvmViewModelBase
{
    public string UserName { get; set; }
    public string Password { get; set; }

    [FromQuery("returnUrl")]
    public string ReturnUrl { get; set; }

    public async Task Login()
    {
        if (VerifyCredentials(UserName, Password)) 
        {
            // the CreateIdentity is your own method which creates the IIdentity representing the user
            var identity = CreateIdentity(UserName);
            await Context.GetAuthentication().SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            
            if (!string.IsNullOrEmpty(ReturnUrl)) 
            {
                Context.RedirectToLocalUrl(ReturnUrl);
            }
            else 
            {
                Context.RedirectToRoute("Default");
            }       
        }
        else 
        {
            // show the error to the user
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

### ASP.NET Core Identity

If you plan to use [ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-5.0&tabs=visual-studio), the logic for verifying credentials and creating the `ClaimsIdentity` is already implemented.

The viewmodel for the login page can look like this:

```CSHARP
public class LoginViewModel : DotvvmViewModelBase
{
    private readonly SignInManager signInManager;

    public string UserName { get; set; }
    public string Password { get; set; }

    [FromQuery("returnUrl")]
    public string ReturnUrl { get; set; }

    public LoginViewModel(SignInManager signInManager) 
    {
        this.signInManager = signInManager;
    }

    public async Task Login()
    {
        var result = await _signInManager.PasswordSignInAsync(UserName, Password, false, true);
        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(ReturnUrl)) 
            {
                Context.RedirectToLocalUrl(ReturnUrl);
            }
            else 
            {
                Context.RedirectToRoute("Default");
            }
        }
        else 
        {
            // show the error to the user
        }
    }
}
```

## Azure Active Directory authentication

In order to use Azure Active Directory as the identity provider, you can use the Open ID Connect middleware using the `Microsoft.AspNetCore.Authentication.OpenIdConnect` package.

For the details, visit the [DotVVM with Azure AD Authentication Sample](https://github.com/riganti/dotvvm-samples-azuread-auth).

## See also

* [Authentication & authorization](overview)
* [Sample: Azure Active Directory authentication](https://github.com/riganti/dotvvm-samples-azuread-auth)
