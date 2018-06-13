## Sample 5: CheckBoxes in Repeater and ButtonGroup

You can combine the [CheckBox](/docs/controls/bootstrap/CheckBox/{branch})es and [RadioButton](/docs/controls/bootstrap/RadioButton/{branch})s with the [Repeater](/docs/controls/builtin/Repeater/{branch}) in the `ButtonGroup` control.
Use the `CheckedItems` and `CheckedValue` properties to bind these checkboxes to a collection in the viewmodel.

Because we don't want the `Repeater` to render its wrapper `<div>` element, the `ButtonGroup` will switch the `RenderWrapperTag` property to `false` automatically.


