## Sample 5: CheckBoxes in Repeater and ButtonGroup

You can combine the [CheckBox](~/controls/bootstrap/CheckBox)es and [RadioButton](~/controls/bootstrap/RadioButton)s with the [Repeater](~/controls/builtin/Repeater) in the `ButtonGroup` control.
Use the `CheckedItems` and `CheckedValue` properties to bind these checkboxes to a collection in the viewmodel.

Because we don't want the `Repeater` to render its wrapper `<div>` element, the `ButtonGroup` will switch the `RenderWrapperTag` property to `false` automatically.


