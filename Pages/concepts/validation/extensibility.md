# Extensibility

The validation engine in DotVVM offers several extensibility points. You can use your own validation attributes, and you can interact with the validation engine on the client-side.

## Custom validation attributes

You can build your own validation attributes by implementing the `IValidationAttribute` class, or inheriting from the `ValidationAttribute` base class. See [Custom attributes](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-5.0#custom-attributes) page in the official docs.

## `IValidatableObject`

Each validation view model can implement the `IValidatableObject` interface, see [Validation overview](./overview#ivalidatableobject).

## `IViewModelValidator`

DotVVM validates the view model using this interface, and you can replace it by registering your own implementation in DI (as a singleton).
This is more advanced way to completely change how automatic server-side validation is performed.
It may be useful if you want to use something different from the data annotation attributes to validate your view models.
The default implementation is [`ViewModelValidator`](https://github.com/riganti/dotvvm/blob/main/src/Framework/Framework/ViewModel/Validation/ViewModelValidator.cs).

## Client-side validation

Providing a client-side validation for custom attributes is possible by providing a custom implementation of the `IValidationRuleTranslator` interface, and then by registering a custom rule into `dotvvm.validation.rules` collection, but the process is quite complicated. We plan to make it easier in the future versions of DotVVM.

It is also possible to add any validation errors client-side.
This can be used to implement validation in JS Commands, or to implement custom validation logic before a command is invoked.

The `dotvvm.validation.removeErrors("/")` function is used to remove existing validation errors.
The `"/"` is the root path of the removal, this argument can be used to clear only certain parts of the view model.
Any number of paths can be specified.

The `dotvvm.validation.addErrors(errors)` adds new errors.
The argument should be an array of objects with
* `errorMessage` - a string displayed to the user
* `propertyPath` - an absolute validation path of the invalid property, e.g. `/SomeArray/2/InvalidProperty`.

```JAVASCRIPT
myCommand() {
    const model = dotvvm.state.MyForm
    dotvvm.validation.removeErrors("/MyForm")
    const errors = []
    if (model.FirstName.length > model.LastName.length)
        errors.push({ errorMessage: "Persons with longer first name than their last name cannot be registered.", propertyPath: "/MyForm/FirstName"})
    if (errors.length > 0)
    {
        dotvvm.validation.addErrors(errors)
        return
    }
}
```

### Validation events

You can subscribe to the client-side events called `dotvvm.validation.events.validationErrorsChanged` to be notified when the validation error collection is changed. The error collection is in `dotvvm.validation.allErrors`. 

## See also

* [Validation overview](overview)
* [Validation controls](controls)
* [Validation target](target)
* [Client-side validation](client-side)


