## Sample 5: Working with Objects

To make the `SelectedValues` property contain only some values from the `DataSource` objects (like the `Id` property of the object), you may use the `ItemValueBinding` property.

The `SelectedValues` will then contain only the values of the properties specified in the `ItemValueBinding`. Provided the values of the properties used as a value are unique, you don't need to specify the `ItemKeyBinding` property.

> The `ItemKeyBinding` property is required only when the control cannot compare the objects in the `DataSource` collection with the objects in the `SelectedValues`. If the `ItemValueBinding` is set and makes the `SelectedValues` collection use primitive types that can be compared, the `ItemKeyBinding` property is not needed. Similarly, if the `DataSource` collection contains only primitive values (`string`, `int` etc.), the `ItemKeyBinding` property is not necessary. 
