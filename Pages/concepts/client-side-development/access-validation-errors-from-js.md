# Access validation errors from JS

Validation error can be accessed and modified from client-side Javascript in the `dotvvm.validation.errors` collection:

```
for (const error of dotvvm.validation.errors) {
    console.log(error.errorMessage, "at", error.propertyPath, "value =", error.validatedObservable.state)
}
```

Each error has:

* `errorMessage : string` - message from the validator.
* `propertyPath : string` - validation path of the property with the error. The path is slash-delimited for nested objects and arrays: `MyArray/10/MyProperty`.
* `validatedObservable : KnockoutObservable` - KnockoutObservable with the error, you can primarily use it to obtain the value.

If you want to be notified about changes in the error array, subscribe to the `dotvvm.validation.events.validationErrorsChanged` [event](./dotvvm-javascript-events.md).

## Modification

The validation module has `removeErrors` and `addErrors` methods. The `removeErrors` method simply takes a path where it will clear all errors recursively:

```
// clear all errors on the page
dotvvm.validation.removeErrors("/")
```

The addErrors method accepts an array of `{ errorMessage: string, propertyPath: string }` objects:

```
dotvvm.validation.addErrors([
    {
        propertyPath: "MyObject/MyArray/3/MyProperty",
        validationError: "Server is on fire",
    }
])
```
