## Sample 1: Basic Usage

The `DataSource` property specifies a collection of `string`s. For each item in the collection, a list item is created.

The `SelectedValue` property is bound to a property which contains the item from the `DataSource` collection which was selected in the control. The data-binding works in both ways, so you can either read, or modify the property in the viewmodel and the changes will get synchronized with the control.
