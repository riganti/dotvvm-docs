## Sample 3: Layouts and background colors

The `Layout` property selects how the parts are arranged:

* `Default` - the label with an optional icon on top, the value with the change indicator below.
* `DiffIcon` - the label and value on the left, the change direction displayed as a colored box on the right.
* `Ring` - a progress ring next to the label and value (see the previous sample).
* `TopIcon` - centered content with a large circular icon over the top edge of the card. The `Icon` property is required in this layout.

The `Layout` property cannot be data-bound because it changes the structure of the rendered HTML. The `BackgroundColor` property renders the card with a gradient background in the specified color.
