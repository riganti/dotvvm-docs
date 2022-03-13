## Sample 3: Validation errors from the `Validation.Target`

The `ValidationSummary` control also displays validation errors attached directly to the `Validation.Target` object. This is useful for errors which do not belong to a particular property (for example, incorrect username or password). 

You can disable the behavior by setting the `IncludeErrorsFromTarget` property to `false`.

> The sample below creates a server-side validation error using the [IValidatableObject](~/pages/concepts/validation/overview) interface.

> The default value of the `IncludeErrorsFromTarget` property was changed in DotVVM 4.0 - previously, it was `false`. In DotVVM 4.0, we've decided to change this to `true` as it is a more reasonable default value.