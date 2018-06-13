# Introduction

As a .NET developer, you have several ways to build a web app. The official way is to use **ASP.NET MVC**, 
or **ASP.NET Web API** in combination with some client-side framework, like _React_, _Angular_ or _Knockout_.  

But when you start building big enterprise application, you soon discover that there are not many ready-made solutions for that, and each one is different.

Moreover, you have to learn many new things and build parts of your own infrastructure to make the development convenient for large scale enterprise apps.

**DotVVM** is a framework which handles client-server communication, validation, SPAs, date-time formatting, and which provides plenty of ready-made controls that you can use immediately. 

Also, there is no need to learn many javascript libraries, frameworks and tools. You can do most of the stuff with
**C#, HTML and CSS only**. 

**DotVVM** is a framework that helps you write modern websites in the MVVM fashion.

**dot** represents **.NET Framework** and **VVM** stands for **MVVM - Model View ViewModel pattern**.



## How Does It Work?

First, you have to define the **view**. We use HTML syntax, but there are some flavors added. That's why we call it _DOTHTML_.

Notice the `{value: Property}` and `{command: Method}` __data-bindings__ and the custom `dot:Something` elements - these are called _DotVVM controls_.

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

The second step is to write the **viewmodel**. The viewmodel is a plain C# class. It doesn't need any `INotifyPropertyChanged`, you can declare just plain C# properties.

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

And that's it! **DotVVM** translates the `<dot:TextBox>` control into `<input type="text">` and `<dot:Button>` into `<input type="button">`.

The `{value: Number1}` binding is translated to Knockout JS expressions `<input data-bind="value: Number1">`.

The `{command: Function()}` is translated to a JavaScript call of a `dotvvm.postBack` function. This will call the  `Calculate` function on the server, and all changes to the viewmodel will be applied to the page. 

All the JavaScript work is done by **DotVVM** already, so you can **concentrate on the business logic** and use a convenient data-binding syntax to manipulate with the page DOM.
