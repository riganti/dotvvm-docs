This control is used to render a text in the page. If you use the binding with double curly braces `{{value: Something}}`, it generates the *Literal* control.

If you set the `FormatString` property, you can apply a format string to numbers and date-time values. Most of .NET Framework format strings is supported.

By default, the *Literal* encodes the text, so you don't have to worry about the *script injection* attacks. 

If you want to render raw HTML content in the page, use [HtmlLiteral](~/controls/builtin/HtmlLiteral) control instead.