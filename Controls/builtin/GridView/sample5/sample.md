## Sample 5: RowDecorators and CellDecorators

Sometimes you need the rows in the grid to be clickable, or the cells to have some specific color. 

That's why the **GridView** can use *decorators* for table rows and cells.
A *decorator* can add various attributes to the element it decorates.

The `<dot:Decorator>` control will pass all its attributes and event handlers on the decorated element.

There are the following types of collections on `GridView` and decorate the `<tr>` elements:

* `RowDecorators` apply on all data-rows except for those that are in the edit mode (if inline editing is enabled).
* `EditRowDecorators` apply on the currently edited row (if inline editing is enabled).
* `HeaderRowDecorators` apply on the header row only.

The `GridViewColumn` and its descendants allow to specify the following decorators to decorate the `<td>` or `<th>` elements:

* `CellDecorators` apply on all cells except for those that are in the edit mode (if inline editing is enabled).
* `EditCellDecorators` apply on the cell in the row is in the edit mode (if inline editing is enabled).
* `HeaderCellDecorators` apply on the cell in the header row only.
