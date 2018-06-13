## Sample 3: TabControl Data-Binding

To create tab items from a collection in the viewmodel, use the `DataSource` property bound to any `IEnumerable` collection in the viewmodel.

The `HeaderTemplate` and `ContentTemplate` specify the templates for the generated tab items.
  
If you want to control which tab item is active in combination with the `DataSource` property, you can do it by setting the `IsActiveBinding` property 
to a property on the data-bound items which specifies whether the tab should be active or not.