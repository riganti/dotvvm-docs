# Security headers

DotVVM 4.0 added support for several security-related HTTP headers. We believe they should be used in most cases, they are therefore enabled by default.
However, all the headers and checks can be disabled in DotvvmConfiguration, if they cause issues.

Firstly, **this DOES NOT magically protect your application** from all kinds of attacks. Most common attacks are related to wrongly configured authentication and insufficient user input validation, and none of these headers can help with that. Please remember that everything in your viewmodel which is not marked by the `[Protect]` attribute is suddenly a user input in a command or static command. See the [Recommendations for viewmodels](recommendations-for-viewmodels) section for more info.

Also, we only set headers which are not likely to cause any problems to your application — incidentally, these are the headers which are less effective. 

Please don't forget to set the `Strict-Transport-Security` yourself and check your application using [Mozilla Observatory](https://observatory.mozilla.org/) — it may reveal the most common security misconfigurations.

## Running DotVVM pages in iframe

DotVVM sets the `X-Frame-Options: DENY` header by default, which disallows running in an HTML frame.
This is configured using `config.Security.FrameOptionsSameOrigin` for same-origin ("internal") frames and `config.Security.FrameOptionsCrossOrigin` for cross-origin ("external") frames.

Please note that enabling cross-origin iframes is actually somewhat risky, and we recommend enabling it only for routes where it is needed.
Additionally, the iframe hosts can be restricted using `Content-Security-Policy: frame-ancestors ...`, if you don't need to make it generally available for embedding.
When cross-site frames are enabled on any route, the DotVVM session cookie will have `SameSite=None` to allow postbacks from the other sites.

DotVVM also checks the `Sec-Fetch-Dest` header — if it's an iframe, we verify on server that iframes are allowed. This mostly allows us to print a nicer error message, but may also in theory prevent some timing attacks using iframes.

## XSS protection header

The `X-XSS-Protection: 1; mode=block` blocks some kinds of XSS attacks. It is probably not super-useful, but it's also not harmful. This header is configured by `config.Security.XssProtectionHeader` property.

The `X-Content-Type-Options: nosniff` disables the inference of content-type based on content, this could prevent some XSS attacks. It is also not super-useful, but we believe it's unlikely to cause any problems. This is configured by the `config.Security.ContentTypeOptionsHeader` property.

## Session cookie disabled for subdomains

If the website is running on HTTPS, we set the session cookie with the `__Host-` prefix. This prevents subdomains from accessing it, which can help if a system running on subdomain gets compromised.

## Sec-Fetch headers

DotVVM checks the `Sec-Fetch-*` headers, which tell us what the browser intends to do with the page and whether it is a cross-origin or same-origin request:

* We don't allow JS initiated GET requests to DotVVM pages.
    - This makes it harder to scrape data from your application, if the attacker can exploit a XSS vulnerability or leaked CSRF token.
    - However, the check is incompatible with some legitimate use cases, such as link prefetching and prerendering. Use the `config.Security.VerifySecFetchForPages` option to disable this check.
        - Specifically for presentation websites, link prefetching is likely more valuable, and we can recommend turning this check off.
    - Note that it is implicitly disabled for [SPA](../layout/single-page-applications-spa) requests.
* We never allow cross-origin postbacks and [SPA](../layout/single-page-applications-spa) requests.
* If the browser does not send these Sec-Fetch-* headers, we don't check anything. Strict checking can be enabled using the `config.Security.RequireSecFetchHeaders` option. By default, it's enabled on compilation page to prevent SSRF.

<!-- Include the error message so google can find this -->
If the checks don't pass, DotVVM will return an HTTP 403 response with plain text body `request rejected: ...`.
For instance, if a `Sec-Dest-Fetch: empty` GET request is sent to a page, we return `request rejected: Pages can not be loaded using Javascript for security reasons.` along with some additional info.

## See also

* [Recommendations for viewmodels](recommendations-for-viewmodels)
* [Authentication & authorization](authentication-and-authorization/overview)
* [Configuration](../configuration/overview)

