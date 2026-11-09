The DatePicker documentation covers the current `DateTimePicker` and `DateTimePickerFormField` controls. They render Tailwind-styled date, time, and date-time pickers with nullable `DateTime` bindings.

The format of the date and time in the input follows `CultureInfo.CurrentCulture`, and the calendar texts (names of months and days) follow `CultureInfo.CurrentUICulture`. Set `Culture` to override both, or `UICulture` to override only the calendar texts. The `DateFormat` and `TimeFormat` properties override the format explicitly.

> **Changed in DotVVM 5.1:** The `UICulture` property was added. The `Culture` property now affects the format of the date and time input (by default, `CurrentCulture` is used instead of `CurrentUICulture`), and it selects the calendar language only when `UICulture` is not set.
