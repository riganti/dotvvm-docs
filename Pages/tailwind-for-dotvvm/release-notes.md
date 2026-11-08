# Release notes

## 5.1.0

Requires **DotVVM 5.0.0** or newer.

### New controls

* **Charts** - [BarChart](~/controls/tailwind/BarChart), [LineChart](~/controls/tailwind/LineChart), [DonutChart](~/controls/tailwind/DonutChart), [PolarAreaChart](~/controls/tailwind/PolarAreaChart), [RadarChart](~/controls/tailwind/RadarChart), [ScatterChart](~/controls/tailwind/ScatterChart), and [BubbleChart](~/controls/tailwind/BubbleChart), with the `ChartSeries` and `PointSeries` controls for defining data series. The charts are rendered with Chart.js, which is loaded only on pages that contain a chart.
* **Stats** - [StatCard](~/controls/tailwind/StatCard) with four layouts (`Default`, `DiffIcon`, `Ring`, and `TopIcon`), [StatGroupedCard](~/controls/tailwind/StatGroupedCard), and [StatSegmentsCard](~/controls/tailwind/StatSegmentsCard).
* **[MultiSelect](~/controls/tailwind/MultiSelect)** and `MultiSelectFormField` - a searchable selector for multiple values with optional server-side search.
* **[FileUpload](~/controls/tailwind/FileUpload)** - a drop zone for uploading files with client-side validation of file types, sizes, and counts, and a compact mode for forms.
* **[ImageCrop](~/controls/tailwind/ImageCrop)** - an image editor with cropping, zooming, rotating, and flipping, and the `ImageService` class for validating and re-encoding images on the server.
* **[FilePreview](~/controls/tailwind/FilePreview)** and **[UploadedFilesPreview](~/controls/tailwind/UploadedFilesPreview)** - previews of images and files.
* **[TextBoxGroup](~/controls/tailwind/TextBoxGroup)** - a composed input with header and footer templates (similar to prompt inputs in AI chat applications), together with the `TextBoxGroupButton`, `TextBoxGroupComboBox`, `TextBoxGroupDropDownButton`, and `TextBoxGroupLiteral` controls.
* **[TextView](~/controls/tailwind/TextView)** - safe rendering of plain text or Markdown with automatic links.
* **[Popover](~/controls/tailwind/Popover)** - a floating panel opened on click or hover.
* **[Collapse](~/controls/tailwind/Collapse)** - a standalone collapsible section.
* **[Rating](~/controls/tailwind/Rating)** - a read-only or interactive rating with half-point precision.
* **[DraggableList](~/controls/tailwind/DraggableList)** - a list with reorderable items and transfers between lists.
* **[GridLayout](~/controls/tailwind/GridLayout)** - a CSS grid with a responsive number of columns.
* **Skeletons** - [SkeletonCard](~/controls/tailwind/SkeletonCard), [SkeletonList](~/controls/tailwind/SkeletonList), [SkeletonParagraph](~/controls/tailwind/SkeletonParagraph), and [SkeletonTable](~/controls/tailwind/SkeletonTable) placeholders for loading content.

### Improvements

