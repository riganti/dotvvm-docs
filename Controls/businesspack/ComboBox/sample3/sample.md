## Sample 3: Working with Objects 2

To make the `SelectedValue` property contain only a value of a specific property from the `DataSource` object (like the `Id` property of the selected object), you may use the `ItemValueBinding` property.

The `SelectedValue` property will then contain only the value of the property specified in the `ItemValueBinding`. Provided the values of the properties used as a value are unique, you don't need to specify the `ItemKeyBinding` property.

> The `ItemKeyBinding` property is required only when the control cannot compare the objects in the `DataSource` collection with the object in the `SelectedValue` property. If the `ItemValueBinding` is set and makes the `SelectedValue` property use a primitive type that can be compared, the `ItemKeyBinding` property is not needed. Similarly, if the `DataSource` collection contains only primitive values (`string`, `int` etc.), the `ItemKeyBinding` property is not necessary. 
