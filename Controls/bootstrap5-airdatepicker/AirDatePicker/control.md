Renders a Bootstrap 5-styled input using the [AIR Datepicker](https://air-datepicker.com/) widget. It supports date, time, and combined date-time selection with a nullable `DateTime` `SelectedValue` binding.

Install the `DotVVM.Controls.Bootstrap5.AirDatePicker` package and register it in `DotvvmStartup.cs` after Bootstrap 5:

```CSHARP
config.AddBootstrap5Configuration();
config.AddBootstrap5AirDatePickerConfiguration();
```

Use `Type` to choose `Date`, `Time`, or `DateTime`. `DateFormat` and `TimeFormat` use .NET format strings; supported date tokens are `d`, `dd`, `MMM`, `MMMM`, `yy`, and `yyyy`, and supported time tokens are `h`, `hh`, `H`, `HH`, `m`, `mm`, and `tt`.