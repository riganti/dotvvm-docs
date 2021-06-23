## Sample 7: Inline Inserting

The `InlineInsertMode` property can be used to enable the inline insert feature on the grid. This feature adds an additional editable row to the table that can be used to add new items.

The `InlineInsertRowPlacement` property specifies whether the row is placed on the `Top` or the `Bottom` of the table.

In the column, you can then specify the `InsertTemplate` to provide a custom template for a cell in the insert row.

If you need to combine the inline insert with inline edit, you can provide both templates, or set only the `EditTemplate`. If the `InsertTemplate` is not set, the `EditTemplate` will be used.