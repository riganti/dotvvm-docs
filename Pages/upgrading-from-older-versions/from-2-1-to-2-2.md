# Upgrading from 2.1 to 2.2

See [Release notes of DotVVM 2.2](https://github.com/riganti/dotvvm/releases/tag/v2.2.0.2) for complete list of changes.

## Breaking changes

### 1. Session cookies are SameSite

If you use your app in `iframe` from a different site, it may stop working bacause we've changed the cookies to be `same-site`. You can implement your own `ICookieManager` if you want to provide a cookie without this option.

### 2. Validation - EnforceClientFormat is enabled by default

If you have a nullable property of a numeric or a `DateTime` type edited by the user in e. g. `TextBox`, you want to make sure the value is in a correct format. When the user enters a value which cannot be parsed, `null` value will be stored in the viewmodel. Since the value is optional, it is a ligitimate situation, however the user has no way to easily determine if the value was parsed or not. That's why we introduced the `EnforceClientFormat` attribute which will raise a validation error if the value is not in a correct format.

This attribute is now applied by default. We believe it is a reasonable behavior, but new validation errors may be shown on places where they weren't before. We recommend to go through all optional numeric or DateTime properties and making sure the forms in the app were not affected by this change. 

## See also

* [From 2.2 to 2.3](from-2-2-to-2-3)