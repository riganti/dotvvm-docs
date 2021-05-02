# OWIN authentication

> This section is applicable if your application uses OWIN with .NET Framework. For ASP.NET Core, visit the [Authentication in ASP.NET Core](aspnetcore) chapter.

In general, the authentication is configured in the `Startup.cs` file by registering an authentication middleware before the DotVVM middleware.

## Cookie authentication

The most common way of authenticating is using the cookie authentication, which can be configured like this:

```CSHARP
app.UseCookieAuthentication(new CookieAuthenticationOptions()
{
    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
    LoginPath = new PathString("/login"),       // don't use ~/login here - the ~ in URLs is a DotVVM feature, OWIN security doesn't know it
    Provider = new CookieAuthenticationProvider()
    {
        OnApplyRedirect = e => DotvvmAuthenticationHelper.ApplyRedirectResponse(e.OwinContext, e.RedirectUri)
    }
});

app.UseDotVVM<DotvvmStartup>(...);
```

> Please note that authentication middlewares should be always registered **before DotVVM**. The authentication middleware needs to determine the current user (e.g. by parsing the authentication token from the cookie) before DotVVM takes control of the HTTP request. 

> The `DotvvmAuthenticationHelper.ApplyRedirectResponse` method is used to perform the redirect because DotVVM uses a different way to handle redirects. Because the HTTP requests invoked by the command bindings are done using AJAX, DotVVM cannot return the HTTP 302 code. Instead, it returns HTTP 200 with a JSON object which instructs DotVVM to load the new URL.

### Create the login page

In the login page, you need to verify the user credentials and create the `ClaimsIdentity` object that represents the logged user's identity. Then, you need to pass the identity to the `GetAuthentication().SignIn` method:

```CSHARP
public class LoginViewModel : DotvvmViewModelBase
{
    public string UserName { get; set; }
    public string Password { get; set; }        

    [FromQuery("returnUrl")]
    public string ReturnUrl { get; set; }

    public void Login() 
    {
        if (VerifyCredentials(UserName, Password)) 
        {
            // the CreateIdentity is your own method which creates the IIdentity representing the user
            var identity = CreateIdentity(UserName);
            Context.GetAuthentication().SignIn(identity);

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
            DefaultAuthenticationTypes.ApplicationCookie);
        return identity;
    }
}
```

## Social providers authentication

If you want to let the users to sign in using Facebook or other third party identity provider, you need to use a different middleware. 

First, you need to install the appropriate NuGet package, for example `Microsoft.Owin.Security.Facebook`.

In order to redirect to the Facebook login page, you can use this code:

```CSHARP
public void LoginWithFacebook()
{
    var properties = new AuthenticationProperties()
    {
        RedirectUri = "http://myapp.com/Register"
    };
    Context.GetAuthentication().Challenge(properties, "Facebook");
    Context.InterruptRequest();
}
```

Please note that after the `Authentication.Challenge` call which sets the HTTP code to 403, we need to call
`Context.InterruptRequest()`. This will tell DotVVM to stop executing this request and not to manipulate with the HTTP response.

To configure the Facebook authentication, use the following code in the `Startup.cs`:

```CSHARP
app.UseFacebookAuthentication(new FacebookAuthenticationOptions()
{
    ...
    Provider = new FacebookAuthenticationProvider()
    {
        OnApplyRedirect = context => DotvvmAuthenticationHelper.ApplyRedirectResponse(context.OwinContext, context.RedirectUri),
        ...
    }
});
```

## Azure Active Directory authentication

In order to use Azure Active Directory as the identity provider, you can use the Open ID Connect middleware using the `Microsoft.Owin.Security.OpenIdConnect` package.

For the details, visit the [DotVVM with Azure AD Authentication Sample](https://github.com/riganti/dotvvm-samples-azuread-auth).

## See also

* [Authentication & authorization](overview)
* [Sample: Azure Active Directory authentication](https://github.com/riganti/dotvvm-samples-azuread-auth)