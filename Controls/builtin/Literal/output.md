In the [Client rendering mode](/docs/tutorials/basics-server-side-html-generation/{branch}), the control renders a span with Knockout data-bind 

```DOTHTML
<span data-bind="text: expression"></span>
```

If you set the `RenderSpanElement` to *false*, the span won't be rendered. In this mode you cannot apply HTML attributes to the control and use some properties (e.g. Visible, ID).

```DOTHTML
<!-- ko text: expression --><!-- /ko -->
```



In the [Server rendering mode](/docs/tutorials/basics-server-side-html-generation/{branch}), the text is rendered directly to the response.
This helps the search engines to understand the page content.

```DOTHTML
<span>Text</span>
```

If you turn the span element off, only the raw text will be rendered.