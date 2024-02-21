# Validation target

You often need to validate only a part of the viewmodel. For example, if the page holds two forms, you may want to validate one of them independently on the other.

In DotVVM, there is the `Validation.Target` property. Using this property you can specify the validation target (the object which gets validated) for commands in a specific part of the page:

```DOTHTML
<form Validation.Target="{value: Customer}">

    <!-- All postbacks made from inside of this form will only validate 
    the Customer object instead of validating the entire viewmodel. -->

</form>
```

You can apply the `Validation.Target` property to any HTML element, and to almost all DotVVM controls. In the example, for all postbacks made by controls in the `form` element, the validation rules will be checked only on the `Customer` property of the viewmodel. 

> The `AddModelError` method on the context object is used to report errors anywhere in the viewmodel, even outside of the validation target.

## ValidationSummary

This applies also to the [ValidationSummary](~/controls/builtin/ValidationSummary) control. If this control or some of its parents has the `Validate.Target` property set, the `ValidationSummary` will display only the errors from the validation target.

If you don't set the `Validation.Target`, entire viewmodel is validated - the default value for the property is `{value: _root}`.

> The `Validation.Target` must always point to an object. Collections, or properties of primitive types (strings, numbers, dates etc.) are not allowed as validation targets.  

## See also

* [Validation overview](overview)
* [Validation controls](controls)
* [Validator control](~/controls/builtin/Validator)
* [ValidationSummary control](~/controls/builtin/ValidationSummary)


