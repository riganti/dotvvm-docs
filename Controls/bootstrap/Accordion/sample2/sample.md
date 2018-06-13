## Sample 2: Data-binding the CollapsiblePanels

You can data-bind the panels using the [Repeater](/docs/controls/builtin/Repeater/{branch}) control and use the `ExpandedPanelIndex` property to determine which panel is currently expanded.  
When none of the panels is expanded, the value of this property will be -1.

Because we don't want the `Repeater` to place the panels inside one `<div>` element, we are using the `RenderWrapperTag` property to modify the default behavior.