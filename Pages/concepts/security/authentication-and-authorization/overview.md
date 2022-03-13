# Authentication and authorization overview

**DotVVM** mostly relies on authentication and authorization mechanisms that are available in ASP.NET Core or OWIN. Every type of authentication and authorization which is available in the ASP.NET platform, is available in DotVVM.

## Configure authentication

The configuration of authentication is commonly done in `Startup.cs` file, and it is not very different from how it's done in ASP.NET MVC or other frameworks.

Because of the differences between ASP.NET Core and OWIN, different NuGet packages are used:

* In [ASP.NET Core](aspnetcore), the `Microsoft.AspNetCore.Authentication.*` NuGet packages are used.
* In [OWIN](owin), the `Microsoft.Owin.Security.*` NuGet packages are used. 

See the [ASP.NET Core authentication](aspnetcore) and [OWIN authentication](owin) for more info.

## Restrict access to viewmodels and their methods

Once the authentication and authorization is configured, you can restrict access to viewmodels and their methods based on the user identity, role memberships, and more.

The easiest way is to use the `[DotVVM.Framework.Runtime.Filters.Authorize]` attribute. 

You can use the attribute to decorate:

* the viewmodel class; the entire page will be accessible only to the authorized users

* specific viewmodel methods which are called from the [command binding](~/pages/concepts/respond-to-user-actions/commands); the commands will fail for unauthorized users

> The `Authorize` attribute is checked **only if method is called from a command binding**. If you call the method from another method in your C# code, the attribute won't be checked automatically.

> ASP.NET MVC and other framework are also using the `Authorize` attribute, but they have their own implementation. When adding the `using` statement, make sure the `Authorize` attribute comes from the `DotVVM.Framework.Runtime.Filters` namespace.

```CSHARP
using System;
using System.Threading.Tasks;
using DotvvmWeb.BL.Facades;
using DotVVM.Framework.Runtime.Filters;

namespace DotvvmDemo.ViewModels
{
    [Authorize]
    public class AdminViewModelBase : DotvvmViewModelBase
    {
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
        [Authorize(Roles = new[] { "Admin" })]
        public void DeleteUser(int id)
        {
            // Only the users with the Admin role will be able
            // to call this method from the command binding.
        }

        // Please note that if you call the DeleteUser method from your own code, the Authorize attribute will not be checked.
        // The attribute is checked **only** if the method is called from a command binding:
        // <dot:Button Text="Delete" Click="{command: DeleteUser(Id)}" />
    }
}
```

It is possible to apply the `Authorize` to the base class of the viewmodel. This is useful when you need to restrict access for an entire section of the application, for example an admin area. All pages in the admin area will use the same [master page](~/pages/concepts/layout/master-pages), so the `Authorize` attribute is often applied to the viewmodel of the master page. 

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
