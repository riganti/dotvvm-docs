## Sample 2: Selecting objects and server-side search

When you need to store the whole object in `SelectedValue`, omit `ItemValueBinding` and use `ItemKeyBinding` to specify a primitive key which identifies the objects. The `ItemTitleBinding` property sets the tooltip of each item.

The optional `Search` property accepts a static command which receives the text typed by the user, and returns the matching items. The results are merged into the `DataSource` collection, so the collection must be a writable property in the viewmodel. If the search method is asynchronous, use `.Result` in the binding - DotVVM translates it to an asynchronous call.

> The `Search`, `ItemKeyBinding`, and `ItemTitleBinding` properties are new in DotVVM 5.1.
