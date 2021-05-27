# Upgrading from 2.3 to 2.4

See [Release notes of DotVVM 2.4 Preview 1](https://github.com/riganti/dotvvm/releases/tag/v2.4.0-preview01), [Release notes of DotVVM 2.4 Preview 2](https://github.com/riganti/dotvvm/releases/tag/v2.4.0-preview02), and [Release notes of DotVVM 2.4](https://github.com/riganti/dotvvm/releases/tag/v2.4.0) for complete list of changes.

## Breaking changes

### Dropped support for ASP.NET Core 2.0

We have raised the oldest supported version of **ASP.NET Core** to **ASP.NET Core 2.1** as it is the oldest LTS version supported by Microsoft.

### UpdateTextAfterKeydown changed to UpdateTextOnInput

The `UpdateTextAfterKeydown` property on `TextBox` was changed to `UpdateTextOnInput`.

### Type checking for CheckBox, RadioButton and ComboBox

We have added type checks for `CheckBox`, `RadioButton` and `ComboBox` controls. These checks make sure that `CheckedItem`/`SelectedItem` has the same or compatible type to the `CheckedValue`/`ItemValueBinding` properties. 

If the types didn't match, the controls could work in some cases, but using different types in these situations is not a good idea in general.

We recommend to run [Compilation status page](compilation-status-page) on the app to see if the types used in these controls are correct on all pages.

### ItemValueBinding must return primitive types

We have also enforced the `ItemValueBinding` property of `ComboBox`, `ListBox` and `MultiSelect` controls to return a primitive type value. If this property returns objects, there is no way how to compare them on the client-side, so the controls may not be able to identify the selected item in some cases. 

## Frozen configuration

The `DotvvmConfiguration` object is now frozen after the DotVVM startup routine is finished, so it is not possible to modify the configuration when the application is running. It could lead to dangerous situations and possible multi-threading issues. 

If you receive errors when you try to modify the configuration, make sure you modify it in `Startup.cs` or in `DotvvmStartup.cs`. There is a new overload of `UseDotVVM` that allows to modify the configuration before it is frozen:

```CSHARP
app.UseDotVVM<DotvvmStartup>(..., modifyConfiguration: c => {
    c.RouteTable.Add(...);
});
```

## See also

* [From 2.4 to 2.5](from-2-4-to-2-5)