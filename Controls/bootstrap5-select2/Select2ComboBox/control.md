Extends the Bootstrap 5 [ComboBox](~/controls/bootstrap5/ComboBox) control with the [Select2](https://select2.org/) widget, which provides search and improved keyboard navigation.

Install the `DotVVM.Controls.Bootstrap5.Select2` package and register it in `DotvvmStartup.cs` after Bootstrap 5:

```CSHARP
config.AddBootstrap5Configuration();
config.AddBootstrap5Select2Configuration();
```

The control uses the `bs` prefix and supports all properties of `ComboBox`.