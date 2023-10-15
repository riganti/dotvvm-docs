# Code-only controls

This kind of controls is useful when you need to render more complex or dynamic HTML, or when you need to support complex data-binding scenarios, or manipulate the viewmodel on the client-side.

Building code-only controls is more difficult, but they can do much more. All built-in DotVVM controls are implemented as code-only controls. 

If you want to learn about how to write controls in DotVVM, we encourage you to look in the [DotVVM GitHub repository](https://github.com/riganti/dotvvm/tree/main/src/Framework/Framework/Controls), on in the [DotVVM Contrib repo](https://github.com/riganti/dotvvm-contrib) to see how the built-in controls are implemented.

## Fundamentals

In general, all controls in `DotVVM` inherit from the `DotvvmControl` class. This base class provides only basic functionality and _it is not a good base class to inherit directly_ for most purposes. 

**The most useful class to be derived from is `HtmlGenericControl`** (which inherits from `DotvvmControl`). It is prepared to render one HTML element, and can contain child elements or controls. Most built-in controls in DotVVM derive from the `HtmlGenericControl` class. 

## Register controls

First, you need to register the code-only control in the `DotvvmStartup.cs` file. 

```CSHARP
config.Markup.AddCodeControls("cc", typeof(DotvvmDemo.Controls.Control));
```

Using this code snippet, if you use the `<cc:` tag prefix, DotVVM will search for the control in the specified namespace and assembly.

> If you register a markup control with code behind like this, it won't work correctly. If the control has a markup, it must be registered using the `AddMarkupControl` method. See the [Markup control registration](markup-control-registration) chapter for more details.

## Create code-only control

The best example to learn how to write controls in DotVVM is to look how the built-in controls are implemented. Some of them are pretty complicated, but there are some which are simple. 

For the purpose of this chapter, let's begin with implementing a control similar to the [TextBox](~/controls/builtin/TextBox) control. The real `TextBox` control included in the framework is much more complex as it supports formatting values or different kinds of `input` element, but let's focus only on the main functionality - providing the way to edit a string value in the viewmodel.

In order to use the control in the browser, we need it to produce the following markup:

```DOTHTML
<input type="text" data-bind="value: FirstName" />
```

This is what we'd like to render when we see `<cc:SimpleTextBox Text="{value: FirstName"}" />`. 

So let's create a class that derives from `HtmlGenericControl`. In the constructor, we call the base constructor and tell the name of the HTML element we want the control to render: `input`.

```CSHARP
public class SimpleTextBox : HtmlGenericControl
{
    public SimpleTextBox() : base("input")
    {
    }
}
```

This would render just `<input></input>` in the page. Also, if you add any custom attributes (e.g. `style`, `class`) on the `TextBox` control, it would append them in the page. The `HtmlGenericControl` takes care about all this stuff for you.

## HTML rendering pipeline

Now, the `HtmlGenericControl` has 4 methods which we can override to modify the rendered HTML. They are called in this order:

+ `AddAttributesToRender` - by default, this method takes all HTML attributes set to the control, and prepares them to be rendered.

+ `RenderBeginTag` - by default, this method renders the begin tag.

+ `RenderContents` - by default, this method renders the child controls.

+ `RenderEndTag` - by default, this method renders the end tag.

## HtmlWriter

Now, let's see how to render the HTML element. In DotVVM, we use the `HtmlWriter` object to generate HTML. To render the `<input type="text" />` we need to call something like this:

```CSHARP
    writer.AddAttribute("type", "text");
    writer.RenderSelfClosingTag("input");
```

Notice that the flow is this: first add the attributes we want to render, and then render the tag. The `HtmlWriter` stores the attributes in a temporary buffer, and emits them when you decide what tag you want to render.

There are also methods `RenderBeginTag("input")`, `RenderEndTag()`, `WriteText("some text")` or `WriteUnencodedText("some HTML")`.

The `AddAttribute` method is called before rendering the tag and it also has a third argument called `append`. If you call `AddAttribute("class", "blue")` and then `AddAttribute("class", "red", append: true)`, the classes will be appended. The `HtmlWriter` knows that values in the `class` HTML attribute are separated by spaces, values in the `style` attribute by semicolons etc. You can also specify your own separator character as the fourth argument.

## Render HTML

Let's continue with our `SimpleTextBox` class. We don't want to render begin and end tags `<input></input>`, but just the self closing one `<input />`. 

Also, it doesn't make sense to allow the `TextBox` to have any content inside. We can decorate the class with the `[ControlMarkupOptions(AllowContent = false)]` attribute to tell DotVVM that there should be no content inside the `<dot:TextBox>` and `</dot:TextBox>` tags. If the user places anything there, DotVVM will display an error page.  

We can override the `RenderBeginTag` method to render the self-closing tag, and the `RenderEndTag` to render nothing. 

Between these two methods, the rendering pipeline calls also the `RenderContents` method which renders the contents between the `<dot:TextBox>` and `</dot:TextBox>` tags, but we won't have anything here thanks to the `ControlMarkupOptions` attribute.

```CSHARP
[ControlMarkupOptions(AllowContent = false)]
public class SimpleTextBox : HtmlGenericControl
{
    public SimpleTextBox() : base("input")
    {
    }

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
}
```

### HTML attributes

The most interesting is the `AddAttributesToRender` method. 

The default implementation takes all HTML attributes that are not mapped to DotVVM properties, and add them to the `HtmlWriter`. So, if the user uses the following snippet, the default implementation of `AddAttributesToRender` will add the `class`, `style` and `placeholder` attributes 
to the `HtmlWriter`. 

> Remember that `HtmlWriter` requires to add all attributes before we call `RenderBeginTag`. After you render a tag, you cannot go back and add any attributes to it.

The custom attributes even support data-bindings, so you don't have to care about this. You just need to take care of the control properties.

```DOTHTML
<cc:SimpleTextBox Text="{value: FirstName}" style="border: none" class="txb1" placeholder="Enter first name" />
```

We need to declare the `Text` property first:

```CSHARP
public string Text
{
    get { return Convert.ToString(GetValue(TextProperty)); }
    set { SetValue(TextProperty, value); }
}
public static readonly DotvvmProperty TextProperty =
    DotvvmProperty.Register<string, SimpleTextBox>(t => t.Text, "");
```

However, we should support two scenarios:

```DOTHTML
<cc:SimpleTextBox Text="{value: FirstName}" />
<cc:SimpleTextBox Text="Test" />
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

Because this pattern is quite usual, and in most controls you would have written the `if` statement checking the presence of binding and rendering the appropriate output, there is an overload of the `AddKnockoutDataBind` method with four arguments.

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

## Build the inner control tree

Rendering HTML using the `HtmlWriter` class is good for simple controls. If the control is more complicated or can contain controls which invoke postbacks, you need to build a control tree inside the control.

This is especially handy if you need to compose a complex control of already existing ones.

This approach often results in a cleaner code, but rendering the HTML using the `HtmlWriter` is much faster than creating a control for the `<div>` element using `new HtmlGenericControl("div")`.

You need to decide if rendering raw HTML is OK for your case, or if the control is more complex and you need to build a tree of child controls and manipulate them somehow.  

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

See the [Control properties](control-properties#specify-markup-options) chapter for more info about the `MarkupOptions` attribute.

> [Composite Controls](./composite-controls.md) offer a simpler API for controls which do not have custom rendering and rely solely on building the inner tree.

### Child controls

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

### Using Markup controls in code-only controls

When you try to instantiate a markup control in your code-only control, you'll se it doesn't work correctly - only the control's code-behind class will be instantiated, but the content specified in the markup is not shown.

```CSHARP
// WRONG - do not instantiate markup controls like this
var child = new MyMarkupControl();
this.Children.Add(child);
```

From DotVVM 4.0, there is a special control which can help with rendering markup controls in your code-only controls - `MarkupControlContainer`.

```CSHARP
var child = new MarkupControlContainer("cc:MyControl", c => {
    c.SetValue(MyControl.NameProperty, someValue); 
    ...
});
this.Children.Add(child);
```

If you know the `@baseType` of the markup control, you may use the generic version to simplify the initialization:

```CSHARP
var child = new MarkupControlContainer<MyControl>("cc:MyControl", c => {
    c.Name = someValue;
    ...
});
this.Children.Add(child);
```

* The initialized markup control is stored in the `CreatedControl` property. It is initialized in OnInit, after the MarkupControlContainer is placed into the Children collection.
* Instead of the tagname, it is also allowed to specify the path to the dotcontrol file:
    - If the markup file is the project: `new MarkupControlContainer("Control/MyControl.dotcontrol")`
    - if the markup is in [an embedded resource](./markup-control-registration.md#embed-markup-control-in-a-class-library): `new MarkupControlContainer("embedded://Assembly.Name/Path.To.File.dotcontrol")`
* If the control has [markup declared properties](./markup-controls.md#declare-the-property-using-the-property-directive), use the `c.SetProperty("MyProperty", value)` method overload to access it.

## See also

* [Markup controls](markup-controls)
* [Markup control registration](markup-control-registration)
* [Markup controls with code](markup-controls-with-code)
* [Control properties](control-properties)
* [Adding interactivity using Knockout binding handlers](interactivity)
