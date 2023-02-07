## Sample 8: Bootstrap 3 and 4 compatibility

The `BootstrapForm` control contains several properties which can be set to configure CSS class names. The default values of these properties are suited to work with **Bootstrap 5**. 

For earlier versions, you may configure the values as follows:

| Property                              | Bootstrap 4               | Bootstrap 3       |
|---------------------------------------|---------------------------|-------------------|
| `FormGroupCssClass`                   | `"form-group"`            | `"form-group"`    |
| `LabelCssClass`                       | `""`                      | `""`              |
| `FormControlCssClass`                 | `"form-control"`          | `"form-control"`  |
| `FormSelectCssClass`                  | `"form-control"`          | `"form-control"`  |
| `FormCheckCssClass`                   | `"form-group form-check"` | `"checkbox"`      |
| `FormCheckLabelCssClass`              | `"form-check-label"`      | `""`              |
| `FormCheckInputCssClass`              | `"form-check-input"`      | `""`              |
| `WrapControlInDiv`                    | `false`                   | `true`            |
| `Validator.InvalidCssClassProperty`   | `"is-invalid"`            | `"has-error"` 	|

You can set these properties globally for your application using [server-side styles](~/pages/concepts/dothtml-markup/server-side-styles) in `DotvvmStartup.cs`:

```CSHARP
// for Bootstrap 3
config.Styles.Register<BootstrapForm>()
    .SetProperty(c => c.FormGroupCssClass, "form-group")
    .SetProperty(c => c.LabelCssClass, "")
    .SetProperty(c => c.FormControlCssClass, "form-control")
    .SetProperty(c => c.FormSelectCssClass, "form-control")
    .SetProperty(c => c.FormCheckCssClass, "checkbox")
    .SetProperty(c => c.FormCheckLabelCssClass, "")
    .SetProperty(c => c.FormCheckInputCssClass, "")
    .SetProperty(c => c.WrapControlInDiv, true)
    .SetDotvvmProperty(Validator.InvalidCssClassProperty, "has-error");

// for Bootstrap 4
config.Styles.Register<BootstrapForm>()
    .SetProperty(c => c.FormGroupCssClass, "form-group")
    .SetProperty(c => c.LabelCssClass, "")
    .SetProperty(c => c.FormControlCssClass, "form-control")
    .SetProperty(c => c.FormSelectCssClass, "form-control")
    .SetProperty(c => c.FormCheckCssClass, "form-check")
    .SetProperty(c => c.FormCheckLabelCssClass, "form-check-label")
    .SetProperty(c => c.FormCheckInputCssClass, "form-check-input")
    .SetDotvvmProperty(Validator.InvalidCssClassProperty, "is-invalid");
```

## Validation style of CheckBox in Bootstrap 4 and 5

Due to the way how DotVVM [CheckBox](~/controls/builtin/CheckBox) renders (`label` wrapped around `input`), you may need to add the following CSS to correctly display validation appearance of this control in the form.

```
<style>
    .is-invalid.form-check-label, .is-invalid .form-check-label {
        color: #dc3545;
    }
    .is-invalid .form-check-input {
        border-color: #dc3545;
    }
</style>
```