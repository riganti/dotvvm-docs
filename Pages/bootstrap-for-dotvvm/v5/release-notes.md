# Release notes

## 5.0.0
### Package updates
- DotVVM upgraded to **5.0**

## 4.3.3
### New features
- Added `CollapsedOrExpanded` event to `CollapsiblePanel` 

## 4.3.2
### New features
- Added `ListBox` and `MultiSelect` controls
- New subpackage for `Select2` wrappers with `Select2ComboBox` and `Select2MultiSelect`
- Added `ActiveTabChanged` event and `UrlFragment` property to `TabControl`

### Bug fixes
- Various fixed of `CollapsiblePanel` - e.g. collapsed by default
- Fixed bug for nested `TabControl`, fixed auto-selecting of first item
- `NavBarDropDown` - added `ItemTarget` property
- `DateTimePicker` - fixed issue with nullable date binding

## 4.3.1
### New features
- Added `Type` property to the `DateTimePicker` control
- Added `Alignment` property to the `DropDown` control
- Added `ModalConfirmPostBackHandler`

### Bug fixes
- Fixed `SelectedTabIndex` property in `TabControl`

## 4.3.0
### Bug fixes
- Fixed `SelectedTabIndex` property in `TabControl`
- Fixed backdrop when `ModalDialog` is removed from the DOM
- Fixed `ExpandedItemIndex` property in `Accordion`

### Package updates
- DotVVM upgraded to **4.3**

## 4.2.0
### Bug fixes
- Fixed support for numeric and date-time types in `FormControlTextBox` 
- Fixed support of nullable types in `DateTimePicker`
- Added support for `PostBack.Handlers` (thanks to adding support for Postback handlers in composite controls in DotVVM Framework)
- Fixed behavior of `TabItem` when the `Selected` property was a hard-coded value
- Fixed bundling of the included scripts and styles (minification and cleanup)

### Package updates
- DotVVM upgraded to **4.2**

## 4.1.0
- The first release of the library