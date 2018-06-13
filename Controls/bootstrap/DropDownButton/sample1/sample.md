## Sample 1: Basic Usage

The `Text` property specified the text on the button. Alternatively, you can use the `ButtonTemplate` property.

The `Type` property can specify whether the menu drops up or down.

The `IsCollapsed` property indicates whether the menu is open or not. You can also use it in data-binding.

Place `<bs:DropDownButtonItem>` controls inside the `<bs:DropDownButton>` control and use their `NavigateUrl` property to specify the link URL. 
You can place them inside the `<Items>` element, however it is not required because the `Items` is the default property of `DropDownButton`.

Because the class `DropDownButtonItem` inherits from the [ListItem](/docs/controls/bootstrap/ListItem/{branch}), please refer to the `ListItem` documentation for more information.

