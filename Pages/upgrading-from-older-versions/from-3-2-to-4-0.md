# Upgrading from 3.2 to 4.0

See [Release notes of DotVVM 4.0](https://github.com/riganti/dotvvm/releases/tag/v4.0.0) for complete list of changes.

## Breaking changes

### Dropped support for Internet Explorer 11

**DotVVM 4.0 doesn't support Internet Explorer any more.** If you need to use this ancient browser, please stay on DotVVM 3.2.

### Upgrade to .NET Core 3.1 or .NET Framework 4.7.2

DotVVM is now compiled against `net472` and `netstandard2.1`, which allows us to take advantage of newer features in the framework. 

If you are using .NET Framework, upgrade to **.NET Framework 4.7.2 or newer**.

In case of .NET Core, upgrade to **.NET Core 3.1**, or better **.NET 6**. 

### Validation changes

If you have been adding validation errors into `Context.ModelState.Errors` collection directly, this is now not supported. Please use `Context.AddModelError(this, vm => vm.SomeProperty.SomeProperty...)` to add errors in the collection.

If you need to build your validation paths as strings (in `IValidatableObject` implementation), use the `CreateValidationResult` extension method which will help you to build the path.

```CSHARP
public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
{
    yield return this.CreateValidationResult<YourViewModel>(validationContext,
        "Error message", 
        t => t.Child.InvalidProperty
    );
}
```

If you specify the validation path as a string, please note that the format of property paths was changed - instead of using Knockout JS expressions (`Property().Child()[2]().Title`), DotVVM now uses slash-separated paths (`Property/Child/2/Title`). If you need these paths, please use the `Context.AddRawModelError` extension method.

See [Validation](~/pages/concepts/validation/overview) section for more info.

### HTTP security headers enabled by default

Some HTTP security headers are enabled by default. See the [Security headers](~/pages/concepts/security/security-headers) chapter for more info.

### Use Context.Authorize() instead of the [Authorize] attribute

The `[Authorize]` attribute is now deprecated as it behaved unintuitively on some places (for example, if you call a method protected by this attribute yourself, the attribute is not enforced).

Use the `context.Authorize(...)` extension method in the viewmodel `Init` method or in command / staticCommand methods instead. 

```CSHARP
public class AdminViewModelBase : DotvvmViewModelBase
{

    public override async Task Init() 
    {
        // ASP.NET Core has new asynchronous authorization API:
        await Context.Authorize();

        // OWIN has a synchronous Authorize method
        // Context.Authorize();

        await base.Init();  // always call base.Init() - another authorization checks can be specified in the base page 
    }
}
```

The attribute still works and we don't plan to remove it, but it's marked obsolete - we recommend to use the new method.

See the [Authentication and authorization](~/pages/concepts/security/authentication-and-authorization/overview) chapter for more info.

### Removed support for virtual control properties 

Prior to DotVVM 4.0, DotVVM controls can specify properties without the property descriptors (`DotvvmProperty.Register` or `DotvvmPropertyGroup.Register`) - DotVVM inferred these descriptors itself.

The feature was not used frequently and caused problems on some places, so we decided to remove it. Add the property descriptors with proper calls to `DotvvmProperty.Register`.

### SetValue doesn't update source in control properties

If you used the `control.SetValue` method on a property which had the value binding specified, the method tried to update the underlying viewmodel property. However, this behavior was unreliable, so the call now always replaces the value binding with the new value. 

If you want to update the bound viewmodel property, you have to do it explicitly using `control.SetValueToSource` method.

### Changed signature of SetControlProperty method in server-side styles

The `SetControlProperty` method in the [server-side style](~/pages/concepts/dothtml-markup/server-side-styles) registration has now a different signature. 
The call `.SetControlProperty<MyControl>(TheProperty)` is replaced by `.SetControlProperty(TheProperty, new MyControl())`. Also, you'll need to add additional using so the extension method is found.

### DotVVM error page displays only errors that occurred in DotVVM

The DotVVM error page now only displays errors coming from DotVVM pages and presenters, not from the subsequent middlewares. If you are using both DotVVM and other technology in your backend, add the ASP.NET Error Page too.

### Repeater uses HTML templates by default

In the client-side rendering mode, the [Repeater](~/controls/builtin/Repeater) control renders its `ItemTemplate` into a separate `<template id=...>` element instead of placing the template inside the wrapper tag. If it poses a problem, it can be configured by setting `RenderAsNamedTemplate=false`.

### RouteLink and Literal changes in server-rendering mode

The [RouteLink](~/controls/builtin/RouteLink) now renders the link even in client-side mode.

The [Literal](~/controls/builtin/Literal) now renders the binding even in server-side rendering mode. Use a resource binding to force it to render the value server-side without generating the Knockout binding expression.

### DotVVM Diagnostics Status Page is now included in the DotVVM framework package

The `DotVVM.Diagnostics.StatusPage` NuGet package is now deprecated - the [Compilation status page](compilation-status-page) page is now included in `DotVVM` package itself.

Also, it is available on a new URL - **`_dotvvm/diagnostics/compilation`**. 

Uninstall the `DotVVM.Diagnostics.StatusPage` package from your project - it's not needed any more.

