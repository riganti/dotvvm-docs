## Sample 1: Click popover

Use `TriggerText` (or `TriggerTemplate`) to define the element which opens the popover, and `ContentText` (or `ContentTemplate`) for the panel content.

The `Placement` property sets the preferred position of the panel, and `ShowArrow` renders an arrow pointing to the trigger. The `IsOpen` property can be data-bound to open or close the popover from the viewmodel - it is also updated when the user opens or closes the panel.

By default, the panel closes when the user clicks outside or presses Escape. Use `CloseOnOutsideClick` and `CloseOnEscape` to change this behavior. If the panel has no visible heading, set `AriaLabel` to give it an accessible name.
