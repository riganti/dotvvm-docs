# Validator Controls

## ValidationSummary

To show all errors in the viewmodel, you can use the [ValidationSummary](/docs/controls/builtin/ValidationSummary/{branch}) control.
 
```DOTHTML
<dot:ValidationSummary />
```

Because of performance reasons, the `ValidationSummary` control displays only the errors attached to its `Validation.Target`'s properties and doesn't recurse any further.
If you want the `ValidationSummary` to show all the errors from its `Validation.Target`'s descendant objects, you can set its `IncludeErrorsFromChildren` property to `true`.

And if your desire is to show the errors attached to the `Validation.Target` object itself, set `IncludeErrorsFromTarget` to `true`;

## Validator Control

The second options is to use the [Validator](/docs/controls/builtin/Validator/{branch}) to display errors for an individual field.

```DOTHTML
<dot:TextBox Text="{value: NewTaskTitle}" />
<dot:Validator Value="{value: NewTaskTitle}">*</dot:Validator>
```

This will display the `*` character when the property contains invalid value. The `Value` property contains a [value binding](/docs/tutorials/basics-value-binding/{branch}) to a property which is being validated.

The `Validator` control has several properties that let you set how the error is reported. You can combine them as you need:

* `HideWhenValid` - set to `false` if you need this control to remain visible even when the field is valid. By default, the control is hidden when the field is valid.

* `InvalidCssClass` - the CSS class specified in this property will be set to this control when the field is not valid. 

* `ShowErrorMessageText` - the text of the error message will be displayed inside this control.

* `SetToolTipText` - the text of the error message will be set as the `title` attribute of the control.

## Validator Attached Properties

In many cases, you may need to apply the `Validator` properties on any other element, for example the `<div>`.
If the property is not valid and you need to apply a CSS class to a `div`, you can use the following syntax:

```DOTHTML
<div Validator.InvalidCssClass="has-error" Validator.Value="{value: FirstName}">
   your content
</div>
```

The validation is always triggered on elements which has the `Validator.Value` property set. 

You can use the `Validator.HideWhenValid`, `Validator.InvalidCssClass`, `Validator.ShowErrorMessageText` and `Validator.SetToolTipText` to define what happens to the element. These properties are inherited to the child elements, so you may set them once on the form or page level. 

```DOTHTML
<form Validator.InvalidCssClass="has-error">    <!-- the invalid CSS class is set for the whole form -->

    <div Validator.Value="{value: FirstName}">  <!-- the Validator.Value is marks the element which gets the invalid CSS class -->
        First Name: <dot:TextBox Text="{value: FirstName}" />
    </div>
    <div Validator.Value="{value: LastName}">
        Last Name: <dot:TextBox Text="{value: LastName}" />
    </div>

</form>
```

If you want to set the `Validation.InvalidCssClass` property globally, you can apply it on the `<body>` element in the master page. 
You can of course override the CSS class on any child element.
