## Sample 1: Basic usage

Bind `DataSource` to the collection of items, and `SelectedValue` to the viewmodel property which receives the selected value. When the items are objects, use `ItemTextBinding` to specify the text displayed for each item, and `ItemValueBinding` to specify the value which is stored in `SelectedValue`.

The `Placeholder` property sets the text displayed when nothing is selected. If the `SelectedValue` property is nullable, the user can clear the selection. Use `Size` (or `InputSize`) to change the size of the control, `Enabled` to disable it, and the `Changed` command to react to changes of the selection.
