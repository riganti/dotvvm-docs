# Code-only Controls

This kind of controls is useful when you need to render a piece of HTML and/or you need to support data-binding and manipulate the viewmodel.

Building code-only controls is more difficult, but they can do much more. All built-in DotVVM controls are implemented as code-only controls. 

If you want to learn about how to write controls in DotVVM, we encourage you to look in the [GitHub](https://github.com/riganti/dotvvm) repository to see how the built-in controls are implemented.

## Control Registration

First, you need to register the code-only control in the `DotvvmStartup.cs` file. 

```CSHARP
config.Markup.AddCodeControls("cc", typeof(DotvvmDemo.Controls.Control));
```

Using this code snippet, if you use the `<cc:` tag prefix, DotVVM will search for the control in the specified namespace and assembly.

> If you register a markup control with code behind like this, it won't work. If the control has a markup, it must be registered using the `AddMarkupControl` method.

## Basics

All controls in `DotVVM` derive from the `DotvvmControl` class. This base class provides only 
basic functionality and _it is not a good base class to inherit directly_ for most purposes. 

The most useful class to be derived from is `HtmlGenericControl`. It is prepared to render one HTML element (which can contain child elements of course). Most built-in controls in **DotVVM** derive from the `HtmlGenericControl` class. 

## Creating Code-only Control

The best example to learn how to write controls in **DotVVM** is to look how the built-in controls are implemented.
Let's begin with the [TextBox](/docs/controls/builtin/TextBox/{branch}).

The textbox in HTML (with Knockout JS binding) looks like this:

```DOTHTML
<input type="text" data-bind="value: FirstName" />
```

This is what we'd like to render when we see `<dot:TextBox Text="{value: FirstName"}" />`. 

So let's create a class that derives from `HtmlGenericControl`. In the constructor, we call the base constructor and tell the name of the HTML element - in our case we want `input`.

```CSHARP
public class TextBox : HtmlGenericControl
{
    public TextBox() : base("input")
    {

    }
}
```

This would render just `<input></input>` in the page. Also, if you add any custom attributes (e.g. `style`, `class`) on the `TextBox` control, it would append them in the page. The `HtmlGenericControl` takes care about all this stuff for you.

## Rendering Pipeline

Now, the `HtmlGenericControl` has 4 methods which we can override to modify the rendered HTML. They are called in this order:

+ `AddAttributesToRender` - by default, this method takes all HTML attributes set to the control, and prepares them to be rendered.

+ `RenderBeginTag` - by default, this method renders the begin tag.

+ `RenderContents` - by default, this method renders the child controls.

+ `RenderEndTag` - by default, this method renders the end tag.

## HtmlWriter

Now, let's see how to render the HTML element. In **DotVVM**, we use the `HtmlWriter` to generate HTML. To render the `<input type="text" />` we need to call something like this:

```CSHARP
    writer.AddAttribute("type", "text");
    writer.RenderSelfClosingTag("input");
```

There are also methods `RenderBeginTag("input")`, `RenderEndTag()`, `WriteText("some text")` or `WriteUnencodedText("some HTML")`.

The `AddAttribute` method is called before rendering the tag and it also has a third argument called `append`. If you call `AddAttribute("class", "blue")` and then `AddAttribute("class", "red", true)`, the class will be appended. 

The `HtmlWriter` knows that values in the `class` HTML attribute are separated by spaces, values in the `style` attribute by semicolons etc. You can also specify your own separator character as the fourth argument.

## Rendering HTML

Let's continue with the **TextBox** class. We don't want to render begin and end tags `<input></input>`, but the self closing one `<input />`. 

It doesn't make sense to allow the `TextBox` to have any content inside. We can decorate the class with the `[ControlMarkupOptions(AllowContent = false)]` attribute to tell DotVVM that there should be no content inside the `<dot:TextBox>` and `</dot:TextBox>` tags. If the user places anything there, DotVVM will display an error page.  

We can override the `RenderBeginTag` method to render the self-closing tag, and the `RenderEndTag` to render nothing. 

Between these two methods, the rendering pipeline calls also the `RenderContents` method which renders the contents between the `<dot:TextBox>` and `</dot:TextBox>` tags, but we won't have anything here thanks to the `ControlMarkupOptions` attribute.

```CSHARP
protected override void RenderBeginTag(IHtmlWriter writer, IDotvvmRequestContext context)
{   
    // TagName contains the value passed to the base constructor. 
    // We don't want to call base.RenderBeginTag here because it would render the begin tag and then the closing tag.
    // We want the self closing tag. 
    
    writer.RenderSelfClosingTag(TagName); 
}

protected override void RenderEndTag(IHtmlWriter writer, IDotvvmRequestContext context)
{    
    // do nothing, we have already rendered the self-closing tag
}
```

## Rendering HTML attributes

The most interesting is the `AddAttributesToRender` method. 

The default implementation takes all HTML attributes that are not mapped to DotVVM properties, and add them to the `HtmlWriter`. So, if the user uses the following snippet, the default implementation of `AddAttributesToRender` will add the `class`, `style` and `placeholder` attributes 
to the `HtmlWriter`. 

> Remember that `HtmlWriter` requires to add all attributes before we call `RenderBeginTag`. After you render a tag, you cannot go back and add any attributes to it.

The custom attributes even support data-bindings, so you don't have to care about this. You just need to take care of the control properties.

```DOTHTML
<dot:TextBox Text="{value: FirstName}" style="border: none" class="txb1" placeholder="Enter first name" />
```

We need to declare the `Text` property first:

```CSHARP
public string Text
{
    get { return Convert.ToString(GetValue(TextProperty)); }
    set { SetValue(TextProperty, value); }
}
public static readonly DotvvmProperty TextProperty =
    DotvvmProperty.Register<string, TextBox>(t => t.Text, "");
```

However, we should support two scenarios:

```DOTHTML
<dot:TextBox Text="{value: FirstName}" />
<dot:TextBox Text="Test" />
```

In the first case, we need to render `data-bind="value: FirstName"`, in the second case we need to render `value="Test"`.

We can solve this like this:

```CSHARP
protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
{
    var textBinding = GetValueBinding(TextProperty);
    if (textBinding != null) 
    {
        // the property contains binding - this will render data-bind="value: expression"
        writer.AddKnockoutDataBind("value", this, TextProperty);
    }
    else 
    {
        // render the value in the HTML
        writer.AddAttribute("value", Text);
    }

    writer.AddAttribute("type", "text");
    
    base.AddAttributesToRender(writer, context);
}
```

Because this pattern is quite usual and in most controls you would have written the `if` statement checking the presence of binding and rendering the appropriate output, there is an overload of the `AddKnockoutDataBind` method with four arguments.

It allows you to specify a function which is called when the specified property doesn't contain a binding.

So we could simplify the function above like this:

```CSHARP
protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
{
    writer.AddKnockoutDataBind("value", this, TextProperty, () => {
        writer.AddAttribute("value", Text);
    });

    writer.AddAttribute("type", "text");
    
    base.AddAttributesToRender(writer, context);
}
```

Thanks to this, the syntax is much shorter.

## Building Control Tree

Rendering HTML using the `HtmlWriter` class is good for simple controls. If the control is more complicated or can contain controls which invoke postbacks, you need to build a control tree inside the control.

This is especially handy if you need to compose a complex control of already existing ones.

This approach often results in a cleaner code, but rendering the HTML using the `HtmlWriter` is much faster than creating a control for the `<div>` element using `new HtmlGenericControl("div")`.

You need to decide if rendering raw HTML is OK for your case, or if the control is more complex and you need to build a tree of child controls and manipulate them somehow.  

## Composite Control

Let's create a control that is composed of two existing controls (`TextBox` and `Literal`) placed inside a `div` element.

We will create a new class which derives from `HtmlGenericControl` and renders a `div`:

```CSHARP
public class TextBoxWithLabel : HtmlGenericControl
{
    public TextBoxWithLabel() : base("div")
    {
    }
}
```

Next, let's add the `Text` and `LabelText` properties.

Both of them are required. We can indicate this by using the `MarkupOptions` attribute. The attribute can also specify whether the property can contain a data-binding or a hard-coded value or both. By default, it can contain both of them.

```CSHARP
[MarkupOptions(AllowHardCodedValue = false)]
public string Text
{
    get { return (string)GetValue(TextProperty); }
    set { SetValue(TextProperty, value); }
}
public static readonly DotvvmProperty TextProperty
    = DotvvmProperty.Register<string, TextBoxWithLabel>(c => c.Text, null);

[MarkupOptions(AllowBinding = false)]
public string LabelText
{
    get { return (string)GetValue(LabelTextProperty); }
    set { SetValue(LabelTextProperty, value); }
}
public static readonly DotvvmProperty LabelTextProperty
    = DotvvmProperty.Register<string, TextBoxWithLabel>(c => c.LabelText, null);
```

## Child Controls

Similarly to the viewmodels, every control has lifecycle events `OnInit`, `OnLoad` and `OnPreRender` which follow the logic of the viewmodel `Init`, `Load` and `PreRender` events.

A basic rule is to create the controls as soon as possible. If you don't need data from the viewmodel (which are deserialized after the `Init` event), build the child controls in the `OnInit` phase. If you rely on values entered by the user, build the controls in the `OnLoad` phase. 

```CSHARP
protected override void OnInit(IDotvvmRequestContext context)
{
    var textBox = new TextBox();

    // copy the binding from the control's Text property to the TextBox.Text property
    textBox.SetBinding(TextBox.TextProperty, GetValueBinding(TextProperty));

    // we can set LabelText now, it cannot contain binding
    var label = new Literal(LabelText);

    // the controls are always the same, they don't depend on the viewmodel data, so we can use the OnInit event
    Children.Add(label);
    Children.Add(textBox);

    base.OnInit(context);
}
```

After the `Load` phase, the commands are executed and the control tree must be complete at that moment. Moreover, the control tree must be equal as it was in the previous postback, otherwise DotVVM won't be able to find the control which triggered the postback. DotVVM validates postback information and if the control doesn't exist in the page, an error page shows up and the postback is not processed.
