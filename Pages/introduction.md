# Introduction

**DotVVM** is a UI framework that enables you to build ASP.NET Core (or old ASP.NET) websites and applications using the [Model-view-viewmodel](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel) pattern. 

DotVVM consists of a **server-side part** (which integrates in the ASP.NET infrastructure) and a **client-side part** based on [Knockout JS](https://knockoutjs.com/) which creates the MVVM experience in the web browser. 

The framework includes a **set of built-in components** (also called _controls_), and offers an [extension for Visual Studio 2019 and 2017](https://www.dotvvm.com/get-dotvvm) that brings IntelliSense.

## Use-cases for DotVVM

### Building apps with MVVM pattern

DotVVM **simplifies building enterprise applications**, admin dashboards, company portals, CRMs, ERPs or other applications commonly referred as [line of business applications](https://en.wikipedia.org/wiki/Line_of_business).

In general, DotVVM helps in applications which contain data grids, heavy forms with many form field, complex validation and business logic, pages with multi-step wizards, modal dialogs, or other “desktop-like” UI components.

Thanks to the **Model-view-viewmodel approach**, it is **easy and convenient** to build these kinds of experiences. 

DotVVM also helps with **delivering the data from the server to the browser** without the need to build and maintain REST APIs — the data loaded in the viewmodel are transferred to the browser transparently.

There is also a community extension called [DotVVM AMP](community-add-ons/dotvvm-amp) which can generate [AMP](https://en.wikipedia.org/wiki/Accelerated_Mobile_Pages) version of pages automatically, which can increase the site visibility in search engines and decrease the page load times.

See the [Quick start: Create a new project](quick-starts/build/create-new-project) chapter for more info.

> If you are not sure whether DotVVM is a good choice for your project, ask us on our [Gitter chat](https://gitter.im/riganti/dotvvm). We will be happy to help you with the decision.

### Modernizing legacy ASP.NET applications

DotVVM can also be used to incrementally [modernize old ASP.NET-based applications](quick-starts/modernize/add-dotvvm-to-existing-app) and migrate them to the latest versions of .NET.

DotVVM supports both ASP.NET Core and old ASP.NET (.NET 4.5.1 and newer) and **can run in the same process side-by-side with other frameworks** such as ASP.NET Web Forms. 

DotVVM can be installed in an existing ASP.NET Web Forms application and used to **replace ASPX pages** with their DotVVM equivalents **one by one**. During the process, the application will still work, so the developers can fix bugs, implement new features and modernize the code base at the same time. 

Most of the code (business logic, data access, integrations of other systems and so on) **won't need significant changes**, and after all dependencies on ASP.NET Web Forms and `System.Web` are replaced with DotVVM, the **project can be switched to the newest version** of .NET and ASP.NET Core.

See the [Quick start: Add DotVVM to existing app](quick-starts/modernize/add-dotvvm-to-existing-app) chapter for more info.

## Anatomy of a DotVVM page

Every DotVVM page consists of two files — the _view_ and the _viewmodel_. The view describes the content and structure of the page while the viewmodel maintains the page state and responds to actions made by users. 

### View

DotVVM uses an [extended HTML syntax](concepts/dothtml-markup/overview) called _DotHTML_ to define views. 

```DOTHTML
@viewModel DotvvmDemo.CalculatorViewModel, DotvvmDemo
    
<p>
    Enter the first number: 
    <dot:TextBox Text="{value: Number1}" />
</p>
<p>
    Enter the second number: 
    <dot:TextBox Text="{value: Number2}" />
</p>
<p>
    <dot:Button Text="Calculate" Click="{command: Calculate()}" />
</p>
<p>
    The result is: {{value: Result}}
</p>
```

DotHTML extends classic HTML syntax with the following constructs:

* _Directives_ — e.g. `@viewModel DotvvmDemo.CalculatorViewModel, DotvvmDemo`
* _Data-binding expressions_ — e.g. `{value: Number1}` or `{command: Calculate()}` in the sample above
* _DotVVM controls_ — e.g. `<dot:Button Text="Calculate" />`

When users of the web application navigate to the page, DotVVM translates the DotHTML markup to plain HTML syntax which can be displayed by the browser.

DotHTML files usually have the `.dothtml` extension. There are special kinds of views — [Master pages](concepts/layout/master-pages) or [Markup controls](concepts/control-development/markup-controls) — which are using different file extensions, but they are still using DotHTML syntax.

See the [DotHTML markup](concepts/dothtml-markup/overview) chapter for more info.

## Viewmodel

The viewmodel is a C# class with **public properties** representing the state of the page, and **public methods** handling actions triggered by users.

In contrast to other MVVM frameworks, viewmodel properties don't have to implement `INotifyPropertyChanged` — they can have just default getters and setters.

```CSHARP
using System;
    
namespace DotvvmDemo 
{
    public class CalculatorViewModel 
    {
            
        public int Number1 { get; set; }
            
        public int Number2 { get; set; }
            
        public int Result { get; set; }
            
        public void Calculate() 
        {
            Result = Number1 + Number2;
        }
    }
}
```

In the example above, when a user clicks a button in the page, the `Calculate` method is called. It can read or modify the state of the page via viewmodel properties. 

See the [Viewmodels](concepts/viewmodels/overview) chapter for more info.

## Data-binding

The viewmodel properties are bound to controls in the view using the data-binding expressions:

```DOTHTML
<dot:TextBox Text="{value: Number1}" />
```

The **data-binding works in both ways** by default:

* Whenever the value of the property changes, the control will be updated accordingly.
* Whenever the user changes the value in the control, the viewmodel property will be updated accordingly.

See the [Data-binding](concepts/data-binding/overview) chapter for more info.

## Page lifecycle

When a user visits the page, DotVVM will translate the DotHTML syntax in plain HTML with Knockout JS `data-bind` expressions.

```DOTHTML
<!-- DotHTML markup -->
<dot:TextBox Text="{value: Number1}" />

<!-- rendered output -->
<input type=text data-bind="value: Number1" />
```

DotVVM also **serializes the viewmodel in JSON** and embeds it in the page, together with scripts required for DotVVM to work. 

Thanks to this, the page will have all the state information it needs without the need to build any API. The HTML and data the page needs will are delivered to the client in one HTTP request.

### User interactions

When the user interacts with controls in the page, all changes are written into the viewmodel representation in the browser (stored in Knockout observable objects).

For example, if the user enters some value in the `TextBox` control, a corresponding property in the viewmodel is updated.

If the user clicks on a button, a method in the viewmodel needs to be called to handle such action. DotVVM takes the viewmodel and posts it to the server (serialized as JSON, using an [AJAX call](https://cs.wikipedia.org/wiki/AJAX)). 

DotVVM runtime on the server creates a viewmodel instance, populates it with the data sent by the client, and invoke the desired method. All changes made to the viewmodel state are serialized and sent back to the browser which applies them to the viewmodel (the controls are updated through Knockout JS data-bindings without the need to reload the page).

> Since sending the entire viewmodel to the server can be inefficient, DotVVM also offers different ways of calling methods on the server and updating parts of the viewmodel. 

See the [Respond to user actions](concepts/respond-to-user-actions/overview) chapter for more info.

## See also

* [Quick start: Create a new project](quick-starts/build/create-new-project)
* [Quick start: Modernize legacy applications using DotVVM](quick-starts/modernize/add-dotvvm-to-existing-app)
* [DotVVM project structure](concepts/project-structure)
* [DotHTML markup](concepts/dothtml-markup/overview)
* [Data-binding](concepts/data-binding/overview)
* [Viewmodels](concepts/viewmodels/overview)
