# Common control properties

All DotVVM controls derive from the `DotvvmControl` base class, which gives them some common properties you can use everywhere. 

You can use these properties on plain HTML elements too.

+ `DataContext` - changes the [binding context](~/pages/concepts/data-binding/binding-context) for the content of the control or element.
+ `Visible` - hides the control or element in the page (using CSS `display: none`).
+ `IncludeInPage` - includes or removes the control or element from DOM.
+ `ID` - specifies an ID of the control. 

## HTML attributes on controls

You can add any HTML attributes to (almost) all controls. You can even use [binding expressions](~/pages/concepts/data-binding/overview) in the HTML attributes.
All additional attributes used on the DotVVM control will be added to the main HTML element rendered by the control.

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

> DotVVM allows to combine multiple CSS classes or inline styles dynamically. See [Combine CSS classes and styles](~/pages/concepts/dothtml-markup/combine-css-classes-and-styles) for more information.

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

## Control IDs

Because the control can appear in the page multiple times (e.g. when it is inside the `Repeater` control), the real `id` of the HTML element might be different. Typically, DotVVM will add some prefix to it to make sure it is unique in the page.
Sometimes, the ID is even calculated on the client side dynamically - DotVVM generates a data-binding expression which will calculate the ID. 

You can set the ID of the control or element explicitly by setting the `ClientIDMode` property to `Static`. In such case, you are responsible for making sure that the ID is unique.

## See also

* [Combine CSS classes and styles](~/pages/concepts/dothtml-markup/combine-css-classes-and-styles)
* [Data-binding](~/pages/concepts/data-binding/overview)
* [Respond to user actions](~/pages/concepts/respond-to-user-actions/overview)
* [Built-in controls](~/pages/concepts/dothtml-markup/builtin-controls)
