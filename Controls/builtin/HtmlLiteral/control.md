This control is used to render raw HTML content in the page. 

> Be very careful when using this control. If you allow users to enter HTML into your application and then you render this HTML,
it can make your application vulnerable. Always make sure that the HTML you pass in this control is safe and doesn't contain
any scripts because the browser would have run them.

If you want to render a text in the page, use the [Literal](~/controls/builtin/Literal) control instead.
