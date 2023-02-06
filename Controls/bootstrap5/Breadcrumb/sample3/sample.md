## Sample 3: Data-bound Breadcrumbs

If you want to data-bind the items inside the `Breadcrumbs` control, use the `DataSource` property. This property expects `IEnumerable`. 

Using the `ItemText`, `ItemNavigateUrl`, `ItemIsSelected`, `ItemIsEnabled` and `ItemClick`, you can declare how the generated items will look.
Bindings in these properties are evaluated for every collection item and set to the corresponding properties of the generated list items.
You can use `ItemTemplate` to specify template of generated list items.