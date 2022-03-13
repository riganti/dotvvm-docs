# Security headers

DotVVM 4.0 added support for several security-related HTTP headers. We believe they should be used in 99% of cases - thus, they are enabled by default. 

Firstly, **this DOES NOT magically protect your application** from all kinds of attacks. Most common attacks are related to wrongly configured authentication and insuficient user input validation, and none of these headers can help with that. Please remember that everything in your viewmodel which is not marked by the `[Protect]` attribute is suddently a user input in a command or static command. See the [Recommendations for viewmodels](recommendations-for-viewmodels) section for more info.

Also, we only set headers which are not likely to cause any problems to your application - incidentally, these are the headers which are less effective. 

Please don't forget to set the `Strict-Transport-Security` yourself and check your application using [Mozilla Observatory](https://observatory.mozilla.org/) - it may reveal the most common security misconfigurations.

## Running DotVVM pages in iframe

The `X-Frame-Options: DENY` is used by default. It should pose no problem since DotVVM did not work with `iframe` anyway due to cookie SameSite policy. When CrossSite frames are enabled, the cookie will have `SameSite=None`.

This is configured using `config.Security.FrameOptionsSameOrigin` and `config.Security.FrameOptionsSameOrigin` properties. Please enable the cross-origin iframes only for routes where you actually need it.

DotVVM also checks the `Sec-Fetch-Dest` header - if it's an iframe, we validate on server that iframes are allowed. This mostly allows us to print a nicer error message, but may also in theory prevent some timing attacks using iframes.

## XSS protection header

The `X-XSS-Protection: 1; mode=block` blocks some kinds of XSS attacks. It is probably not super-useful, but it's also not harmful.

This header is configured by `config.Security.XssProtectionHeader` property.

The `X-Content-Type-Options: nosniff` disables the inference of content-type based on content, this could prevent some XSS attacks. It is also not super-useful, but we believe it's unlikely to cause any problems.

This is configured by the `config.Security.ContentTypeOptionsHeader` property.

## Session cookie disabled for subdomains

If the website is running on https, we set the session cookie with the `__Host-` prefix. This prevents it being used by subdomains which can help in a case when a system on the subdomain is compromised - the attacker can not pivot to the parent domain so easily.

We also check the `Sec-Fetch-*` headers - that tells us what the browsers intends to do with the page and whether it is a cross-origin or same-origin request. Basically, we don't allow cross-origin postbacks and [SPA](../layout/single-page-applications-spa) requests. 

We also don't allow JS initiated GET requests to DotVVM pages. Use the `config.Security.VerifySecFetchForPages` option to disable this if you rely on it.

If the browser does not send these Sec-Fetch-* headers, we don't check anything. You can enable strict checking by `config.Security.RequireSecFetchHeaders` option. By default it's enabled on compilation page to prevent SSRF.

## See also

* [Recommendations for viewmodels](recommendations-for-viewmodels)
* [Authentication & authorization](authentication-and-authorization/overview)
* [Configuration](../configuration/overview)

