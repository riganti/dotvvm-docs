# Built-in controls

DotVVM ships with about 25 built-in controls that cover most frequent scenarios. Some of them are just wrappers for standard HTML elements (inputs and various form elements), some offer complex functionalities (rendering tables with data).

The built-in controls are universal and do not include any styles or themes. They render simple HTML that can be easily styled by CSS. 

If you plan to build a larger application, check out our [commercial components](https://www.dotvvm.com/products) - they offer advanced functions as well as customizable themes:

* [DotVVM Business Pack](https://www.dotvvm.com/products/dotvvm-business-pack)
* [Bootstrap for DotVVM](https://www.dotvvm.com/products/bootstrap-for-dotvvm)

## Form controls

+ [Button](~/controls/builtin/Button) - `button` or `input[type=button]` that triggers a postback
+ [ComboBox](~/controls/builtin/ComboBox) - standard HTML `select` with advanced binding options
+ [CheckBox](~/controls/builtin/CheckBox) - standard HTML `input[type=checkbox]`
+ [FileUpload](~/controls/builtin/FileUpload) - renders a stylable file upload control with progress indication
+ [HtmlLiteral](~/controls/builtin/HtmlLiteral) - renders a HTML content in the page
+ [LinkButton](~/controls/builtin/LinkButton) - a hyperlink that triggers the postback
+ [ListBox](~/controls/builtin/ListBox) - standard HTML `select[multiple]`
+ [Literal](~/controls/builtin/Literal) - renders a text in the page, supports date and number formatting
+ [RadioButton](~/controls/builtin/RadioButton) - standard HTML `input[type=radio]`
+ [RouteLink](~/controls/builtin/RouteLink) - renders a hyperlink that navigates to a specified route with specified parameters
+ [TextBox](~/controls/builtin/TextBox) - HTML `input` or `textarea`

## Validation
+ [Validator](~/controls/builtin/Validator) - displays a validation error for particular field
+ [ValidationSummary](~/controls/builtin/ValidationSummary) - displays a list of validation errors

## Collections and data
+ [DataPager](~/controls/builtin/DataPager) - displays a list of pages in the grid
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
+ [InlineScript](~/controls/builtin/InlineScript) - includes an inline JavaScript snippet in the page
+ [RequiredResource](~/pages/concepts/script-and-style-resources/use-resources-in-pages) - includes a script or style resource in the page
+ [UpdateProgress](~/controls/builtin/UpdateProgress) - displays specified content during the postback

## See also

* [Common control properties](~/pages/concepts/dothtml-markup/common-control-properties)
* [Data-binding](~/pages/concepts/data-binding/overview)
* [Respond to user actions](~/pages/concepts/respond-to-user-actions/overview)
* [DotVVM Business Pack](https://www.dotvvm.com/products/dotvvm-business-pack)
* [Bootstrap for DotVVM](https://www.dotvvm.com/products/bootstrap-for-dotvvm)