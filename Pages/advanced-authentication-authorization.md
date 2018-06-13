# Authentication and Authorization

There are some special things you need to be aware of before you start working 
with the authentication.

**DotVVM** doesn't implement any specific authentication features, however it 
can use advantage of the common ASP.NET authentication system. The fact, whether
the user is authenticated and in which role he is, is determined by the 
`HttpContext.User` property. It's the same as in other ASP.NET technologies.

In OWIN, you can use the `Microsoft.Owin.Security.*` NuGet packages to configure the cookie authentication or different authentication mechanisms, like Open ID.

In ASP.NET Core, you can use the `Microsoft.AspNetCore.Authentication.*` NuGet packages for the same thing.


## Restricting Access to ViewModels and ViewModel Methods

In **DotVVM**, you can use the `[Authorize]` attribute from the `DotVVM.Framework.Runtime.Filters` namespace. You can use it to decorate the viewmodel class, or a specific viewmodel method referenced by a command binding.

> Only the method called from a command binding and the page viewmodel class are checked for the Authorize attribute. If you call the method from C# code, the attribute is not checked automatically.

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
        // The page with this viewmodel will return 403 Forbidden
        // if the user is not authenticated.

        // No commands will be accepted.
    }
}
```

Also, you can limit the access to a specific user roles.

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

        // Please note that if you call the DeleteUser from your own code, the Authorize attribute will not be checked.
    }
}
```

You can find more details about the authentication providers in the following chapters:

* [Using OWIN Security for Authentication](/docs/tutorials/advanced-owin-security/{branch})
* [Using Microsoft ASP.NET Core Authentication](/docs/tutorials/advanced-aspnetcore-authentication/{branch}) 