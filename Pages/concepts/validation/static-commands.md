# Validation in static commands

As of DotVVM 4.2, it is possible to enable validation in [`staticCommand` bindings](../respond-to-user-actions/static-commands).

The validation must be explicitly enabled for each static command using the `AllowStaticCommand(StaticCommandValidation)` attribute on the invoked method.
The validation **is not affected by `Validation.Target` and `Validation.Enabled` properties**, only the data passed into the method arguments is validated.
Although you can attach errors to any property in the view model using the `AddRawError` method, you can only read the data which were provided in the method arguments.

Every time a validated static command is called, all client-side error are cleared – for this reason, the validation should only be enabled on static command which need it.
Specifically, validation should be disabled on background tasks, lazy loading or autocompletion requests.

### Manual validation

Manual validation is the equivalent of adding errors to `Context.ModelState` in commands.
In static commands, the `StaticCommandModelState` class is used instead. This class can be initialized anywhere, access to the `IDotvvmRequestContext` isn't required.

The available methods are:
* `AddArgumentError("myArgument", "Message")` – attaches error to the argument value.
* `AddArgumentError(() => myArgument.SomeProperty[3].Name, "Message")` – attaches error to the `Name` property of the nested object. Note that `myArgument` must really be a method argument, local variables will not work.
* `AddRawArgumentError("myArgument", "SomeProperty/3/Name")` – same as above, but allows specifying the **relative** validation path manually.
* `AddRawError("/RootProperty", "Message")` – attaches error to the property specified using a view model **absolute** path - in this case `RootProperty` on the page view model.
* `FailOnInvalidModelState()` – if any errors were added, throws an exception to interrupt the request and returns the validation errors to the client.

For example, this is how we can validate the method arguments using few basic rules:

```csharp
[AllowStaticCommand(StaticCommandValidation.Manual)]
public static string DoSomething(MyViewModel vm, string name)
{
    var modelState = new StaticCommandModelState();
    if (vm.AProperty > 10)
        modelState.AddArgumentError(() => vm.AProperty, "Must be less than 10.");
    if (string.IsNullOrWhiteSpace(name))
        modelState.AddArgumentError(() => name, "Please specify a name.");
    modelState.FailOnInvalidModelState();
    // ...
}
```

DotVVM automatically maps the argument errors onto the page viewmodel.
Note that in some cases it might not be possible - for example if we'd call the method using `{staticCommand: DoSomething(_this, "")}`, we'd get the error on the empty string constant.
In such cases, the request will fail with HTTP 500 and client-side `dotvvm.events.error` will be called.
You will want to avoid this situation by passing the arguments using simple property access expression.
But, if you make a mistake it is "only a cosmetic" issue, the user will not be able to bypass validation because of it. 

### Automatic validation

When you use `StaticCommandValidation.Automatic` instead of `Manual`, DotVVM will recursively validate all arguments before calling the method.
Both `ValidationAttribute`s and `IValidatableObject` are checked, same as in `command` invocations.
This setting is similar to the default behavior of `Validation.Target` in commands.

The above example can be thus simplified to

```
[AllowStaticCommand(StaticCommandValidation.Automatic)]
public static string DoSomething(MyViewModel vm, [Required] string name)
{
}

...

public class MyViewModel {
    [Range(0, 10)]
    public int AProperty { get; set; }
}
```

In both command and staticCommand invocations with automatic validation, the validation is performed by `IViewModelValidator` from DI.
You can use this extensibility point to implement custom general validation mechanisms.

> Classic `command` bindings first validate these attributes client-side, but this is currently not implemented for staticCommands.


## See also

* [Validation overview](overview)
* [Validation controls](controls)
    * [Validator control](~/controls/builtin/Validator)
    * [ValidationSummary control](~/controls/builtin/ValidationSummary)


