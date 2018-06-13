## Sample 2: GridViewTextColumn

Each column in the grid must be declared in the `<Columns></Columns>` collection. **GridViewTextColumn** displays the field as plain text.

&nbsp;

### Required Properties

The most important property is the `ValueBinding` which contains a binding which selects the property to display from the object.

You can customize the header cell using the `HeaderText` property, or the `HeaderTemplate` template.

&nbsp;

### Sorting

There is also a property `AllowSorting` which can be used to sort the column. If it is set to true, you can also use the `SortExpression` 
property to specify which column should be used for sorting. It is useful when the values in the column are e.g. 'lowest', 'normal' and 'highest' 
and you don't need to order them alphabetically, but you have another column with numeric representation of those values. In that case, you can use 
the name of such column as a `SortExpression`.

&nbsp;

### Styling

The `Width` property allows to set the width of the column. It is rendered as `style="width: XXX"` attribute on the `<th>` element in the table header.

You can specify `CssClass` to be applied on each table cell `<td>`. This property supports data-binding.

There is also the `HeaderCssClass` property that adds the specified CSS class to the `<th>` element in the table header.

&nbsp;

### Formatting

Use the `FormatString` property to specify the format of numbers or date-time values. Most of the .NET Framework format strings is supported.