# Server-Side HTML Generation and SEO

DotVVM uses Knockout JS to create the MVVM experience, but that doesn't have to make your site SEO-unfriendly. The important DotVVM controls support server-side rendering which can make the data in the page indexable even if the search engine doesn't evaluate scripts.

## Server-Side Rendering

You can use the `RenderSettings.Mode` property on HTML elements and most of the DotVVM controls.

The default value for this property is `Client`, which means that all value bindings are translated to the Knockout JS `data-bind` expressions and thus evaluated on the client.

When you switch the mode to the `Server`, the following samples will be rendered on the server side instead of generating Knockout JS code:

* All value bindings used directly in text will be evaluated on the server and rendered directly in the page.

```DOTHTML
<p RenderSettings.Mode="Server">{{value: Text}}</p>
```
will be rendered to
```DOTHTML
<p>Hello World!</p>
```

> Note that [Resource Binding](/docs/tutorials/basics-resource-binding/{branch}) has the same effect: `{{resource: Text}}` would produce the same output.

* [Literal](/docs/controls/builtin/Literal/{branch}) and [HtmlLiteral](/docs/controls/builtin/HtmlLiteral/{branch}) bindings will be also rendered directly in the HTML:

```DOTHTML
<dot:Literal Text="{value: Text}" RenderSettings.Mode="Server" />
```
will be rendered to
```DOTHTML
Hello World!
```

> Note that [Resource Binding](/docs/tutorials/basics-resource-binding/{branch}) has the same effect: `{{resource: Text}}` would produce the same output.

* The [Repeater](/docs/controls/builtin/Repeater/{branch}) and [GridView](/docs/controls/builtin/GridView/{branch}) controls will render each row directly into the HTML output. In the default rendering mode they just render a template which is copied on the client-side by the JavaScript code using the Knockout JS `foreach` binding. 

```DOTHTML
<!-- Repeater in client side rendering -->
<tbody data-bind="foreach: Rows">
    <!-- this template is copied for each row on the client side -->
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

The `RenderSettings.Mode` property is inherited to the child elements.

## Restrictions

The principles mentioned above indicate that some combinations of client-side and server-side rendering won't work properly.

For example, if you use client rendering on a `Repeater` control, and then use server rendering inside its `ItemTemplate`, it won't work properly because the template with hard-coded value would be copied and the bindings won't work properly.

In the typical app scenarios you need to set the server rendering on `Repeater` or `GridView` controls, or on the page-level.

```DOTHTML
<dot:Content ContentPlaceHolderID="MainContent" RenderSettings.Mode="Server">
    
</dot:Content>
```

Remember that the goal of server-side rendering is not to create an application that works without the JavaScript. The JavaScript part is still there and most of the things (like buttons and postbacks) require JavaScript to be enabled in the client's browser.

The server rendering only expands text content, `Literal`s and controls that render collections. The rest of the functionality (including e.g. the `Visible` property) is still done using JavaScript and Knockout JS bindings.

## PostBack.Update property

When you render something directly in the HTML, the value won't react to the changes made to the viewmodel property any more.

Sometimes you may need to regenerate and replace the HTML during the postback. That's why we have the `PostBack.Update` property in DotVVM.

```DOTHTML
<div PostBack.Update="true">
...
</div>
```

If the `PostBack.Update` is used, the control is rendered on every postback and the HTML is sent as part of the response.

The content is then replaced in the page. 

Typically, you use this property in combination with the server-side rendering.
