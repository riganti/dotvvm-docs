## Sample 2: Working with Objects

When the `DataSource` collection is bound to a collection of objects, you may use the `ItemTextBinding` to specify, which property of the object in the collection will be displayed as a text of a particular list item.

The `SelectedValue` will still contain the object from the `DataSource` collection which is selected. To be able to identify these objects, you should use the `ItemKeyBinding` property to define a property which can work as a key. The key must be unique for all items in the collection, otherwise the control will not work properly.

The `Placeholder` property specifies the text which is displayed when no value is selected.