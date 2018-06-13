## Sample 3: RadioButton's Changed Event

The RadioButton has the `Changed` event which is fired whenever the radio button is checked or unchecked.

Notice the `CheckedItem` and `CheckedValue` properties. The first radio button will set **true** to the `Value` property in the viewmodel,
the second one will set `false`. he value binding in the `CheckedValue` must be used, otherwise the `true` and `false` values would
be treated as strings.