# Control development overview

Building custom controls is not only for advanced developers and scenarios. We really encourage you to learn how to write your own **DotVVM** controls because it will help you even in very small apps. It will also boost your productivity because you'll be able to reuse significant amount of code across multiple pages or even across multiple projects.

In DotVVM, there are three types of controls - **markup controls**, **code-only controls** and **composite controls**.

> Composite controls were added in DotVVM 4.0 and they provide an easy way of composing new controls from the existing ones.


## Markup controls

[Markup controls](markup-controls) are just a piece of DOTHTML markup which you can put in its own file and use it from multiple places.

For example, if you write a shopping site, you need the user to enter a billing and delivery address. Both of them use the same set of fields like name, number and street, city, state, ZIP code etc. It would be great to create a control called `AddressEditor`, and use it on every place you need to edit the addresses.

Moreover, you can take this ready-made control, and use it in another project because in almost all apps you need the user to give you an address. The control can maintain its own state and have its own internal logic, e.g. guess the city name from the ZIP code. This is commonly done by shipping a control viewmodel together with the control. This viewmodel is then embedded as a child in the viewmodel of the page.

```DOTHTML
@viewmodel object
@property string Text

<dot:RouteLink RouteName="MyRoute" Text={value: _control.Text}
```

## Composite controls

Composite controls are written in C# without any DotHTML markup, but still mainly compose their logic from other components.
The primary entrypoint of a composite control is the GetContents method, which take properties as parameters and returns a new control tree - a hierarchy of HTML elements or other DotVVM components.
The method can do any processing, and is therefore much more flexible than DotHTML, at the cost of giving up the HTML syntax.
Controls written in C# are also easier to distribute as NuGet packages.


```CSHARP
/// Simple CompositeControl example
/// * creates a link to MyRoute with the given text
public class MyLink : CompositeControl
{
    public DotvvmControl GetContents(
        ValueOrBinding<string> text,
    )
    {
        return new RouteLink()
            .SetProperty(r => r.RouteName, "MyRoute")
            .SetProperty(r => r.Text, text);
    }
}
```

See the [Composite controls](composite-controls) chapter for more info. 

> Composite controls are relative new DotVVM feature, and we recommend it as the default way of writing code-only controls.
> However, DotVVM itself and most of our commercial components are not written this way mainly for legacy reasons.


## Code-only controls

[Code-only controls](code-only-controls) are the most flexible type of control, but usually require more knowledge of DotVVM framework to use right.
They are primarily used whenever you need to render a precise piece of HTML and incorporate bindings with it, for example to wrap a Knockout.js binding handler for DotVVM.

If your use case is to write out precise HTML without having to support DotVVM value bindings, it is quite easy.
For instance, you may want to invoke a shared method which outputs HTML, be it a Markdown transpiler, or a legacy T4 HTML template.
See the [Code-only controls](./code-only-controls.md) chapter for details about more advanced uses.


```CSHARP
public class MarkdownLiteral : DotvvmControl
{
    [MarkupOptions(AllowBinding = false)]
    public string Text
    {
        get { return (string)GetValue(TextProperty)!; }
        set { SetValue(TextProperty, value); }
    }
    public static readonly DotvvmProperty TextProperty =
        DotvvmProperty.Register<string, MarkdownLiteral>(nameof(Text), "");

    protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
    {
        var html = MarkdownTranspiler.Transpile(Text);
        writer.WriteUnencodedText(html);
    }
}
```

## Commercial controls

You don't need to write all controls yourself. We have created several packs of commercial controls which can save much time:

* [Bootstrap for DotVVM](https://www.dotvvm.com/products/bootstrap-for-dotvvm)
* [DotVVM Business Pack](https://www.dotvvm.com/products/dotvvm-business-pack).

## DotVVM contrib

If you author some DotVVM controls and think they may be useful to the community, check out our [DotVVM Contrib](https://github.com/riganti/dotvvm-contrib) repository - it contains dozens of community-contributed components which are shipped as separate NuGet packages. We'd be happy for your contributions.

Also, this repo can be used as a learning material or inspiration for creating your own controls - many control development concepts are covered there.

## See also

* [Markup controls](markup-controls)
* [Code-only controls](code-only-controls)
* [Composite controls](composite-controls)
* [Control properties](composite-controls)
* [Adding interactivity using Knockout binding handlers](interactivity)
* [Custom postback handlers](custom-postback-handlers)
* [Binding system extensibility](binding-extensibility)
* [Binding extension parameters](binding-extension-parameters)
* [Custom JavaScript translators](custom-javascript-translators)
* [Testing controls](testing-controls)
