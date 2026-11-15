## Sample 1: Basic usage

Bind `DataSource` to the collection of items, and `SelectedValues` to a collection in the viewmodel which receives the selected values. When the items are objects, use `ItemTextBinding` to specify the text displayed for each item, and `ItemValueBinding` to specify the value which is stored in `SelectedValues`.

The `Placeholder` property sets the text displayed when nothing is selected. `Size` (or `InputSize`) changes the size of the control, `Enabled` disables it, and the `Changed` command is invoked when the selection changes.
