# Release notes

## 3.0.6
### Package updates
- DotVVM upgraded to **3.0.5**

### Changes to existing controls
- Fixed few bugs in `DateTimePicker`.

## 3.0.1
### Package updates
- DotVVM upgraded to **3.0.1**

## 2.5.2
### Changes to existing controls
#### Tooltip
- *Placement* now supports option *auto*.
- **BREAKING CHANGE** : Default value for placement is now *auto*.
#### ResponsiveNavigation
-  Header can be now set using *HeaderTemplate*. Content of this template gets inserted into header link.
#### GridView
-  Added new *TableTheme* enum options (such as danger,warning,...)

#### DateTimePicker
- Fixed popup positioning on longer pages.
- Modified algorithm which decides whether popup displays above or bellow input.
 
#### Icon                     
- Optimized icon serving when using binging. 

## 2.5.1
### Package updates
- DotVVM upgraded to **2.5.1**
### Changes to existing controls
#### TabControl
- Added support for setting *Visible* property on *TabItem*
#### ResponsiveNavigation
        * Added property  *HeaderImageAltText* for setting alt text of image.
        * Added property *HeaderImageTooltip* for setting title property of image.

## 2.4.0.10
### New controls
- [Icon](~/controls/bootstrap4/Icon)
    
### Changes to existing controls
#### **Toast**
- Fixed bug in *OnShown* and *OnHide* events.
-  *OnShown* and *OnHide* events are no longer called immediately after page load. Those events are now called only after client side change.
#### Other changes
- General bug fixes in Custom CSS feature.

## 2.3.1

### Package updates
- Dotvvm upgraded to **2.3.1**

### Changes to existing controls
#### **Tooltip** and **Popover**
- Added *AutoCloseMonitoringDepth* property which allows to set how many levels of ancestors would be monitored for element removal. 
        Detecting removal of **Tooltip** | **Popover** source element or its parents would close **Tooltip** | **Popover**.
## 2.3.0

### Package updates
- Dotvvm upgraded to **2.3.0**

### Changes to existing controls
#### CheckBox
- Added *RenderLabel* property which allows user to set whether checkbox should render label.  
When not set than label will be rendered only if needed.
#### RadioButton
- Added *RenderLabel* property which allows user to set whether checkbox should render label.  
        When not set than label will be rendered only if needed.

### Other changes
- Users can now specify their own IResource to be used as BoostrapJs, BootstrapCss and JQuery.  
    [Usage example](getting-started)
    
## 2.2.0

### Package updates
- Dotvvm upgraded to **2.2.0**

### Bug fixes
#### BootstrapItemsControl     
- Base for most controls with DataSource*
- Data context is now correctly set for child controls.
#### GridView
- All HTML attributes are now present on correct element when *MaximumScreenSizeBeforeScrollBarShows* is set to *None*

    


## 2.1.0
**First stable (non-preview) release.**

### Package updates
- Dotvvm upgraded to **2.1.0**
- Bootstrap upgraded to **4.3.1**

### New controls
- [Spinner](~/controls/bootstrap4/Spinner)
- [Toast](~/controls/bootstrap4/Toast)

### Changes to existing controls
#### Carousel
- Added *ImageAlternateTextBinding* property
#### CheckBox
- **BREAKING CHANGE** : enum *BootstrapFormStyle* used in *FormControlStyle* property renamed to *CheckBoxStyle*
#### CheckBoxFormGroup
- **BREAKING CHANGE** : enum *BootstrapFormStyle* used in *FormControlStyle* property renamed to *CheckBoxStyle*
 #### GridView
-  Added *Border* property
-  Added *Caption* property
-  **BREAKING CHANGE** : Boolean - IsResponsive* property was replaced by - MaximumScreenSizeBeforeScrollBarShows* enum.
#### ListGroup
-  Added *Type* property
-  Added - MaximumScreenSizeBeforeChangeToVertical* property
#### ModalDialog
- Added *ScrollableContent* property
- Added new XL *Size*
#### Popover
- Added *AllowHtmlSanitization* property
#### ResponsiveNavigation
- **BREAKING CHANGE** : enum *ResponsiveNavigationBreakpoins* used in *MaximumScreenSizeBeforeCollapse* property was renamed to *ResponsiveBreakpoints*
#### Tooltip
-  Added *AllowHtmlSanitization* propery
        
## 2.0.0-preview02-final
### New controls
  * [CollapsiblePanel](~/controls/bootstrap4/CollapsiblePanel)
  * [ComboBoxFormGroup](~/controls/bootstrap4/ComboBoxFormGroup)
  * [DateTimePickerFormGroup](~/controls/bootstrap4/DateTimePickerFormGroup)
  * [RadioButtonFormGroup](~/controls/bootstrap4/RadioButtonFormGroup)
  * [TextBoxFormGroup](~/controls/bootstrap4/TextBoxFormGroup)

### Changes to existing controls
- Every items control (`Accordion`, `NavigationBar`, etc.) now accepts any child control which implements given interface instead of one concrete implementation (`IAccordionItem` instead of `AccordionItem`).