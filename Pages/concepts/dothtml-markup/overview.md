# Built-in Controls

**DotVVM** contains about 25 built-in controls for various purposes.
For more information, read the [Built-in Control Reference](/docs/controls/builtin/Button/{branch}).

## Built-in Controls

### Forms
+ [Button](/docs/controls/builtin/Button/{branch}) - `button` or `input[type=button]` that triggers a postback
+ [ComboBox](/docs/controls/builtin/ComboBox/{branch}) - standard HTML `select` with advanced binding options
+ [CheckBox](/docs/controls/builtin/CheckBox/{branch}) - standard HTML `input[type=checkbox]`
+ [FileUpload](/docs/controls/builtin/FileUpload/{branch}) - renders a stylable file upload control with progress indication
+ [HtmlLiteral](/docs/controls/builtin/HtmlLiteral/{branch}) - renders a HTML content in the page
+ [LinkButton](/docs/controls/builtin/LinkButton/{branch}) - a hyperlink that triggers the postback
+ [ListBox](/docs/controls/builtin/ListBox/{branch}) - standard HTML `select[multiple]`
+ [Literal](/docs/controls/builtin/Literal/{branch}) - renders a text in the page, supports date and number formatting
+ [RadioButton](/docs/controls/builtin/RadioButton/{branch}) - standard HTML `input[type=radio]`
+ [RouteLink](/docs/controls/builtin/RouteLink/{branch}) - renders a hyperlink that navigates to a specified route with specified parameters
+ [TextBox](/docs/controls/builtin/TextBox/{branch}) - HTML `input` or `textarea`

### Validation
+ [Validator](/docs/controls/builtin/Validator/{branch}) - displays a validation error for particular field
+ [ValidationSummary](/docs/controls/builtin/ValidationSummary/{branch}) - displays a list of validation errors

### Collections
+ [DataPager](/docs/controls/builtin/DataPager/{branch}) - displays a list of pages in the grid
+ [GridView](/docs/controls/builtin/GridView/{branch}) - displays a table grid with sort and inline edit functionality
+ [Repeater](/docs/controls/builtin/Repeater/{branch}) - repeats a template for each item in the collection
+ [EmptyData](/docs/controls/builtin/EmptyData/{branch}) - displays a content when a collection is empty

### Master Pages
+ [Content](/docs/controls/builtin/Content/{branch}) - defines a content that is hosted in `ContentPlaceHolder`
+ [ContentPlaceHolder](/docs/controls/builtin/ContentPlaceHolder/{branch}) - defines a place where the content page is hosted
+ [SpaContentPlaceHolder](/docs/controls/builtin/SpaContentPlaceHolder/{branch}) - a `ContentPlaceHolder` which works as a Single Page Application container

## Conditional Views
+ [AuthenticatedView](/docs/controls/builtin/AuthenticatedView/{branch}) - displays some content to the authenticated users only
+ [ClaimView](/docs/controls/builtin/ClaimView/{branch}) - displays some content if the current user has a particular claim
+ [EnvironmentView](/docs/controls/builtin/EnvironmentView/{branch}) - displays some content in a particular environment (e.g. Debug, Production)
+ [RoleView](/docs/controls/builtin/RoleView/{branch}) - displays some content if the current user is in a particular role

### Other
+ [InlineScript](/docs/controls/builtin/InlineScript/{branch}) - includes an inline JavaScript snippet in the page
+ [RequiredResource](/docs/tutorials/basics-javascript-and-css/{branch}) - includes a script or style resource in the page
+ [UpdateProgress](/docs/controls/builtin/UpdateProgress/{branch}) - displays specified content during the postback

