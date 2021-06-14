# HTML encoding

When you need to display any content to the users, in most cases it should be HTML-encoded to prevent [cross-site scripting](https://en.wikipedia.org/wiki/Code_injection#Cross-site_scripting) attacks.

DotVVM takes the safety-first approach, and encodes automatically all content before printing it out using the [value binding](~/pages/concepts/data-binding/value-binding) or [resource binding](~/pages/concepts/data-binding/resource-binding). 

```DOTHTML
<!-- The DisplayName is HTML-encoded automatically -->
<p>Hi, {{value: DisplayName}}!</p>
```

## Render raw HTML

Sometimes, you need to render raw HTML without the encoding. There are only two ways how it can be done in DotVVM:

* Use the [HtmlLiteral](~/controls/builtin/HtmlLiteral) control.

```DOTHTML
<dot:HtmlLiteral Html="{resource: BlogPostHtml}" />
```

* Build your own [code-only control](~/pages/concepts/control-development/code-only-controls) and use `writer.WriteUnencodedText` method when rendering the control.

## Review the app security

If you want to make sure your app is secure, we recommend to:

* search for all usages of `HtmlLiteral` (in the markup or instantiated from C# code)
* search for all usages of `writer.WriteUnencodedText`

For all these usages, you need to track the origin of the value - where does it come from or how it was composed. 

Sometimes, it comes from a trusted source (e. g. a small group of the website authors) where you don't need to do anything.

In other cases, it is provided by the website users: entirely, or partially (users provide some input which is inserted into some template). In such cases, you either need to HTML-encode all the input from the user, or use libraries like [HtmlSanitizer](https://www.nuget.org/packages/HtmlSanitizer) to remove all potentially dangerous constructs from the HTML.

We also recommend to use some penetration testing tool to discover potential vulnerabilities.

## See also

* [Recommendations for viewmodels](recommendations-for-viewmodels)
* [Authentication & authorization](authentication-and-authorization/overview)

