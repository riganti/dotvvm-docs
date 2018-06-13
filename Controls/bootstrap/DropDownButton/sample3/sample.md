## Sample 3: DropDownButton Data-binding

You can also load the dropdown button items from a collection in the viewmodel using the `DataSource` property.

You have to specify the `TextBinding` which points to an appropriate property of the collection item. The URL of the item is specified using the `NavigateUrlBinding` property.

You can also use the `IsDisabledBinding` and `IsSelectedBinding` to specify which properties of the collection item declare the selected or disabled state of the button. 
The same logic applies to the `ClickBinding` property - use it if you want the buttons to invoke a command on the viewmodel.
