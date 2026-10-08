Extends the Bootstrap 5 [MultiSelect](~/controls/builtin/MultiSelect) control with the [Select2](https://select2.org/) widget, which provides searchable multiple selection.

Install the `DotVVM.Controls.Bootstrap5.Select2` package and register it in `DotvvmStartup.cs` after Bootstrap 5:

```CSHARP
config.AddBootstrap5Configuration();
config.AddBootstrap5Select2Configuration();
```

The control uses the `bs` prefix and supports all properties of `MultiSelect`.

Set `AllowCreateItems="true"` to let users create new items while searching. This feature is disabled by default and requires `DataSource` and `SelectedValues` to be collections of strings. New items are added to `DataSource` and remain available after they are deselected.
