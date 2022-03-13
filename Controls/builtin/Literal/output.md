In the [Client rendering mode](~/pages/concepts/server-side-rendering), the control renders a span with Knockout `data-bind` expression:

```DOTHTML
<span data-bind="text: expression"></span>
```

If you set the `RenderSpanElement` to *false*, the span won't be rendered. In this mode you cannot apply HTML attributes to the control and use some properties (e.g. `Visible`, `ID`).

```DOTHTML
<!-- ko text: expression --><!-- /ko -->
```



In the [Server rendering mode](~/pages/concepts/server-side-rendering), the text is rendered directly to the response. 
This helps the search engines to understand the page content.

```DOTHTML
<span data-bind="text: expression">Text</span>
```

If you turn the span element off, only the raw text will be rendered.

> From DotVVM 4.0, the Knockout data-bind expression is rendered even in the server-rendering mode so the text can reflect further updates of the property.

If you want the render just the text without the Knockout data-binding expression, use the [resource binding](~/pages/concepts/data-binding/resource-binding). This will behave the same way as using hard-coded value in the markup.