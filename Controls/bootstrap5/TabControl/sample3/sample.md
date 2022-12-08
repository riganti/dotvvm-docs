## Sample 3: Data-Binding Tab Items

To create tab items from a collection in the viewmodel, use the `DataSource` property bound to any `IEnumerable` collection in the viewmodel.

The `ItemHeaderTemplate` and `ItemContentTemplate` specify the templates for the generated tab items. To set a plain text for header and a TabItem content `ItemContentText` and `ItemHeaderText` properties could be used.

The `ItemEnabled` and `ItemSelected` allow to change the TabItem availability and selected state with data binging.