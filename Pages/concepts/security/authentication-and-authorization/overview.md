# Authentication and authorization overview

**DotVVM** mostly relies on authentication and authorization mechanisms that are available in ASP.NET Core or OWIN. Every type of authentication and authorization which is available in the ASP.NET platform, is available in DotVVM.

## Configure authentication

The configuration of authentication is commonly done in `Startup.cs` file, and it is not very different from how it's done in ASP.NET MVC or other frameworks.

Because of the differences between ASP.NET Core and OWIN, different NuGet packages are used:

* In [OWIN](owin), the `Microsoft.Owin.Security.*` NuGet packages are used. 
* In [ASP.NET Core](aspnetcore), the built-in authentication mechanism is used.

See the [ASP.NET Core authentication](aspnetcore) and [OWIN authentication](owin) for more info.

## Restrict access to viewmodels and their methods

Once the authentication and authorization is configured, you can restrict access to viewmodels and their methods based on the user identity, role memberships, and more.

This restriction is done by calling the `Context.Authorize()` method in the `Init` stage of the viewmodel, or in the [command](~/pages/concepts/respond-to-user-actions/commands) and [static command](~/pages/concepts/respond-to-user-actions/static-commands) methods.

> Prior to DotVVM 4.0, the `[DotVVM.Framework.Runtime.Filters.Authorize]` attribute was used for this. We've decided to make the attribute obsolete (even though it will remain functional and stay in the framework forever) as its usage was not intuitive in all cases. For example, if you call a method "protected" by this attribute yourself, the attribute is not respected and the method is just called. We wanted to make the authorization explicit.   

The `Context.Authorize()` method can be called in:

* the `Init` method of the viewmodel; the entire page will be accessible only to the authorized users

* specific viewmodel methods which are called from the [command binding](~/pages/concepts/respond-to-user-actions/commands); the commands will fail for unauthorized users

> If you are still using the `[Authorize]` attribute, please note that ASP.NET MVC and other frameworks are also using the `Authorize` attribute, but they have their own implementation. When adding the `using` statement, make sure the `Authorize` attribute comes from the `DotVVM.Framework.Runtime.Filters` namespace.

```CSHARP
using System;
using System.Threading.Tasks;
using DotvvmWeb.BL.Facades;
using DotVVM.Framework.Runtime.Filters;

namespace DotvvmDemo.ViewModels
{
    public class AdminViewModelBase : DotvvmViewModelBase
    {

        public override async Task Init() 
        {
            await Context.Authorize();

            await base.Init();  // always call base.Init() - another authorization checks can be specified in the base page 
        }
        
        // The page with this viewmodel will return 403 Forbidden if the user is not authenticated.
        // No commands to this page will be accepted.

        // Note: In most cases, the 403 response will be caught by the authentication middleware, 
        // and the user will be redirected to the sign in page.
    }
}
```

You can also restrict the access only to specific user roles.

```CSHARP
using System;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Runtime.Filters;

namespace DotvvmDemo.ViewModels
{
    public class AdminViewModelBase : DotvvmViewModelBase
    {
        public async Task DeleteUser(int id)
        {
            await Context.Authorize(roles: new[] { "Admin" });

            // Only the users with the Admin role will be able
            // to call this method from the command binding.
        }

        // Please note that if you call the DeleteUser method from your own code, the Authorize attribute will not be checked.
        // The attribute is checked **only** if the method is called from a command binding:
        // <dot:Button Text="Delete" Click="{command: DeleteUser(Id)}" />
    }
}
```

Note that the `Context.Authorize` method has several optional parameters - you can enforce the user to be a member of specific roles, or comply with specific ASP.NET Core Authorization policies, or be authenticated via specified authentication scheme.

If you want the same permission check for all pages, you can place the `Authorize` method call to the [master page](~/pages/concepts/layout/master-pages) viewmodel. If you override the `Init` method in page viewmodels, make sure to properly call `await base.Init(context);` so the check is applied. 

### Obtaining the context in static commands

You should perform the authorization also in the [static commands](~/pages/concepts/respond-to-user-actions/static-commands). 

If you declare the static command methods in a [static command service](~/pages/concepts/respond-to-user-actions/static-command-services), you can use dependency injection to obtain the `IDotvvmRequestContext` object. This object contains the `Authorize` method.

In case your static command method is static, you need to pass `IDotvvmRequestContext` to the method so it can perform the check. You can resolve the context object in the markup file using the `@service` directive:

```DOTHTML
@service context = IDotvvmRequestContext

<dot:Button Text="Call" Click="{staticCommand: MyMethod(context, ...)"} />
```

```CSHARP
[AllowStaticCommand]
public static async Task MyMethod(IDotvvmRequestContext context, ...)
{
    await context.Authorize();
    ...
}
```

## Render different content for authorized users

Often, you want to provide a different UI to the authenticated users, or to the users which belong to specific roles.

DotVVM provides the [AuthenticatedView](~/controls/builtin/AuthenticatedView) and [RoleView](~/controls/builtin/RoleView) controls which can help with this.

The `AuthenticatedView` can display different content to authenticated and anonymous users:

```DOTHTML
<dot:AuthenticatedView>
    <AuthenticatedTemplate>
        I am authenticated.
    </AuthenticatedTemplate>
    <NotAuthenticatedTemplate>
        I am not authenticated.
    </NotAuthenticatedTemplate>
</dot:AuthenticatedView>
```

The `RoleView` can display different content based on the user's role membership:

```DOTHTML
<dot:RoleView Roles="admin,moderator,tester" HideForAnonymousUsers="false">
    <IsMemberTemplate>
        I am a member.
    </IsMemberTemplate>
    <IsNotMemberTemplate>
        I am not a member.
    </IsNotMemberTemplate>
</dot:RoleView>
```

## See also

* [Authentication & authorization in ASP.NET Core](aspnetcore) 
* [Authentication & authorization in OWIN](owin)
* [AuthenticatedView](~/controls/builtin/AuthenticatedView)
* [RoleView](~/controls/builtin/RoleView)
* [Security headers](../security-headers)