* [GridView](~/controls/tailwind/GridView) supports filtering - a full-text search box and filter chips are displayed when the columns have `AllowFiltering="true"`. Use the new `FilteredGridViewDataSet<T>` class in the viewmodel. New Tailwind columns were added: `GridViewTextColumn`, `GridViewTemplateColumn`, `GridViewCheckBoxColumn`, `GridViewDateColumn`, `GridViewDateTimeColumn`, and `GridViewMultiSelectColumn`.
* The [ComboBox](~/controls/tailwind/ComboBox) was rewritten as a searchable selector. It supports typing to filter the items, keyboard navigation, server-side search using the `Search` static command, selecting whole objects using `ItemKeyBinding`, and a `Placeholder`.
* All [form fields](~/controls/tailwind/Form) share the same set of properties. They support `HelpText` and `HelpTemplate`, the `TextBoxFormField` supports `Placeholder`, and the validation message for the bound value is displayed automatically.
* All built-in texts of the controls (labels for screen readers, button texts, messages) are localized. The package contains English and Czech texts, and you can provide other languages or override individual texts - see [Localization](getting-started#localization).
* The [DateTimePicker](~/controls/tailwind/DateTimePicker) uses `CurrentCulture` for the format of the date and time input, and `CurrentUICulture` for the calendar texts. The new `UICulture` property can override the calendar language.
* Items of [ListGroup](~/controls/tailwind/List) don't need to be clickable or navigable.
* The `Wrap` property of [StackLayout](~/controls/tailwind/StackLayout) supports data-binding.
* [Notifications](~/controls/tailwind/Notifications) are displayed above the backdrop of an open modal dialog.
* The sample application contains a new page about theming which explains how to customize colors, fonts, corner radius, and layout dimensions using CSS variables, either for the whole application or for a part of the page.
* Fixed the outline variant of the `PercentBadge` icon which did not follow the text color.

### Breaking changes

* The `ComboBox` control does not inherit from the built-in `ComboBox` anymore. The `DataSource`, `SelectedValue`, `ItemTextBinding`, `ItemValueBinding`, `EmptyItemText`, `SelectionChanged`, and `Enabled` properties work as before, but other properties inherited from the built-in control are not available.
* The prefix of HTML attributes which are applied to the input element in form fields was unified to `Control-`. Replace the `TextBox-`, `ComboBox-`, and `CheckBox-` prefixes (for example `TextBox-class`) with `Control-` (for example `Control-class`).
* The `Wrap` property of `FormRow` was renamed to `IsWrappingEnabled`.
* The `Culture` property of `DateTimePicker` now also affects the format of the date and time input. When it is not set, the input format follows `CurrentCulture` instead of `CurrentUICulture`.
* The package has new dependencies: `SkiaSharp` (used by `ImageService`) and `Markdig` (used by `TextView`).

## 5.0.0

The first stable version of the library. Requires **DotVVM 5.0.0**.

Most of the changes since the RC 1 version unified the API of the controls:

* New [AppLayout](~/controls/tailwind/AppLayout) control for building the page layout with the top and side menus.
* `ListControl` was renamed to [ListGroup](~/controls/tailwind/List), and `RangeInterval` was renamed to [RangeSlider](~/controls/tailwind/RangeSlider).
* New [DateTimePicker](~/controls/tailwind/DateTimePicker) and `DateTimePickerFormField` controls replaced the previous date picker. They work with `DateTime?` values and support date, time, and date-time modes, format strings, and value limits.
* The `Color` property of [ProgressBar](~/controls/tailwind/ProgressBar) was renamed to `Type`.
* [Breadcrumb](~/controls/tailwind/Breadcrumb), [TopMenu](~/controls/tailwind/TopMenu), [SideMenu](~/controls/tailwind/SideMenu), and [DropDownButton](~/controls/tailwind/DropDownButton) items share the same item capabilities, and support text, templates, icons, URLs, routes, and click commands consistently.
* [Button](~/controls/tailwind/Button) and [RouteLink](~/controls/tailwind/RouteLink) support `Icon` and `IconPosition`, and use the same text and content properties.
* [Icon](~/controls/tailwind/Icon) supports data-binding of the `Icon` property.
* [RadioButton](~/controls/tailwind/RadioButton) supports the `GroupName` property.
* `ModalConfirmPostBackHandler` supports the `ConfirmButtonType` and `CancelButtonType` properties.
* [DataPager](~/controls/tailwind/DataPager) supports the `RenderLinkForCurrentPage` property.
* [Spinner](~/controls/tailwind/Spinner) supports the `AriaLabelText` property.
* Accessibility improvements in [ModalDialog](~/controls/tailwind/ModalDialog) (focus handling), [Notifications](~/controls/tailwind/Notifications), [Alert](~/controls/tailwind/Alert), and other controls.
* Clickable controls display the hand cursor.
* Property names of [Card](~/controls/tailwind/Card), [Accordion](~/controls/tailwind/Accordion), [Carousel](~/controls/tailwind/Carousel), [TabControl](~/controls/tailwind/TabControl), [ButtonGroup](~/controls/tailwind/ButtonGroup), and [ProcessTimeline](~/controls/tailwind/ProcessTimeline) were revised to follow the same conventions as other controls.

## 5.0.0 RC 1

First version of the library with the basic set of components. Requires **DotVVM 5.0 Preview 7**.
