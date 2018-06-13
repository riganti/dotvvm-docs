## Sample 1: Basic Usage

The `DataSource` property specifies a collection of `string`s. For each item in the collection, a checkbox is created.

The `SelectedValues` property is bound to a collection which contains all `string`s of checkboxes that are checked. The data-binding works in both ways, so you can either read, or modify the collection in the viewmodel and the changes will get synchronized with the checkboxes.
