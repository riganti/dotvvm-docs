Renders read-only plain text or Markdown as HTML. URLs and e-mail addresses are turned into hyperlinks automatically, and line breaks are preserved. Raw HTML in the text is never rendered, so the control is safe to use with user-provided content.

Hard-coded and resource-bound texts are rendered on the server. When `Text` is bound using a value binding, the text is rendered in the browser and the output is updated whenever the value changes.

> The `TextView` control is new in DotVVM 5.1.
