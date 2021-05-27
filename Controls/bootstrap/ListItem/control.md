Represents a single item in Bootstrap lists and menus.

This control is used inside several Bootstrap controls - e.g. the [Breadcrumb](~/controls/bootstrap/Breadcrumb) and [NavigationBar](~/controls/bootstrap/NavigationBar) controls.

_In the `ListGroup` control, you have to use the [ListGroupItem](~/controls/bootstrap/ListGroupItem) control._



If you set the `NavigateUrl` property or you use the `RouteName` and `Param-*` properties,
the item behaves like a hyperlink. For more information about `RouteName` properties see the
[RouteLink](~/controls/builtin/RouteLink) control - the usage is the same.

If you set the `Click` command, the specified method in the viewmodel will be 
triggered.

Remember that you cannot use those two features together.