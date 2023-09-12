## Sample 1: Basic HierarchyRepeater

The `DataSource` points to a collection of `PageViewModel`s. The `ItemChildrenBinding` property is a nested collection of pages on the `PageViewModel` type.

Inside the `ItemTemplate`, you can use **_this** to access the current data item. Note that **_parent** is the data context of the HierarchyRepeater, not the parent hierarchical item.
