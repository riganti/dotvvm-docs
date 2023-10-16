# Extensibility

The validation engine in DotVVM offers several extensibility points. You can use your own validation attributes, and you can interact with the validation engine on the client-side.

## Custom validation attributes

You can build your own validation attributes by implementing the `IValidationAttribute` class, or inheriting from the `ValidationAttribute` base class. See [Custom attributes](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-5.0#custom-attributes) page in the official docs.

## Client-side validation

Providing a client-side validation for custom attributes is possible by providing a custom implementation of the `IValidationRuleTranslator` interface, and then by registering a custom rule into `dotvvm.validation.rules` collection, but the process is quite complicated. We plan to make it easier in the future versions of DotVVM.

It is also possible to add any validation errors client-side.
This can be used to implement validation in JS Commands, or to implement custom validation logic before commands.

The `dotvvm.validation.removeErrors("/")` function is used to remove existing validation errors.
The `"/"` is the root path of the removal, this argument can be used to clear only certain parts of the view model.
Any number of paths can be specified.

The `dotvvm.validation.addErrors(errors)` adds new errors.
The errors argument should be an array of objects with
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

You can subscribe to the client-side events called `dotvvm.validation.events.validationErrorsChanged` to be notified if the validation error collection has been changed. 

You can also access the collection of all validation errors using `dotvvm.validation.allErrors`. 

## See also

* [Validation overview](overview)
* [Validation controls](controls)
* [Validation controls](target)
* [Client-side validation](client-side)


