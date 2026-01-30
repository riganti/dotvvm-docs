# Built-in controls

DotVVM ships with about 25 built-in controls that cover most frequent scenarios. Some of them are just wrappers for standard HTML elements (inputs and various form elements), some offer complex functionalities (rendering tables with data).

The built-in controls are universal and do not include any styles or themes. They render simple HTML that can be easily styled by CSS. 

If you plan to build a larger application, check out our [commercial components](https://www.dotvvm.com/products) - they offer advanced functions as well as customizable themes:

* [DotVVM Business Pack](https://www.dotvvm.com/products/dotvvm-business-pack) - an enteprise-grade set of components with customizable look & feel
* [Bootstrap for DotVVM](https://www.dotvvm.com/products/bootstrap-for-dotvvm) - easy-to-use DotVVM wrappers for **Bootstrap 3, 4, and 5** 

## Form controls

+ [Button](~/controls/builtin/Button) - `button` or `input[type=button]` that triggers a postback
+ [ComboBox](~/controls/builtin/ComboBox) - standard HTML `select` with advanced binding options
+ [CheckBox](~/controls/builtin/CheckBox) - standard HTML `input[type=checkbox]`
+ [FileUpload](~/controls/builtin/FileUpload) - renders a stylable file upload control with progress indication
+ [HtmlLiteral](~/controls/builtin/HtmlLiteral) - renders a HTML content in the page
+ [Label](~/controls/builtin/Label) - renders a `label` element for a specified form control, ensuring a unique control ID
+ [LinkButton](~/controls/builtin/LinkButton) - a hyperlink that triggers the postback
+ [ListBox](~/controls/builtin/ListBox) - standard HTML `select` list that allows selecting a single value
+ [Literal](~/controls/builtin/Literal) - renders a text in the page, supports date and number formatting
+ [ModalDialog](~/controls/builtin/ModalDialog) - allows to display a modal dialog using the `<dialog>` HTML element
+ [MultiSelect](~/controls/builtin/MultiSelect) - standard HTML `select` list that allows selecting multiple values
+ [RadioButton](~/controls/builtin/RadioButton) - standard HTML `input[type=radio]`
+ [RouteLink](~/controls/builtin/RouteLink) - renders a hyperlink that navigates to a specified route with specified parameters
+ [TextBox](~/controls/builtin/TextBox) - HTML `input` or `textarea`

## Validation
+ [Validator](~/controls/builtin/Validator) - displays a validation error for particular field
+ [ValidationSummary](~/controls/builtin/ValidationSummary) - displays a list of validation errors

## Collections and data
+ [DataPager](~/controls/builtin/DataPager) - displays a list of pages in the grid
+ [HierarchyRepeater](~/controls/builtin/HierarchyRepeater) - repeats a template for each item in the collection, with support for recursive child items
+ [GridView](~/controls/builtin/GridView) - displays a table grid with sort and inline edit functionality
+ [Repeater](~/controls/builtin/Repeater) - repeats a template for each item in the collection
+ [EmptyData](~/controls/builtin/EmptyData) - displays a content when a collection is empty

## Master pages
+ [Content](~/controls/builtin/Content) - defines a content that is hosted in `ContentPlaceHolder`
+ [ContentPlaceHolder](~/controls/builtin/ContentPlaceHolder) - defines a place where the content page is hosted
+ [SpaContentPlaceHolder](~/controls/builtin/SpaContentPlaceHolder) - a `ContentPlaceHolder` which works as a Single Page Application container

## Conditional views
+ [AuthenticatedView](~/controls/builtin/AuthenticatedView) - displays some content to the authenticated users only
+ [ClaimView](~/controls/builtin/ClaimView) - displays some content if the current user has a particular claim
+ [EnvironmentView](~/controls/builtin/EnvironmentView) - displays some content in a particular environment (e.g. Debug, Production)
+ [RoleView](~/controls/builtin/RoleView) - displays some content if the current user is in a particular role

## Other controls
+ [AlternateCultureLinks](~/controls/builtin/AlternateCultureLinks) - renders `<meta>` tags pointing to [localized versions](~/pages/concepts/routing/route-localization) of the current page
+ [InlineScript](~/controls/builtin/InlineScript) - includes an inline JavaScript snippet in the page
+ [JsComponent](~/controls/builtin/JsComponent) - allows hosting components implemented in **React**, **Svelte**, or other frameworks
+ [NamedCommand](~/controls/builtin/NamedCommand) - provides a way to call a server commands from JavaScript code
+ [PlaceHolder](~/controls/builtin/PlaceHolder) - allows to wrap any content without rendering any HTML element 
+ [RequiredResource](~/pages/concepts/script-and-style-resources/use-resources-in-pages) - includes a script or style resource in the page
+ [TemplateHost](~/controls/builtin/TemplateHost) - used when building [Composite controls](~/pages/concepts/control-development/composite-controls) to host a template passed by the user 
+ [UpdateProgress](~/controls/builtin/UpdateProgress) - displays specified content during the postback

## DotVVM Contrib

There is also a [DotVVM Contrib repository](https://github.com/riganti/dotvvm-contrib) which features various community-authored components. Anyone can contribute to the repository and submit their own controls.

* [BootstrapColorpicker](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/BootstrapColorpicker)
* [BootstrapDatepicker](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/BootstrapDatepicker)
* [CkEditorMinimal](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/CkEditorMinimal)
* [CookieBar](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/CookieBar)
* [EditableForm](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/EditableForm)
* [FAIcon](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/FAIcon)
* [GoogleAnalyticsJavascript](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/GoogleAnalyticsJavascript)
* [GoogleMap](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/GoogleMap)
* [HeroIcon](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/HeroIcon)
* [LoadablePanel](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/LoadablePanel)
* [MultilevelMenu](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/MultilevelMenu)
* [NoUiSlider](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/NoUiSlider)
* [PolicyView](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/PolicyView)
* [PolymorphTemplateSelector](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/PolymorphTemplateSelector)
* [QrCode](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/QrCode)
* [Select2](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/Select2)
* [SvgParser](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/SvgParser)
* [TemplateSelector](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/TemplateSelector)
* [TypeAhead](https://github.com/riganti/dotvvm-contrib/tree/main/Controls/TypeAhead)

## See also

* [Common control properties](~/pages/concepts/dothtml-markup/common-control-properties)
* [Data-binding](~/pages/concepts/data-binding/overview)
* [Respond to user actions](~/pages/concepts/respond-to-user-actions/overview)
* [DotVVM Business Pack](https://www.dotvvm.com/products/dotvvm-business-pack)
* [Bootstrap for DotVVM](https://www.dotvvm.com/products/bootstrap-for-dotvvm)