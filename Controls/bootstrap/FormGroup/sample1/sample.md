## Sample 1: FormGroup Usage

Place any content inside the `FormGroup` control. 

The [TextBox](~/controls/builtin/TextBox) and [ComboBox](~/controls/builtin/ComboBox) controls will get the `class="form-control"` automatically 
so they'll get the default Bootstrap look.

If you use the [CheckBox](~/controls/bootstrap/CheckBox)es and [RadioButton](~/controls/bootstrap/RadioButton)s, make sure you use `<bs:CheckBox>` and `<bs:RadioButton>`
instead of `<dot:CheckBox>` and `<dot:RadioButton>` controls. Their `IsInline` property will be set automatically to **true** so they'll get the correct padding.