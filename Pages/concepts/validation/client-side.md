# Client-side validation

In all cases, the validation takes place _on the server_, because the user might tamper with the HTTP communication, and post a viewmodel which violates some of the validation rules.

However, to prevent unnecessary postbacks and improve the user experience, DotVVM can translate the most common validation attributes to JavaScript, and validate the rules on the client side, before the request to the server is actually made.

DotVVM can do client validation for the following attributes on the client side:

+ `Required`
+ `RegularExpression`
+ `Range`
+ `EmailAddress`
+ `DotvvmEnforceClientFormat` - see [details](#DotvvmEnforceClientFormat) below

> The client-side validation is only an addition to the server-side validation. Even if the rule can be translated to JavaScript and executed on the client side, it is always executed on the server.

## Disable client-side validation

If you don't like the client-side validation, you can turn it off in the [Configuration](~/pages/concepts/configuration/overview). 

In such case, everything will be validated only on the server.

```CSHARP
config.ClientSideValidation = false;
```

## DotvvmEnforceClientFormat attribute

DotVVM contains a special validation attribute called `DotvvmEnforceClientFormat`. 

It is applied automatically on date and numeric properties, and makes sure that a validation errors is raised when a value entered by the user (e. g. in a [TextBox](~/controls/builtin/TextBox)) cannot be parsed. See [Formatting dates and numbers](~/pages/concepts/localization-and-cultures/formatting-dates-and-numbers) for more info.

This attribute does nothing on the server - it only has a client-side behavior. You may not need to use this attribute, except for a case when you need to turn this default behavior off.

## See also

* [Validation overview](overview)
* [Validation controls](controls)
* [Validation controls](target)
* [Extensibility](extensibility)
