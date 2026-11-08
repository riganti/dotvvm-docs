## Sample 1: Values and change indicators

The `Value` property is required and accepts numbers, strings, or `null`. Please note that a plain attribute value is treated as a string - use a resource binding like `Value="{resource: 4145}"` for hard-coded numbers.

Use `FormatString` to format numbers using the current culture, and `Unit` to display a suffix after the value. Data-bound values are formatted in the browser, which does not support literal text in format strings, so always put suffixes in `Unit`.

The `Diff` property contains the text of the change indicator, and `DiffDirection` (`NoChange`, `Up`, or `Down`) selects its icon and color. `Label`, `Icon`, and `Description` add additional information to the card.
