# Client Side vs Server Side Validation

In all cases, the validation takes place _on the server_, because the user might tamper with the HTTP communication and post a viewmodel which violates some of the validation rules.

However, to prevent unnecessary postbacks, DotVVM can translate the most common validation attributes to JavaScript and validate the rules on the client side, before the postback is actually sent to the server.

DotVVM can do client validation for the following attributes on the client side:

+ `Required`
+ `RegularExpression`
+ `Range`
+ `DotvvmEnforceClientFormat` - see the [Formatting Dates and Numbers](/docs/tutorials/basics-formatting-dates-and-numbers/{branch}) for more information.
+ `EmailAddress`

> The client side validation is only an addition to the server side validation. Even if the rule can be translated to JavaScript and executed on the client side, it is also always executed on the server side.

You can turn the client validation off in the [Configuration](/docs/tutorials/basics-configuration/{branch}). In that case, everything will be validated only on the server.

```CSHARP
config.ClientSideValidation = false;
```

If you write your own validation attributes, they will be evaluated on the server side. Currently, DotVVM doesn't support extending client validation in custom validation attributes. 
