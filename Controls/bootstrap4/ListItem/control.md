A base class representing an item in Bootstrap lists and menus. This list item is used in the following controls:

* [Breadcrumb](~/controls/bootstrap4/Breadcrumb) uses `<bs:BreadcrumbItem>`.
* [DropDownButton](~/controls/bootstrap4/DropDownButton) uses `<bs:DropDownButtonItem>`.
* [ListGroup](~/controls/bootstrap4/ListGroup) uses `<bs:ListGroupItem>`.
* [NavigationBar](~/controls/bootstrap4/NavigationBar) uses `<bs:NavigationItem>`.
* [ResponsiveNavigation](~/controls/bootstrap4/ResponsiveNavigation) uses `<bs:NavigationItem>`.

All these controls use the following properties on its list items:

* `Text` represents the text on the list item. Alternatively, you can use `ContentTemplate` property to specify any HTML content. 
* If you set the `NavigateUrl` property, or you use the `RouteName`, `Param-*`, `Query-*` and `UrlSuffix` properties, the item will behave like a hyperlink. For more information about `RouteName` properties, see [RouteLink](~/controls/builtin/RouteLink) control.
* Alternatively, you can set the `Click` command which specifies the  method in the viewmodel to be triggered. 
* `Enabled` property can be used to enable or disable the list item. 
* `IsSelected` property specifies whether the list item is active or not.