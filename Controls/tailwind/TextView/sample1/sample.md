## Sample 1: Plain text and Markdown

By default, `Format` is set to `PlainText` - the text is HTML-encoded, line breaks are preserved, and URLs and e-mail addresses become hyperlinks. Set `Format="Markdown"` to render Markdown.

The second control uses a resource binding, so the Markdown is rendered on the server.

The `HyperlinkTarget` property specifies where the generated links open (`Blank` by default), and `WrapperTagName` changes the element that wraps the content (`div` by default).
