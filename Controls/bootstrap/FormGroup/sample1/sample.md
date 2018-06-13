## Sample 1: FormGroup Usage

Place any content inside the `FormGroup` control. 

The [TextBox](/docs/controls/builtin/TextBox/{branch}) and [ComboBox](/docs/controls/builtin/ComboBox/{branch}) controls will get the `class="form-control"` automatically 
so they'll get the default Bootstrap look.

If you use the [CheckBox](/docs/controls/bootstrap/CheckBox/{branch})es and [RadioButton](/docs/controls/bootstrap/RadioButton/{branch})s, make sure you use `<bs:CheckBox>` and `<bs:RadioButton>`
instead of `<dot:CheckBox>` and `<dot:RadioButton>` controls. Their `IsInline` property will be set automatically to **true** so they'll get the correct padding.