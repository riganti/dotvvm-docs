# Value binding

The **value binding** is the most frequently used binding in DotVVM. It allows you to bind a property in the viewmodel to a property of a control in the DotHTML file, or just render the value as a text. 

The data-binding works in both directions - whenever the `Url` property changes, the control that this property is bound to, will be updated accordingly. Also, if the user makes a change in the control, the property value will be written back to the viewmodel.

## Example

Let's have the following viewmodel:

```CSHARP
public class MyViewModel {
    ...
    public string Url { get; set; }
    ...
}
```

In the DotHTML markup, you can bind the property to the `Text` property of a [TextBox](~/controls/builtin/TextBox) control:

```DOTHTML
<dot:TextBox Text="{value: Url}" />
```

If you run the page and view the page source code, you'll see that DotVVM translated the binding into a [Knockout](https://knockoutjs.com/) `data-bind` attribute. DotVVM uses this popular JavaScript library to perform the data-binding and provide the MVVM experience. This is how the HTML will look like in the browser:

```DOTHTML
<input type="text" data-bind="attr: { 'href': Url }" />
```

## Supported expressions in value binding

You can use more complex expressions in **value bindings** - the only requirement is that they need to be translatable to JavaScript. 

See the [supported expressions](supported-expressions) page for a list of methods and APIs that are supported. 

If you need to use a method that DotVVM cannot translate, you can define your own [JavaScript translator](~/pages/concepts/client-side-development/custom-javascript-translators).

## Use server values in value bindings

Since DotVVM 5.0, value bindings can include server-evaluated fragments using `_page.Resource(...)`.

The resource fragment is evaluated during initial page render.
It is not reactive and is not re-evaluated by commands unless the containing markup is re-rendered, for example by using [PostBack.Update](~/pages/concepts/server-side-rendering#re-render-control-html-on-postbacks).
The resource fragment can be used to access properties not sent to the client, call methods which DotVVM cannot translate to JavaScript or access localization resources.

```DOTHTML
<dot:Literal Text={value: 'Hello ' + _page.Resource(UserDisplayName) + ', ' + CurrentName} />

<dot:Button Text="Open"
            Click={staticCommand: OpenedId = _page.Resource(System.Guid.NewGuid())} />
```

Static members and .NET resource properties used inside value bindings are wrapped as server resources automatically.
This makes RESX values usable in expressions that otherwise run on the client.

## See also

* [Data-binding overview](~/pages/concepts/data-binding/overview)
* [Resource binding](~/pages/concepts/data-binding/resource-binding)
* [Binding context](~/pages/concepts/data-binding/binding-context)
* [Respond to user actions](~/pages/concepts/respond-to-user-actions/overview)
* [Video: Server rendering - value vs resource binding](https://www.youtube.com/watch?v=FLIcYBaja-I&ab_channel=DotVVM)
