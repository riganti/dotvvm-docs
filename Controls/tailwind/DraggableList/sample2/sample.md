## Sample 2: Moving items between lists

To allow moving items between lists, set `AllowTransfer` to `true` on both lists. The lists must have the same `GroupName` and their collections must have the same element type.

When an item is moved to another list, the `OnDrop` commands of the source and target lists are invoked (each of them once), after both collections are updated.
