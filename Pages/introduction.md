## Introduction

As a .NET developer, you have several options on how to build a web application in ASP.NET. Every option has its advantages and is suitable for different kind of projects. 

The oldest way is to use **ASP.NET Web Forms**, a technology that enabled developers to quickly build desktop-like experiences for web. Web Forms was a good choice for line of business or intranet web applications recently, but today it is not recommended for new projects. Web Forms is not supported in ASP.NET Core and there are not many new features coming to it in the future.

The most preferred way today is to use **ASP.NET MVC** or **ASP.NET MVC Core**. These technologies are great for public facing sites or for smaller admin applications. However, to build more complex user interface with large, complicated forms, wizards, modal dialogs and data grids can be challenging in comparison with other methods.

Another way of building a web app is to build a REST API using **ASP.NET Web API** or **ASP.NET MVC Core** and then create the user interface with some client-side framework -  _React_, _Angular_ or _Knockout JS_ for example. This approach allows to create highly interactive user interface, but it requires a good knowledge of C# as well as JavaScript. 

**DotVVM** simplifies building of web applications with interactive and complicated user interface. DotVVM provides a rich set of tools and features required to build line of business or intranet sites. 

DotVVM provides a MVVM rendering engine with dozens of ready-made components, and handles the communication between server and client; there is no need to build your own REST API.

**Dot** represents **.NET Framework** and **VVM** stands for **MVVM - Model View ViewModel pattern**.

DotVVM can handle validation and supports SPAs (single page applications), page localization, date and number formatting and much more. 

DotVVM is easy to learn and doesn't require any JavaScript skills at the beginning - knowing **C#, HTML and CSS** is enough to start. 

The **Dot** represents **.NET Framework** and **VVM** stands for the design pattern that is used: **MVVM - Model View ViewModel**.

DotVVM is built on top of ASP.NET platform. It is an alternative to ASP.NET Web Forms or ASP.NET MVC. It supports the new **ASP.NET Core platform** as well as the **original ASP.NET stack** present in .NET Framework.

DotVVM can be used to build new web applications, or to modernize legacy ASP.NET Web Forms sites. It can be used together with other ASP.NET-based technology in the same application with single sign-on and consistent look and feel, so the Web Forms application can be rewritten in DotVVM page by page and possibly ported to .NET Core in the future.

### Examples

The ideal projects for DotVVM are various admin dashboards, intranet portals, CRM or ERP web applications. These applicatios typically contain data grids, forms with many field, complex validation and business logic, pages with wizards or modal dialogs. 

DotVVM can also help in e-commerce sites, especially in the back-office part. Since DotVVM can be used together with other ASP.NET frameworks in the same application, the public facing part of the website may be implemented in ASP.NET MVC while the admin portal is made in DotVVM. 

Of course, it is possible to make public facing applications in DotVVM, but it might not be as helpful as in the previous types of applications. If you are building a company website with several static pages and one form, it will be easy to do in any web technology or framework and DotVVM cannot make it easier.

If you are not sure whether DotVVM is a good choice for your project, just ask us on our  [Gitter chat](https://gitter.im/riganti/dotvvm). We will be happy to help you.


### How DotVVM Works

DotVVM leverages the MVVM pattern. Every DotVVM page consists of two files.

First, you have to define the **view**. DotVVM views use normal HTML syntax, but there are several enhancements. That's why we call it _DOTHTML_.

Notice the `{value: Property}` and `{command: Method}` __data-binding expression__ and the custom `dot:Something` elements - the __DotVVM controls__.

```DOTHTML
@viewModel DotvvmDemo.CalculatorViewModel, Dotvvm
    
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

Next, you need to write the **viewmodel**. In DotVVM, the viewmodel is a plain C# class. In comparison to other MVVM frameworks, there is no need to implement `INotifyPropertyChanged` in DotVVM. You can declare just normal C# properties with default getters and setters.

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

**DotVVM** translates `<dot:TextBox>` control into `<input type="text">` and `<dot:Button>` into `<input type="button">`.

The `{value: Number1}` binding is translated to Knockout JS expressions `<input data-bind="value: Number1">`. Also, DotVVM will translate the viewmodel into JavaScript and include it in the page HTML.

The `{command: Function()}` is translated to a JavaScript call of a `dotvvm.postBack` function which is defined in DotVVM client-side library. This function calls the  `Calculate` method in the viewmodel, and applies all changes made to the viewmodel to the page. The page is not reloaded completely like in ASP.NET Web Forms. DotVVM uses AJAX and Knockout JS to update only the parts of the page which needs to be changed. 

The viewmodel lives on the client side for as long as the page is loaded. When the page needs to call a method on the server (for example to get some data from the database), the viewmodel is serialized and sent to the server where the method can be invoked. The changes are then sent back to the client. The server doesn't have to store the viewmodel instance in session or in memory. The viewmodel instance exists on the server only for a lifetime of the particular HTTP request, which allows the application to be hosted on a web farm. 

Notice that we didn't need to write a single line of JavaScript code. DotVVM is easy to use for all .NET developer, even those who don't have much experience with web development. 

> Sending the entire viewmodel to the server and back may be inefficient in many real-world scenarios. DotVVM offers different ways of calling methods on the server which don't require presence of the viewmodel. See [Optimizing PostBacks](/docs/tutorials/basics-optimizing-postbacks/{branch}) page for more info.