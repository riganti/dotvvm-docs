## Sample 4: ButtonGroup with DataSource

The `DataSource` property can be used to generate the items from an `IEnumerable` in the viewmodel.

Then you can use the following properties which tell the control what property of the collection item will be used:

* `ItemEnabled` specifies the property of the collection elements to be used that sets whether the button group item is enabled
* `ItemText` specifies the text in the button item
* `ItemType` specifies the button type
* `ItemVisualStyle` specifies the button visualStyle
* `ItemClick` specifies the property command that will be triggered when a button is clicked