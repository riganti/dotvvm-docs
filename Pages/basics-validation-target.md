# Validation Target

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

## Disabling Validation

You can also disable validation on a part of the page or on a specific control, by using `Validation.Enabled="false"`. You often need to do this e.g. for delete 
and cancel buttons where the values in the form don't need to be valid. Also, you might need this e.g. on the `Changed` event of `TextBox` when you need to pre-fill some values for the user, but the form may not be completed yet and thus it is not valid at that time.

```DOTHTML
<dot:TextBox Text="{value: Address}" Changed="{command: GetGpsLocationForAddress()}"
             Validation.Enabled="false" />
<!-- There are additional fields in the form which are required, but we need the command to be executed even if these fields are empty. -->
```

> The validation is enabled by default. If you are using validation attributes in your viewmodel and any button in the page doesn't do the postback,
> it's because the viewmodel is not valid and you have no `ValidationSummary` or `Validator` control to display the errors. 

> If you turn on the **debug mode** in the [configuration](/docs/tutorials/basics-configuration/{branch}), a red notification in the top right corner of the screen appears if the postback was canceled because of validation errors.

