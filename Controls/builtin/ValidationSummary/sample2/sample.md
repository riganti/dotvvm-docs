## Sample 2: Validation errors from child objects

By default, the `ValidationSummary` control displays errors that comes directly from the `Validation.Target` object's properties. 
If the validation target contains another child objects, the validation errors from those objects are not displayed.
This is because of performance reasons.

However, using the `IncludeErrorsFromChildren` property, you can tell the control to display validation errors event from the 
child objects. Just be careful because there will be a performance penalty if you use this feature on large and complicated viewmodels.