## Sample 3: DataSource and Item* bindings

The `DataSource` property can be used to generate the items from an `IEnumerable` in the viewmodel.

Then you can use the following properties which tell the control what property of the collection item will be used:

* `ItemText` specifies the property of the collection elements to be used as list item text.
* `ItemColor` specifies the property of the collection elements to be used as list item color.
* `ItemNavigateUrl` specifies the property of the collection elements to be used as list item link URL.
* `ItemIsEnabled` specifies the property of the collection elements indicating whether the list item is enabled or not.
* `ItemIsSelected` specifies the property of the collection elements indicating whether the list item is active or not.
* `ItemClick` specifies a command in the viewmodel to be called when the list item is clicked.