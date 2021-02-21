# Validation target

You often need to validate only a part of the viewmodel. In DotVVM, there is the `Validation.Target` property. Using this property you can specify the validation target (the object which gets validated) for a specific part of the page:

```DOTHTML
...
    <fieldset Validation.Target="{value: Customer}">
    
        <!-- All postbacks made from the inside of this fieldset will only validate 
             the Customer object instead of whole viewmodel. -->

    </fieldset>
...
```

You can apply this property to any HTML element and to almost all DotVVM controls. In the example, for all postbacks made by controls in the `fieldset`, 
the validation will be executed only on the `Customer` property of the viewmodel. 

This applies also to the `ValidationSummary` control. If this control or some of its parents has the `Validate.Target` property set, the `ValidationSummary` will display only the errors from the validation target.

If you don't set the `Validation.Target`, entire viewmodel is validated. The default value for `Validation.Target` is `_root`.

> The `Validation.Target` must always point to a non-array object. Collections or primitive types (strings, numbers, dates etc.) are not allowed as validation targets.  


