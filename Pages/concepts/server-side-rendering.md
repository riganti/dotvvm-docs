# Server-side rendering

DotVVM uses Knockout JS to create the MVVM experience, which means that evaluation of the [data-binding expressions](~/pages/concepts/data-binding/overview) is done in the browser. This is useful for data which change while the page is loaded, but it is not ideal for "static" data. Most search engines can evaluate JavaScript, but the pages can be penalized for this, so it is a good idea to render the "static" data-binding expressions on the server. 

Some DotVVM controls support __server-side rendering mode__. In such case, the control renders different HTML output, which makes the page indexable even if the search engine cannot evaluate scripts.

## Change rendering mode

The `RenderSettings.Mode` property can be applied on HTML elements and on most of the DotVVM controls. **The setting is inherited to the child elements.**

The default value for this property is `Client`, which means that all value bindings will be translated to the Knockout JS `data-bind` expressions, and thus evaluated on the client.

When you switch the mode to the `Server`, the following samples will be rendered on the server side instead of generating Knockout JS code, 

### Value binding behavior

All value bindings used directly in text will be evaluated on the server and rendered directly in the page.

```DOTHTML
<p RenderSettings.Mode="Server">{{value: Text}}</p>
```

will be rendered as

```DOTHTML
<p>Hello World!</p>
```

> Note that [resource binding](~/pages/concepts/data-binding/resource-binding) has the same effect: `{{resource: Text}}` would produce the same output.

### Literal and HtmlLiteral behavior

The [Literal](~/controls/builtin/Literal) and [HtmlLiteral](~/controls/builtin/HtmlLiteral) controls will be also rendered directly in the HTML:

```DOTHTML
<dot:Literal Text="{value: Text}" RenderSettings.Mode="Server" />
```

will be rendered as

```DOTHTML
Hello World!
```

### Repeater and GridView behavior

* The [Repeater](~/controls/builtin/Repeater) and [GridView](~/controls/builtin/GridView) controls will render each row directly into the HTML output. 

In the default rendering mode they just render a template which is copied on the client-side by the JavaScript code using the Knockout JS `foreach` binding. 

```DOTHTML
<!-- Repeater in client side rendering -->
<tbody data-bind="foreach: Rows">
    <!-- this template is instantiated for each row on the client side -->
    <tr>    
        <td><span data-bind="text: Name"></span></td>
    </tr>
</tbody>
```

```DOTHTML
<!-- Repeater in server side rendering -->
<tbody data-bind="foreach: Rows">
    <tr>
        <td>Row 1</td>
    </tr>
    <tr>
        <td>Row 2</td>
    </tr>
    <tr>
        <td>Row 3</td>
    </tr>
</tbody>
```

## Limitations

The principles mentioned above indicate that some combinations of client-side and server-side rendering won't work properly.

For example, if you use the client rendering on a `Repeater` control, and then use server rendering inside its `ItemTemplate`, it won't work properly because the template with hard-coded value would be copied and the bindings won't work properly.

In the typical app scenarios, you need to set the server rendering on `Repeater` or `GridView` controls, or on the page-level (if the page is static).

```DOTHTML
<dot:Content ContentPlaceHolderID="MainContent" RenderSettings.Mode="Server">
    
</dot:Content>
```

Remember that the goal of server-side rendering is not to create an application that works without the JavaScript. The JavaScript part is still there and most of the things (like buttons and postbacks) require JavaScript to be enabled in the client's browser.

The server rendering only renders text content, `Literal` controls, and controls that work with collections on the server. The rest of the functionality (including e.g. the `Visible` property) is still done using JavaScript and Knockout JS bindings.

## Re-render control HTML on postbacks

When you render something directly in the HTML without the Knockout JS binding, the value won't be synchronized with the changes made to the viewmodel property any more. If the `Repeater` rendered its items in the server-rendering mode, and you add another row to the collection, the new item won't appear.

Sometimes, you may need to regenerate and replace the HTML during the postback. There is the `PostBack.Update` property which can help with this.

```DOTHTML
<div PostBack.Update="true">
...
</div>
```

If the `PostBack.Update` is used, the control will be re-rendered on every postback, and the HTML will be sent as part of the response. 

Typically, you use this property in combination with the server-side rendering.

> Currently, there is no way to set `PostBack.Update` property on the control dynamically - thus, the control will be re-rendered on every postback.

## See also

* [Data-binding overview](~/pages/concepts/data-binding/overview)
* [Resource binding](~/pages/concepts/data-binding/resource-binding)
* [Literal](~/controls/builtin/Literal)
* [HtmlLiteral](~/controls/builtin/HtmlLiteral)
* [Repeater](~/controls/builtin/Repeater)
* [GridView](~/controls/builtin/GridView)
