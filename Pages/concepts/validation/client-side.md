# Client-side validation

In all cases, the validation takes place _on the server_, because the user might tamper with the HTTP communication, and post a viewmodel which violates some of the validation rules.

However, to prevent unnecessary postbacks and improve the user experience, DotVVM can translate the most common validation attributes to JavaScript, and validate the rules on the client side, before the request to the server is actually made.

DotVVM can do client validation for the following attributes on the client side:

+ `Required`
+ `RegularExpression`
+ `Range`
+ `EmailAddress`
+ `DotvvmEnforceClientFormat` - see the [Formatting dates and numbers](~/pages/concepts/localization-and-cultures/formatting-dates-and-numbers) for more info

> The client-side validation is only an addition to the server-side validation. Even if the rule can be translated to JavaScript and executed on the client side, it is always executed on the server.

## Disable client-side validation

If you don't like the client-side validation, you can turn it off in the [Configuration](~/pages/concepts/configuration/overview). 

In such case, everything will be validated only on the server.

```CSHARP
config.ClientSideValidation = false;
```

## See also

* [Validation overview](overview)
* [Validation controls](controls)
* [Validation controls](target)
* [Extensibility](extensibility)
