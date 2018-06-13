## Sample 7: Inline Inserting

The `InlineInserting` property can enable the inline insert feature on the grid which adds an extra row on the top or the bottom of the table.

The `InlineInsertRowPlacement` property specifies whether the insert row is placed on the `Top` or the `Bottom` of the table.

In the column, you can then specify the `InsertTemplate` to provide a custom template for a cell in the insert row.

If you need to combine the inline insert with inline edit, you can provide both templates, or set only the `EditTemplate`. Provided that the `InsertTemplate` is not set, the `EditTemplate` is be used.