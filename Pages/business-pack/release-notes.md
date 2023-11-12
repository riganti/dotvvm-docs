# Release notes

## 4.2.0

### New features
* New `DotVVM.BusinessPack.Messaging` package which provides a simple way to push notifications from the server into the page. See [MessagingConnection](https://www.dotvvm.com/docs/4.0/controls/businesspack-messaging/MessagingConnection) for more info.
* New `DotVVM.BusinessPack.AutoUI` package which extends [Auto UI](https://www.dotvvm.com/docs/4.0/pages/concepts/auto-ui/overview) with Business Pack controls
* Support for CSS layers (can be turned in options when calling `AddBusinessPack`)
* Support for including a bundle with Business Pack scripts and styles instead of chunks (can be turned in options when calling `AddBusinessPack`)
* `DropDownButton` - added `Opened` property
* `NumericUpDown` - added `ButtonPlacement` property
* `GridView` - support for localization in filter values
* `GridView` - filters are supported when a simple expression is used on the column value, e. g. `SomeDate.ToBrowserLocalTime()`
* `TreeView` - performance improvements for expanding larger trees
* `TreeView` - added `ExpandOnChecked` property

### Package updates
- DotVVM upgraded to **4.2**

## 4.1.3

### Bug fixes
* Fixed `ComboBox` provider in DotVVM Dynamic Data providers.

## 4.1.2

### Bug fixes
* `MultiSelect` control doesn't remove values which are not found in the `DataSource` property. This helps to use the control in cases when the data source is loaded lazily (e. g. when using [REST API bindings](https://www.dotvvm.com/docs/4.0/pages/concepts/respond-to-user-actions/rest-api-bindings/overview)).
* `ComboBox` control works correctly when its `DataSource` contains multiple items with the same display text (provided by `ItemTextBinding`). Previously, the control changed the selected value to the first entry with matching text when it lost the focus.

## 4.1.1

### New features
* The CSS rules of Business Pack are now contained within the `dotvvm-business-pack` and `dotvvm-theme` [CSS layers](https://developer.mozilla.org/en-US/docs/Web/CSS/@layer).

### Package updates
- DotVVM upgraded to **4.1**

## 4.0.10

### Bug fixes
* Changed hover effect on `CheckBox` and `RadioButton` controls in the Bootstrap 4 theme.
* Fixed `GridView` and `FilterBuilder` filtering condition for `NotEquals` on `string` properties

## 4.0.9

### Bug fixes
* Changed hover effect on `CheckBox` and `RadioButton` controls in the Enterprise theme.

## 4.0.8

### Bug fixes
* Changed CSS styles for `CheckBoxList` control to use CSS grid - it should now better position multiple boxes with labels of varying sizes.

## 4.0.7

### Bug fixes
* Fixed the overlay under the `RangeSlider` control with non-zero `Minimum` values.

## 4.0.5

### Bug fixes
* Fixed the overlay under the `ModalDialog` control.
* Fixed the frozen headers feature (the `FixedHeaderRow` property) in the `GridView` control.
* Fixed the NuGet package publish issue in `DotVVM.BusinessPack.Messaging` - the `tsconfig.json` and `package.json` files are not linked to the project any more.

## 4.0.4

### Bug fixes
* Fixed the appearance of filter controls hosted in the `GridView` header row (rounded corners) in the Bootstrap theme.
* Fixed the appearance of `CheckBox` and `RadioButton` controls in the Bootstrap theme.

## 4.0.3

### Bug fixes
* Cleanup and tiny fixes in CSS styles.

## 4.0.2

### Bug fixes
* Cleanup and tiny fixes in CSS styles.
* Fixed the appearance of the `DropDownList` control in the Bootstrap theme.

## 4.0.1

### Bug fixes
* Fixed Bootstrap theme which wasn't loading correctly in all cases.

## 4.0.0

### New features
* Support for DotVVM 4.0 branch of the framework.
* The CSS layer was rewritten to use CSS variables. See the [themes/customize](Customize Business Pack Theme) for more information.
* The underlying library for producing Excel files has changed to [ClosedXML](https://github.com/ClosedXML/ClosedXML). 
* New API for exporting data into Excel was introduced. See [exporting-data](Exporting data) for more information. 
* There is a new `Messenger` control contained in a separate NuGet package `DotVVM.BusinessPack.Messaging`. This control allows to easily call commands in the client's page from the server. Internally, this component uses the ASP.NET Core SignalR library.

### Package updates
- DotVVM upgraded to **4.0**

## 3.0.0-preview03-final

### New controls
  * `RichTextBox`
  * `ImageCrop`
### New features
  * New styling possibilities through CSS variables
  * Extended `GridView` export to Excel to support data types and extensibility
  * Property `TabIndex` made bindable on most controls  
### Changes to existing controls
#### `MultiSelect`
- `NewItemAdded` event added to be used with the `AllowNewItems` property
#### `Button`
- `Outline` property added for outline style
#### `TextBox`
 - Wrapped in `<div>` tag
