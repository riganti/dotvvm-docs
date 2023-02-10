# Extensibility

## Custom metadata providers

You can provide your own implementation of metadata providers. This may be useful if you want to read the metadata from other place than .NET attributes, for example from a database.

* `IPropertyDisplayMetadataProvider` provides basic information about properties - the display name, format string, order, group name (you can split the fields into multiple groups and render each group separately). See the [Metadata](./metadata) chapter for more info.

* `IViewModelValidationMetadataProvider` allows to retrieve all validation attributes for each property. See the [Validation](~/pages/concepts/validation/overview) chapter for more info.

* `IEntityPropertyListProvider` provides a list of properties for which the editors or columns shall be generated. 

You can implement these interfaces and replace the default implementation the Auto UI provides for them.

## Custom editors and grid columns

Currently, the framework supports `TextBox`, `CheckBox` and `ComboBox` editors, which can edit string, numeric, date & time, boolean, and enum values, and also handle [selections](./selectors). 

If you want to support any other data type, or if you want to provide a better experience for particular properties, you can implement your own editor and grid column.

### Implement the editor

To implement a custom editor, you need to inherit the `FormEditorProviderBase` class.

* Implement the `CanHandleProperty` method to check whether the editor is capable of providing the editing experience for a property of the given type.

* Implement the `CreateControl` method to generate the editor controls and bindings.

* Optionally, you can override the `UIHints` array to provide a matching mechanism for properties using the `UIHint` attribute. See the [UI Hints](#ui-hints) section for more info.

See the [implementation of the built-in editors](https://github.com/riganti/dotvvm/tree/main/src/AutoUI/Core/PropertyHandlers/FormEditors) for more details.

You will need to register the form editor in the Auto UI configuration:

```CSHARP
options.AddAutoUI(config => 
{
    // auto-discover all editors in assembly
    options.AutoDiscoverFormEditorProviders(yourAssembly);

    // OR
    
    // register editors individually
    // (the order of them matters - if you want to override the built-in editor for some cases, you need to put yourself before)
    config.FormEditorProviders.Insert(0, ...);
});
```

### Implement the grid column

To implement a custom editor, you need to inherit the `GridColumnProviderBase` class.

* Implement the `CanHandleProperty` method to check whether the column is capable of providing the experience for a property of the given type.

* Implement the `CreateColumnCore` method to generate the particular GridView column.

* Optionally, you can override the `UIHints` array to provide a matching mechanism for properties using the `UIHint` attribute. See the [UI Hints](#ui-hints) section for more info.

See the [implementation of the built-in columns](https://github.com/riganti/dotvvm/tree/main/src/AutoUI/Core/PropertyHandlers/GridColumns) for more details.


```CSHARP
options.AddAutoUI(config => 
{
    // auto-discover all columns in assembly
    options.AutoDiscoverGridColumnProviders(yourAssembly);

    // OR
    
    // register columns individually
    // (the order of them matters - if you want to override the built-in column for some cases, you need to put yourself before)
    config.GridColumnProviders.Insert(0, ...);
});
```

### UI Hints

The order of registration of editor and column providers is important - the first provider which will return `true` from the `CanHandleProperty` method will be used for the particular property.

To provide a better matching experience, the Auto UI contains also the concept of UI hints. The UI hint is a string identifier that can define "requirements" for the editor or column.

For example, if you want to build an editor which lets the user to upload a profile image, the editor will support any `string` property. It will just upload the image somewhere and store its URL in the viewmodel property.

This editor can specify `"ImageUpload"` as in its `UIHints` property. If it does that, any model property which has the `UIHint` set to `"ImageUpload"` will use this editor before the default one. 

The editors and columns can specify multiple hints, and so the model properties. The first matching hint will be used, so the order of UI hints should be from the most specific to the most generic. 

## Custom forms

The [Form](~/controls/builtin-autoui/Form) control provides the basic experience for generating forms. 

It is likely that you will want to use either ready-made [BootstrapForm](~/controls/builtin-autoui/BootstrapForm) or [BulmaForm](~/controls/builtin-autoui/BulmaForm) as they are compatible with popular CSS frameworks, or you will want to build your own implementation.

To build a custom form, you can inherit the `AutoFormBase` which contains some helper methods. Then, you can add the `GetContents` method to build your UI.

There are several useful methods:

* `CreateAutoUiContext` will build a `AutoUIContext` object that is required for the other methods during the generation process.

* `GetPropertiesToDisplay` will retrieve a list of properties for which the fields shall be generated.

* `TryGetFieldTemplate` will look whether the field specifies a custom template that shall be used instead of the auto-generated one.

* `InitializeControlLabel` will generate a label for the particular property.

* `CreateEditor` will generate the concrete editor for the particular property.

* `InitializeValidation` will configure the `Validator.Value` property for the editor and will also add the `autoui-required` CSS class on the label if the field is required.

* `SetFieldVisibility` will evaluate whether the field shall be visible or not, and apply these settings.

You can look at the [source code of the Form control](https://github.com/riganti/dotvvm/blob/main/src/AutoUI/Core/Controls/AutoForm.cs) to see how to use the base class.

## See also

* [Auto UI Overview](./overview)
* [Metadata](./metadata)
* [Selectors](./selectors)
* [Form control](~/controls/builtin-autoui/Form)