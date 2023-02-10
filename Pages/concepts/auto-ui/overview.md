# Auto UI Overview

> This feature is new in **DotVVM 4.1**.

**DotVVM Auto UI** is a separate NuGet package `DotVVM.AutoUI` which contains components for auto-generation of user interface based on model classes and metadata.

It can be used make consistent look & feel of forms and data grid pages in large DotVVM applications, and it saves a lot of repeated and often copy-pasted pieces of markup.

## Components

All components from **Auto UI** have the `<auto:` tag prefix. Basically, there are three components which helps with the automatic generation of UI elements.

* [Editor](~/controls/builtin-autoui/Editor) generates the editor control for a specified property. You can use this control in some scenarios, but it is mainly used internally by other Auto UI components.

* [Form](~/controls/builtin-autoui/Form) with its versions [BootstrapForm](~/controls/builtin-autoui/BootstrapForm) and [BulmaForm](~/controls/builtin-autoui/BulmaForm) generates a form for an object in the current [binding context](~/pages/concepts/data-binding/binding-context). 

* [GridViewColumns](~/controls/builtin-autoui/GridViewColumns) is a special type of a `GridView` column which generates the columns based on the current row [binding context](~/pages/concepts/data-binding/binding-context). 

## Model metadata

To provide field labels, display formats and other metadata to the Auto UI, you can use one of the several options:

* __Data annotation attributes__ on properties of the model classes:

```CSHARP
[Display("First Name")]
[Visible(ViewNames = "List")]
[Enabled(Roles = "Administrator | Moderator")]
...
public string FirstName { get; set; }
```

* Providing metadata by a convention-based approach in the Auto UI configuration:

```CSHARP
options.AddAutoUI(config => {
    config.PropertyMetadataRules
        .For(p => p.Name.EndsWith("ImageUrl"), rule => rule.SetUIHint("Image"));
});
```

* Providing RESX files with field labels and error messages.

* Implementing your own `IPropertyDisplayMetadataProvider` and replacing the default implementation in `IServiceCollection`.

See the [Metadata](./metadata) chapter for more info.

## Default rules

By default, Auto UI [Form](~/controls/builtin-autoui/Form) controls looks at the property type and infers the best editor for the purpuse.

* For `string` and `Guid` properties, a `TextBox` controls will be used.
* For numeric and date-time values, a `TextBox` with `FormatString` and appropriate `Type` (`number`, `datetime-local`) will be used.
* For `Enum` values, a `ComboBox` will be used and the items will be generated from the enum members. 
* For `bool` values, a `CheckBox` will be used.

The [GridViewColumns](~/controls/builtin-autoui/GridViewColumns) will use `GridViewTextColumn` for all of the types above except for `bool` which use `GridViewCheckBoxColumn` by default. 

### Selectors

Auto UI adds a special `Selection` attribute which instructs the Auto UI that the value (or values) will be selected from a list of options.

* For primitive type properties (strings, numbers, date & time values, enums and `Guid`), a `ComboBox` will be rendered so the user can pick one value. Nullable versions will allow selecting the `null` value as well.

* For collections of primitive types, a group of `CheckBox` controls will be rendered, so the user can select multiple values.

When you use the selectors, Auto UI will automatically look for a property of type `SelectionViewModel<TSelector>` that will host the collection of options to select from. Also, you will need to provide an implementation of `ISelectionProvider<TSelector>` to be able to obtain the list of the options.

See the [Selectors](./selectors) chapter for more info.

### Custom rules

You can add your own handlers into the `FormEditorProviders` and `GridColumnProviders`. 

See the [Extensibility](./extensibility) chapter for more info.

## Relationship between Auto UI and Dynamic Data

**Auto UI** is an improved version of [DotVVM Dynamic Data](~/pages/community-add-ons/dotvvm-dynamic-data) with more features and performance optimizations. 

Main improvements in **Auto UI**:

* [Selectors](./selectors)
* Pre-compilation of generated controls when the page is loaded for the first time
* Multiple versions of [Form](~/controls/builtin-autoui/Form) controls for popular CSS frameworks Bootstrap and Bulma
* Explicit listing of properties that should be generated using the `IncludeProperties` and `ExcludeProperties`
* An option to override templates for specific generated properties

**DotVVM Dynamic Data** is still working and fully functional, but we don't plan to add new features there. We recommend to migrate to **Auto UI**.

## See also

* [Metadata](./metadata)
* [Selectors](./selectors)
* [Extensibility](./extensibility)
* [Form control](~/controls/builtin-autoui/Form)
* [BootstrapForm control](~/controls/builtin-autoui/BootstrapForm)
* [BulmaForm control](~/controls/builtin-autoui/BulmaForm)
* [GridViewColumns control](~/controls/builtin-autoui/GridViewColumns)