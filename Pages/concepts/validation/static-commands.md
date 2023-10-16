# Validation in static commands

As of DotVVM 4.2, it is possible to enable validation in [`staticCommand` bindings](../respond-to-user-actions/static-commands).

The validation must be explicitly enabled for each static command using the `AllowStaticCommand(StaticCommandValidation)` attribute.
The validation is not affected by `Validation.Target` and `Validation.Enabled` properties, only the data passed into the method arguments is validated.
Although you can attach errors to any property in the view model using the `AddRawError` method, you can only read the data which were provided in the method arguments.

Every time a validated static command is called, all client-side error are cleared - for this reason, the validation should only be enabled on static command which need it.
Specifically validation should be disabled on background tasks, lazy loading or autocompletion requests.

### Manual validation

Manual validation is the equivalent of adding errors to `Context.ModelState` in commands.
In static commands, the `StaticCommandModelState` class is used instead. This class can be initialized anywhere, no access to the request context is required.

The available methods are:
* `AddArgumentError("myArgument", "Message")` - attaches error to the argument value.
* `AddArgumentError(() => myArgument.SomeProperty[3].Name, "Message")` - attaches error to the `Name` property of some nested object. Note that myArgument must really be an method argument, local variables will not work.
* `AddRawArgumentError("myArgument", "SomeProperty/3/Name")` - same as above, but allows specifying the validation path manually
* `AddRawError("/RootProperty", "Message")` - attaches error to the `RootProperty` on the page view model.
* `FailOnInvalidModelState()` - if any errors were added, throws an exception to interrupt the request and returns the validation errors to the client.

For example, this is how we can validate some basic rules:

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
In such cases, the request will fail with HTTP 500.

### Automatic validation

When you use `StaticCommandValidation.Automatic` instead of `Manual`, DotVVM will recursively validate all arguments before calling the method.
Both `ValidationAttribute`s and `IValidatableObject` are checked.
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

> Classic `command` bindings first validate these attributes client-side, but this is currently not implemented for staticCommands.


## See also

* [Validation overview](overview)
* [Validation controls](controls)
    * [Validator control](~/controls/builtin/Validator)
    * [ValidationSummary control](~/controls/builtin/ValidationSummary)


