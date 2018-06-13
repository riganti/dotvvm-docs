## Sample 4: Changed Event

The `Changed` event can be used to trigger a command in the viewmodel whenever the control value changes.

The `RgbaColor` object contains the `ToHexColor` and the `ToCssColor` which can be used to generate the values in hex (`#112233`) or CSS format (`rgba(10, 20, 30, 1)`).

There are also the `TryParseHexColor` and `ParseHexColor` methods that can be used to transform the hex string representation of the color back to the `RgbaColor` object.