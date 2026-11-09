Renders a data grid with the Tailwind table styling. The columns, sorting, paging, inline editing, templates, and empty-state features work the same way as on the built-in [GridView](~/controls/builtin/GridView), so you can use both the built-in columns (`dot:GridViewTextColumn`) and the Tailwind columns (`t:GridViewTextColumn`).

The Tailwind columns add support for filtering. When at least one column has `AllowFiltering="true"`, the grid displays a filter bar with a full-text search box and filter chips. Use `ShowFilterBar` to display or hide the filter bar explicitly.

The following Tailwind columns are available: `GridViewTextColumn`, `GridViewTemplateColumn`, `GridViewCheckBoxColumn`, `GridViewDateColumn`, `GridViewDateTimeColumn` (they use the Tailwind `DateTimePicker` in the edit mode), and `GridViewMultiSelectColumn` (it displays and edits a collection of enum values).

> **Changed in DotVVM 5.1:** Filtering support, the `FilteredGridViewDataSet<T>` class, and the Tailwind `GridViewTextColumn`, `GridViewTemplateColumn`, `GridViewCheckBoxColumn`, `GridViewDateColumn`, `GridViewDateTimeColumn`, and `GridViewMultiSelectColumn` columns were added.
