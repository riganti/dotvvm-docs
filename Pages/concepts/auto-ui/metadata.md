# Model metadata

The easiest way of providing metadata to properties in model classes is to use [Data annotation attributes](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-7.0).

## Supported attributes

**Auto UI** supports several of these attributes and adds a few of its own attributes for features which are not covered by the standard ones.

| Attribute              | Namespace                               | Property                       | Meaning                                  |
|------------------------|-----------------------------------------|--------------------------------|------------------------------------------|
| `Display`              | `System.ComponentModel.DataAnnotations` | `AutoGenerateField`            | Specifies whether the field should be generated or not. |
| `Display`              | `System.ComponentModel.DataAnnotations` | `Name`                         | Provides the label text for the field. If you set also the `ResourceType` property, the value will be looked up in the specified RESX file. |
| `Display`              | `System.ComponentModel.DataAnnotations` | `Prompt`                       | Provides the placeholder text for empty field. If you set also the `ResourceType` property, the value will be looked up in the specified RESX file. |
| `Display`              | `System.ComponentModel.DataAnnotations` | `Description`                  | Provides the description text for the field. If you set also the `ResourceType` property, the value will be looked up in the specified RESX file. |
| `Display`              | `System.ComponentModel.DataAnnotations` | `Order`                        | Provides the order of the field. If the property is not set, Auto UI will list the properties in the order they were declared in the class. |
| `Display`              | `System.ComponentModel.DataAnnotations` | `GroupName`                    | Assigns the field in a group. The [Form](~/controls/builtin-autoui/Form) and [GridViewColumns](~/controls/builtin-autoui/GridViewColumn) controls can specify the `GroupName` property to include only properties from a specific group. |
| `DisplayFormat`        | `System.ComponentModel.DataAnnotations` | `DataFormatString`             | Specifies the format string for the field value. |
| `DisplayFormat`        | `System.ComponentModel.DataAnnotations` | `NullDisplayText`              | Specifies the text to be displayed when the field is null. |
| `DataType`             | `System.ComponentModel.DataAnnotations` | `DataType`                     | Specifies more precise classification of the field value which is reflected by the generated control (e. g. `Password`, `MultilineText`, `Url`...) |
| `Editable`             | `System.ComponentModel.DataAnnotations` | `AllowEdit`                    | Specifies whether the field can be edited or not. |
| `Enabled`              | `DotVVM.AutoUI.Annotations`             | `IsAuthenticated`              | Specifies whether the field should be editable for authenticated or non-authenticated users, or for both (default behavior). |
| `Enabled`              | `DotVVM.AutoUI.Annotations`             | `Roles`                        | Specifies a name of the role or an expression that specifies for which roles the field should be editable. You can use ! (NOT), & (AND) and | (OR) operators. |
| `Enabled`              | `DotVVM.AutoUI.Annotations`             | `ViewNames`                    | Specifies a name of the view or an expression that specifies for which views (set with the `ViewName` property on the [Form](~/controls/builtin-autoui/Form) and [GridViewColumns](~/controls/builtin-autoui/GridViewColumn) controls) the field should be editable. You can use ! (NOT), & (AND) and | (OR) operators. |
| `Selection`            | `DotVVM.AutoUI.Annotations`             | `SelectionType`                | Defines that the user will be selecting the value from a list of options, and defines the type of selection. A service implementing `ISelectionProvider<TSelectionType>` must be provided. See the [Selectors](./selectors) chapter for more info. |
| `Styles`               | `DotVVM.AutoUI.Annotations`             | `FormControlContainerCssClass` | Specifies the CSS class applied to the container of the form control (e.g. table cell which contains the TextBox control) for this field. |
| `Styles`               | `DotVVM.AutoUI.Annotations`             | `FormRowCssClass`              | Specifies the CSS class applied to the row in the form (e.g. table row which contains the label and the TextBox control) for this field. |
| `Styles`               | `DotVVM.AutoUI.Annotations`             | `FormControlCssClass`          | Specifies the CSS class applied to the control in the form. |
| `Styles`               | `DotVVM.AutoUI.Annotations`             | `GridCellCssClass`             | Specifies the CSS class applied to the GridView table cell for this field. |
| `Styles`               | `DotVVM.AutoUI.Annotations`             | `GridHeaderCellCssClass`       | Specifies the CSS class applied to the GridView table header cell for this field. |
| `Visible`              | `DotVVM.AutoUI.Annotations`             | `IsAuthenticated`              | Specifies whether the field should be visible for authenticated or non-authenticated users, or for both (default behavior). |
| `Visible`              | `DotVVM.AutoUI.Annotations`             | `Roles`                        | Specifies a name of the role or an expression that specifies for which roles the field should be visible. You can use ! (NOT), & (AND) and | (OR) operators. |
| `Visible`              | `DotVVM.AutoUI.Annotations`             | `ViewNames`                    | Specifies a name of the view or an expression that specifies for which views (set with the `ViewName` property on the [Form](~/controls/builtin-autoui/Form) and [GridViewColumns](~/controls/builtin-autoui/GridViewColumn) controls) the field should be visible. You can use ! (NOT), & (AND) and | (OR) operators. |
| `UIHint`               | `System.ComponentModel.DataAnnotations` | `UIHint`                       | Specifies a identifier or a custom editor control which will be used for the property. This attribute can be specified multiple times to provide multiple UI hints. |

## Convention-based approach

