## Sample 1: Using inside a Form

The `FormControl` control and a `Form` control could be used together to avoid unnecessary code.

Every `FormControl` property has a representation of a `Form` control with an `Item` prefix (e.g., ItemFormControlType, ItemLayout, ItemLabelType...). All property values are propagated to every `FormControl` control inside a `Form` control. 

The properties specified on a `FormControl` control directly override the property propagated by a `Form` control.

>To avoid adding the `FormControlType` property to every `FormControl`, multiple `FormControls` could be wrapped with a `Form` and an `ItemFormControlType` property set to `FormControl`.