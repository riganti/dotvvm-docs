## Sample 7: Overriding generated templates

You can use the `EditorTemplate-*` property group to override the generated editor control with your own content.

If you need to override the entire form field (including the label element), use the `FieldTemplate-*` instead.

In our sample, we are providing file upload and image preview experience for the `ImageUrl`, and selection of values from a viewmodel collection for the `Type` property. 

> You can use the [Selector attribute](~/pages/concepts/auto-ui/selectors) to auto-generate selection controls.