## Sample 6: Inline Editing

The `InlineEditing` property enables the row editing mode in the `GridView` control.

You have to do the following things to make the inline editing work:

* Bind the `GridView` control to the `GridViewDataSet<T>`, binding to simple collections is not supported in the inline-editing mode.

* Set the `PrimaryKeyPropertyName` on the data set to a name of the column having unique values in each row. Typically you need to 
    place the primary key column name in this property (Id, CustomerId etc.). It should be a value that works with the === operator
    in Javascript, so we recommend to use integer, string or Guid values.

* If you want to edit some row, set the `EditedRowId` on the `GridViewDataSet` to the value of the primary key of the row. The row will 
    become editable. If you want to exit from the edit mode, set the `EditedRowId` to `null`.

* If you want to make some column read-only, set its `IsEditable` property to `false`.

* If you don't want to use the default `GridViewTextColumn`, which renders a [Literal](/docs/controls/builtin/Literal/{branch}) in the read-only 
    mode and the [TextBox](/docs/controls/builtin/TextBox/{branch}) in the edit mode, use the `GridViewTemplateColumn` and use its `EditTemplate` 
    to specify how the cell will look like when in the edit mode.

<br />

If you use the `RowDecorators` property (see Sample 5), they are applied only to normal rows. If you need to apply a decorator to edit-mode rows,
use the `EditRowDecorators` property. 