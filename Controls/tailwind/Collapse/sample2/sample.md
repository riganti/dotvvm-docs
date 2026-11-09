## Sample 2: Templates, types and icons

Use `HeaderTemplate` and `ContentTemplate` to place any markup in the header and body.

The `Type` property selects the visual style - `Default` (bordered card), `Flush` (adjacent items share their borders), `Borderless`, or `Tree` (borderless with an indented body, suitable for nested structures). `Size="Small"` makes the text and spacing more compact.

The `ExpandIcon` property replaces the default chevron. When `CollapseIcon` is set as well, the two icons are swapped instead of rotated. Use `HeaderMode="Sticky"` to keep the header visible while scrolling through long content, and `AriaLabel` to provide an accessible name for the header button.
