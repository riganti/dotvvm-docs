## Sample 1: Standalone Date, Time, and DateTime Pickers

Use `DateTimePicker` when you need the picker widget without a form-field wrapper. The `SelectedValue` property is always a nullable `DateTime`.

The third picker uses `Culture="cs-CZ"` which selects the Czech calendar texts, and overrides the date and time format using `DateFormat` and `TimeFormat`. To use a different culture for the calendar texts, set the `UICulture` property (new in DotVVM 5.1).
