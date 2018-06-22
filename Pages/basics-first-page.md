# Creating the First Page

Every page in **DotVVM** consists of a **view** and a **viewmodel**. 

To create a page, just right-click the desired folder in the __Solution Explorer__ window and choose **Add / New Item...**.
In the dialog, navigate to the __Web / DotVVM__ section and choose the **DotVVM Page** template.

<p><img src="{imageDir}basics-first-page-img1.png" alt="Adding a new page" /></p>

After you enter the name of the page and confirm the selection, another window will appear. In this window, you can specify whether you want 
to create a viewmodel or not, and where it should be placed.

<p><img src="{imageDir}basics-first-page-img2.png" alt="Creating the viewmodel" /></p>

After you proceed, the view and the viewmodel are added to the project. If you place your view in the **Views** folder in your project,
the window will automatically place your viewmodel in the **ViewModels** folder.

You don't have to follow this convention however. You can always create your own conventions, e.g. to have the views together with viewmodels in the same folder.


## View

The **view** is a file with `.dothtml` extension. The views in DotVVM use standard HTML syntax with three flavors:

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

