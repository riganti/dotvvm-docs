## Sample 2: DropDownButtonItem, DropDownButtonSeparator and DropDownButtonHeader

The `DropDownButtonItem` control has a `Text` property that specifies plain text inside a control. Alternatively, you can place something inside the `DropDownButtonItem` to render custom content.
You can disable the menu item with the `Enabled` property and set the selected state with the `Selected` property.
With a `Click` property, a custom action can be specified after clicking on the `DropDownButtonItem` control.
Navigation to a specific URL can be set with a`NavigateUrl` property. Alternatively, you can use `RouteName` property to navigate to a specific DotVVM page.

You can separate dropdown menu items using the `bs:DropDownButtonSeparator` control. You can also use the `bs:DropDownButtonHeader` to specify a non-clickable title for a group of buttons. `bs:DropDownButtonHeader` also allows you to add custom content.