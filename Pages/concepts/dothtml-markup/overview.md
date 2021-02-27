# DotHTML markup overview

## View

The view is a file with `.dothtml` extension. The views in DotVVM use standard HTML syntax with three flavors:

* **Directives** are placed at the top of the file. They define some page related information.
There is only one directive which is mandatory, and which must be present in each `.dothtml` file:

```DOTHTML    
    @viewModel Namespace.ClassName, Assembly
```

* **Data-binding Expressions** allow to bind properties declared in the viewmodel to the specified place in UI, or to call commands on the viewmodel.

```DOTHTML
<p>Hi, my name is {{value: UserName}}!</p>
<p><img src="{value: ProfileImageUrl}" alt="Profile Image"/></p>
```

* **DotVVM Controls** are standard HTML elements with a namespace prefix. These prefixes are not registered using 
the xmlns attributes like in XML-like languages. Instead, DotVVM looks in its configuration class (`DotvvmStartup`) for registered namespaces, and looks for the controls in these namespaces.
Each DotVVM control can specify several properties which are specified using attributes or child elements.

```DOTHTML
    <dot:Repeater DataSource="..." style="display: none">
        <ItemTemplate>
            some content...
        </ItemTemplate>
    </dot:Repeater>
```

In the code sample above you can see the `Repeater` control. It has the `DataSource` property specified and an attribute
of the `<dot:Repeater>` element, and the `ItemTemplate` property which is specified inside the `<dot:Repeater>` element. 

Whether the property is specified as an attribute, or as an child element, is defined by the developer of the control. 

> The [DotVVM for Visual Studio](/landing/dotvvm-for-visual-studio-extension) extension supports IntelliSense
and it will tell you which controls and properties are available.

If you want to apply any other HTML attribute to a control, you can do it on most of the controls. Notice the `style` attribute applied to the 
`Repeater` control in our example. The most common use case for this is to add a `class` attribute to a DotVVM control.

## ViewModel

The **viewmodel** is a plain C# class which is referenced in the `.dothtml` file using the `@viewModel` directive. Any .NET class can be the viewmodel, however there is one thing to remember:

> The DotVVM viewmodel class must be JSON-serializable. To make everything work properly, we recommend to use only public properties and public methods.

Don't launch rockets in the space in your getters, setters and constructor(s) because they can be executed several times during the JSON serialization. If possible, don't put any logic in your getters and setters. You don't know in which order the serializer will set the values in the properties! 

### DotvvmViewModelBase

We recommend you to make your viewmodels derive from the `DotVVM.Framework.ViewModel.DotvvmViewModelBase` class. This class contains the `Context` property which will allow you to interact with the HTTP request, e.g. do a redirect to another page or route, access the configuration and much more. 

The `DotvvmViewModelBase` class also declares the `Init`, `Load` and `PreRender` methods. You can override them to perform tasks in specific state of the HTTP request execution. You'll find more information about them in the [ViewModels](/docs/tutorials/basics-viewmodels/{branch}) chapter.

If you cannot derive from the `DotvvmViewModelBase` class, you can implement the `IDotvvmViewModel` interface which declares the `Context` property too. **DotVVM** will make sure that you can access the HTTP request context using this property.


## Built-in Controls

**DotVVM** contains about 25 built-in controls for various purposes.
For more information, read the [Built-in Control Reference](/docs/controls/builtin/Button/{branch}).

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

