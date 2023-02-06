## Sample 2: Data-binding the Accordion

The `ExpandedItemIndex` property specifies the index of the accordion item which is selected.

The `DataSource` property can be used to generate the items from an `IEnumerable` in the viewmodel.

Then you can use the following properties which tell the control what property of the collection item will be used:

* `ItemContentText` specifies the property of the collection elements to be used as the content of the accordion item.
* `ItemHeaderText` specifies the text in the accordion item header.