You can define, what tag the *Repeater* renders, using the `WrapperTagName`. The default is **div**:

```DOTHTML
<dot:Repeater DataSource="{value: Items}">
    <ItemTemplate>
        <p>{{value: Name}}</p>
    </ItemTemplate>
</dot:Repeater>

<!-- Client rendering mode -->
<div data-bind="template: { name: "hashOfTheTemplateContent", data = ...}">
</div>
<template id="hashOfTheTemplateContent">
    <p><span data-bind="..."></span></p>
</template>

<!-- Server rendering mode -->
<div>
    <p>Jim Hacker</p>
    <p>Humphrey Appleby</p>
    <p>Bernard Woolley</p>
</div>
```
> The `ItemTemplate` is default element property and so the `<ItemTemplate>` element **can be omitted** if it's the only one inside the Repeater control.

Using the `RenderWrapperTag` you can turn off the wrapper tag. The Knockout comments will then be used 
in the [client rendering mode](~/pages/concepts/server-side-rendering):
```DOTHTML
<!-- ko foreach: ... -->
<p><span data-bind="..."></span></p>
<!-- /ko -->
```

Be careful. Knockout comment bindings don't work well if they are directly in the `<table>` element. Put the Repeater inside the `<tbody>` element and it will work.

> From DotVVM 4.0, the `Repeater` control renders the `<template>` element by default instead of rendering the template inside the wrapper tag. If you want to fall back to the previous behavior, set the `RenderAsNamedTemplate` property to `false`.