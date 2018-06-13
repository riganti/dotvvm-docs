## Sample 1: Basic Usage

The `DataSource` property specifies the collection of root items.

The `SelectedValues` property is bound to a collection with items that are selected.

The `ItemKeyBinding` defines a property which uniquely identifies the item and allows the `DataSource` and `SelectedValues` items to be compared.

The `ItemHasChildrenBinding` property defines an expression which determines whether the particular item has children.

The `ItemChildrenBinding` property specifies the collection of child items for a particular item. The children must be of the same type as the root items.

The contents inside the control are treated as the `ItemTemplate` which specifies a template for each item in the tree.
