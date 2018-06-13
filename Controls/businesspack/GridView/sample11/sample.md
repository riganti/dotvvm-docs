## Sample 11: Server Rendering

The control supports also the server-side rendering. In this mode, many functions cannot work since the control is rendered directly in the HTML output.

The server rendering can be turned on using the `RenderSettings.Mode` property. 

This helps especially the search engines to index the contents of the page. Additionally, the server rendering might make the grid load faster in the browser if there are hundreds of rows or more.

You can read more in the [Server-Side HTML Generation and SEO](/docs/tutorials/basics-server-side-html-generation/{branch}) chapter.