## Sample 2: Disabling the SPAN element

Sometimes you just need to render the text itself without any wrapper element. You can use the `RenderSpanElement` property
to disable rendering the `<span>` element around the text.

Please keep in mind that if you disable the *span* element, you won't be able to add HTML attributes to the `Literal` (e.g. class, style)
and you won't be able to set the `Visible` property because there is no element on which this properties could be applied.