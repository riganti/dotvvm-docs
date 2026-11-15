## Sample 1: Traffic sources

The header of the card is configured using the `Value`, `FormatString`, `Unit`, `Label`, `Description`, `Diff`, and `DiffDirection` properties, which work the same way as on the `StatCard` control.

Each segment is declared using the `StatSegmentsItem` control. Its `Progress` property (0 to 100) sets the width of the segment in the bar, `Label`, `Value`, and `FormatString` define the legend text, and `Color` sets the color of the segment. All properties of the segment can be data-bound.
