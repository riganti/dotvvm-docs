## Sample 2: DropDownItem, DropDownSeparator and DropDownHeader

The `DropDownItem` control has a `Text` property that specifies plain text inside a control. Alternatively, you can place something inside the `DropDownItem` to render custom content.
You can disable the menu item with the `Enabled` property and set the selected state with the `Selected` property.
With a `Click` property, a custom action can be specified after clicking on the `DropDownItem` control.
Navigation to a specific URL can be set with a`NavigateUrl` property. Alternatively, you can use `RouteName` property to navigate to a specific DotVVM page.

You can separate dropdown menu items using the `bs:DropDownSeparator` control. You can also use the `bs:DropDownHeader` to specify a non-clickable title for a group of buttons. `bs:DropDownHeader` also allows you to add custom content.