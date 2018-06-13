## Sample 10: User Settings

The `AllowReorderColumns` property can be used to let the user reorder the columns.

The `UserSettings` property allows to persist the customizations that the user has made to the grid control. This property expects an object of type `GridViewUserSettings` which stores the order, widths and visibility of the individual columns.

This object can be JSON-serialized easily and stored in the database, file system or any other data store. When the user customizes anything in the grid, the changes are stored in this object.

### Identification of Grid Columns

Since the developer may change the order of the grid columns, add new columns or remove the existing ones, the `GridViewUserSettings` object doesn't work with column indexes, but uses column names. You can mark each column in the markup with an unique name that will be used in the user settings object. If you rename the property in the viewmodel, or move the column to another place in the grid, the user settings object won't break and will be able to identify the column.

You can provide the name using the `ColumnName` property. If the name is not specified, the GridView will try to determine the column name from the `ValueBinding` property. In case of the `GridViewTemplateColumn` which does not have the property, the column index will be used instead. 

> If you use the `UserSettings` property, make sure that at least all template columns have the `ColumnName` property set. Otherwise the user settings may break in the future when the columns are changed by the application developer.

