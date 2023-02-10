```DOTHTML
<div class="mb-4">
  <label class="control-label autoui-required" for="Name__input">Person or company name</label>
  <div>
    <input class="form-control" type="text" required id="Name__input" ... />
  </div>
</div>
<div class="mb-4">
  <div class="form-check">
    <label id="IsCompany__input" class="form-check-label">
      <input type="checkbox" class="form-check-input" ... >
      <span>Is company</span>
    </label>
  </div>
</div>
```

### Bootstrap 3 and 4 compatibility

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
| `Validator.InvalidCssClassProperty`   | `"is-invalid"`            | `"has-error"` 	  |

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