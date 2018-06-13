## Sample 2: Data-bound Breadcrumbs

If you want to data-bind the items inside the `Breadcrumbs` control, use the `DataSource` property. This property expects `IEnumerable`. 

Using the `TextBinding`, `NavigateUrlBinding`, `IsSelectedBinding` and `IsDisabledBinding`, you can declare how the generated items will look.
Bindings in these properties are evaluated for every collection item and set to the corresponding properties of the generated list items.