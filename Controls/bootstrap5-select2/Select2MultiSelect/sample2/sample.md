## Sample 2: Create new items

Set `AllowCreateItems="true"` to allow users to enter a new item and select it by pressing Enter or clicking the matching option. The control adds the item to both `DataSource` and `SelectedValues`. Deselecting an item removes it from `SelectedValues` but keeps it in `DataSource`, so it can be selected again without creating a duplicate.

Both collections must contain strings. Bind `DataSource` using a writable value binding, such as a `List<string>` property. Control usage validation rejects non-string data sources and resource bindings when item creation is enabled. Strings are used directly as item text and values, so `ItemTextBinding` and `ItemValueBinding` are not needed.

`AllowCreateItems` defaults to `false` and must be specified as a hard-coded value, not a binding. Without item creation, the control still supports collections of objects as shown in the first sample.

This sample handles `SelectionChanged` to show the data source received by the server. The new item is added before the command runs and persists through the postback. See [MultiSelect](~/controls/builtin/MultiSelect) for the inherited selection properties and events.
