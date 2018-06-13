## Sample 7: Server Search

The `LoadItems` property can specify a command in the viewmodel which will return new items based on what the user types in the control.
Typically, this method is used with the [Static Command](/docs/tutorials/basics-static-command-binding/{branch}) and the method should return a list of items,
and you need to use the value of the `Text` property as an argument to the viewmodel method.

The method in the viewmodel performs the search and returns a collection of items. These items are either added to existing items in the `DataSource` collection or they replace the existing items. It depends on the `LoadItemsMode` property.

If you use the static command, don't forget to decorate the method with the `AllowStaticCommand` attribute.

> In the future versions of DotVVM, there are plans to enhance the server search functionality with more features (e.g. REST API integration). 