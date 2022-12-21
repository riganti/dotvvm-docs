## Sample 3: DropDownButton with DataSource

You can also load the dropdown button items from a collection in the viewmodel using the `DataSource` property.

Induvidual item properties could be specified with as a binding which points to an appropriate property of the collection item. All item properties have an `Item-` prefix (`ItemEnabled`, `ItemIsSelected`, `ItemClick`, `ItemText`, `ItemTemplate`...).