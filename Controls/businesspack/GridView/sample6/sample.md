## Sample 6: Inline Editing

Enum `InlineEditMode` specifies whether the user will be able to edit rows directly in the grid. To enable editing set the value to `SingleRow`.

Every column has the `AllowEditing` property which specifies whether the particular column can be edited or not.

The columns also have the `EditTemplate` property which can be used to define a custom edit template for the cell.

The object that is currently being edited, is found by the `Customers.RowEditOptions.EditRowId` property which contains its primary key. The name of the property that is the primary key is set in `Customers.RowEditOptions.PrimaryKeyPropertyName`.

Validation on the editable row can be enabled by setting `BusinessPack.AllowAutoValidation` on the `<bp:GridView`. This property automatically sets `Validator.Value` on all BusinessPack form controls.
