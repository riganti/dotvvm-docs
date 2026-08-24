# Common control properties

All DotVVM controls derive from the `DotvvmControl` base class, which gives them some common properties you can use everywhere. 

You can use these properties on plain HTML elements too.

+ `DataContext` - changes the [binding context](~/pages/concepts/data-binding/binding-context) for the content of the control or element.
+ `Visible` - hides the control or element in the page using CSS `display: none`.
+ `IncludeInPage` - includes or removes the control or element from the DOM.
+ `ID` - specifies an ID of the control (which is used to build element unique ID, see the [Control IDs](#control-ids) section).
+ `Class-my-class` - toggles `my-class` CSS classes on the control based on a value of a binding expression.
+ `Style-some-style` - sets `some-style` to the assigned value or binding, see [Combine CSS classes and styles](~/pages/concepts/dothtml-markup/combine-css-classes-and-styles) for more details.

## HTML attributes on controls

You can add any HTML attributes to (almost) all DotVVM controls. You can even use [binding expressions](~/pages/concepts/data-binding/overview) in the HTML attributes.

All additional attributes used on the DotVVM control will be added to the root HTML element rendered by the control.

```DOTHTML
<dot:TextBox Text="{value: FirstName}" 
             class="btn btn-primary" 
             placeholder="{value: FirstNamePlaceholder}" />
```

This produces the following HTML:

```DOTHTML
<input type="text" 
       class="btn btn-primary"
       data-bind="value: FirstName, attr: { 'placeholder': FirstNamePlaceholder }" />
```

You can see that the `class` attribute has been added to the rendered `input` element, and the `placeholder` attribute was translated to Knockout JS `attr` binding.

If you want to conditionally include or exclude some HTML attribute, you can bind it to a `boolean` expression:

```DOTHTML
<dialog open="{value: IsOpen}">
    ...
</dialog>
```

## Enable or disable form controls

DotVVM also adds a property `FormControls.Enabled` which can enable or disable all form controls inside the element. This can be useful if you want to disable the entire form or part of the page.

Each control can override this property by setting its own `Enabled` property. 

```DOTHTML
<dot:CheckBox Text="Unlock form" Checked="{value: IsFormUnlocked}" />

<form FormControls.Enabled="{value: IsFormUnlocked}">
    <div>
        First Name: 
        <dot:TextBox Text="{value: FirstName}" />
    </div>
    <div>
        Last Name: 
        <dot:TextBox Text="{value: LastName}" />
    </div>
    <div>
        Age:
        <dot:TextBox Text="{value: Age}" Enabled="{value: IsAgeUnlocked}" />
        <dot:CheckBox Text="Unlock field" Checked="{value: IsAgeUnlocked}" />
    </div>
</form>
```

## Include controls conditionally

The `IncludeInPage` property controls whether DotVVM renders the control into the page.
If the value is `false`, the control is not present in the DOM.
Since DotVVM 5.0, `IncludeInPage="{value: ...}"` will render the excluded markup as a Knockout template; older DotVVM version will keep the excluded element in the DOM before client-side initialization.

```DOTHTML
<dot:Literal Text="{value: Label}"
             IncludeInPage="{value: IsVisible}"
             RenderSpanElement="false" />
```

Use `Visible` when the element should stay in the DOM and only be hidden.
Use `IncludeInPage` when the element should be created only while the condition is true.

## Control IDs

Because a control can appear in the page multiple times (e.g. when it is inside the `Repeater` control), the real `id` of the HTML element might be different. Typically, DotVVM will add some prefix to the `ID` property of the control  it to make sure it is truly unique in the page.

Sometimes, the ID is even calculated on the client side dynamically - DotVVM generates a data-binding expression which will calculate the ID. 

> Be careful when you interact with HTML elements from JavaScript code using their IDs, because the IDs might change dynamically.

You can set the final ID of the control or element explicitly by setting the `ClientIDMode` property to `Static`. In such case, you are responsible for making sure that the ID is unique.

## See also

* [Combine CSS classes and styles](~/pages/concepts/dothtml-markup/combine-css-classes-and-styles)
* [Data-binding](~/pages/concepts/data-binding/overview)
* [Respond to user actions](~/pages/concepts/respond-to-user-actions/overview)
* [Built-in controls](~/pages/concepts/dothtml-markup/builtin-controls)
