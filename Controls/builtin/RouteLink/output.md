In the [Client rendering mode](~/pages/concepts/server-side-rendering), the link URL is built on the client.

```DOTHTML
<a href="#" data-bind="attr: { 'href': ... }, text: ..."></a>
```


In the [Server rendering mode](~/pages/concepts/server-side-rendering), the URL is built on the server.

```DOTHTML
<a href="...">Text or Content</a>
```