# Validation controls

After you defined the [validation rules](overview) and specified the [validation target](target), you need to specify how the validation errors will be presented to the user.

## ValidationSummary

The easiest way is usually to display a list of all validation errors in the viewmodel. You can use the [ValidationSummary](~/controls/builtin/ValidationSummary) control.
 
```DOTHTML
<dot:ValidationSummary />
```

Because of performance reasons, the `ValidationSummary` control displays only the errors attached to its `Validation.Target` properties, and doesn't look in its children.

If you want the `ValidationSummary` to show all the errors from the child objects in the validation target, you can set the `IncludeErrorsFromChildren` property to `true`.

Some validation errors have no reasonable property to attach to (for example, we don't know from a generic error reporting that the user account could not be created, which property in the viewmodel caused the issue). In such cases, the `ModelState` errors are attached to the entire validation target object. 

If you want to display the errors attached to the `Validation.Target` object itself, set `IncludeErrorsFromTarget` to `true`.

## Validator control

The second options is to use the [Validator](~/controls/builtin/Validator}) to display an error attached to a particular viewmodel property.

```DOTHTML
<dot:TextBox Text="{value: NewTaskTitle}" />
<dot:Validator Value="{value: NewTaskTitle}">*</dot:Validator>
```

This will display the `*` character when the property contains invalid value. The `Value` property contains a [value binding](~/pages/concepts/data-binding/value-binding) to a property which is being validated.

The `Validator` control has several properties that let you set how the error is reported. You can combine them as you need:

* `HideWhenValid` - set to `false` if you need this control to remain visible even when the field is valid. By default, the control is hidden when the field is valid.

* `InvalidCssClass` - the CSS class specified in this property will be set to this control when the field is not valid. 

* `ShowErrorMessageText` - the text of the error message will be displayed inside this control.

* `SetToolTipText` - the text of the error message will be set as the `title` attribute of the control.

## Attached properties

In some cases, you may need to apply the behavior on existing elements in the page. 

For example, if a property is not valid, you need to apply a CSS class to a `div` element which wraps the form field:

```DOTHTML
<div Validator.InvalidCssClass="has-error" 
     Validator.Value="{value: FirstName}">
    First name:
    <dot:TextBox Text="{value: FirstName}" />
</div>
```

The validation behavior will be applied only on elements which has the `Validator.Value` property set. Unless you set this property on some element, nothing will happen.

Additionally, you need to use the `Validator.HideWhenValid`, `Validator.InvalidCssClass`, `Validator.ShowErrorMessageText`, or `Validator.SetToolTipText` properties to define what will happen to the element. 

These properties are inherited to the child elements, so you may set them once on the form or page level. 

```DOTHTML
<!-- we specify the behavior of validation globally for the entire form -->
<form Validator.InvalidCssClass="has-error">    

    <div Validator.Value="{value: FirstName}">  <!-- the Validator.Value marks the element which will get the invalid CSS class -->
        First Name: <dot:TextBox Text="{value: FirstName}" />
    </div>
    <div Validator.Value="{value: LastName}">
        Last Name: <dot:TextBox Text="{value: LastName}" />
    </div>

</form>
```

If you want to set the `Validation.InvalidCssClass` property for the entire application, you can apply it on the `<body>` element in the master page. 
You can of course override the CSS class on any child element.

## See also

* [Validation overview](overview)
* [Validator control](~/controls/builtin/Validator)
* [ValidationSummary control](~/controls/builtin/ValidationSummary)
* [Validation target](target)

