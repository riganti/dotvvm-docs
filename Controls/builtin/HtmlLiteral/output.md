By default, the control renders a `<div>` element which will contain the specified HTML. 
You can change it to any other tag name using the `WrapperTagName` property.

```DOTHTML
<div data-bind="html: expression"></div>
```

If you set the `RenderWrapperTag` to *false*, only the pure HTML content will be rendered. However, removing the wrapper tag
is supported only in the [Server rendering mode](/docs/tutorials/basics-server-side-html-generation).
