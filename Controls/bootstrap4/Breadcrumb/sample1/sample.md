## Sample 1: Static Breadcrumbs

You can place `<bs:BreadcrumbItem>` elements inside the `Breadcrumbs` control. 

* The `Text` property or the contents inside the `BreadcrumbItem` element specifies the text of the menu item.
* You may set `NavigateUrl` to make the badge to link to a specific URL.
* The `Click` event can be used to invoke a command in the viewmodel.
* You may also use `RouteName`, `Param-*`, `Query-*` and `UrlSuffix` properties to build a link from a route table. See [RouteLink](~/controls/builtin/RouteLink) for details.

The `BreadcrumbItem` inherits from the [ListItem](~/controls/bootstrap4/ListItem) and uses the same properties.