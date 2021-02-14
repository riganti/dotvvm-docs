# Control Properties and Attributes

All DotVVM controls have some properties in common because they all derive from the `DotvvmControl` class. 
You can use most of these properties on HTML elements too.

+ `DataContext` - changes the [binding context](/docs/tutorials/basics-binding-context/{branch}) for the content of the control / element.
+ `Visible` - hides the control / element in the page (using CSS `display: none`).
+ `IncludedInPage` - allows to remove the control / element from DOM when set to `false`.
+ `ID` - specifies an ID of the control. 

## Control IDs

Because the control can appear in the page multiple times (e.g. when it is inside the `Repeater` control), the real `id` of the HTML element might be different. Typically, DotVVM will add some prefix to it to make sure it is unique in the page.
Sometimes, the ID is even calculated on the client side dynamically. 

You can enforce the ID of the control / element to be the same as you specified in the markup by setting the `ClientIDMode` property to `Static`. In that case, you are responsible to make sure that the ID is unique. 

## HTML Attributes And DotVVM Controls

You can also add any HTML attributes to (almost) all controls. You can use binding in the HTML attributes.
All additional attributes used on the DotVVM control will be added to the main HTML element rendered by the control.

```DOTHTML
<dot:TextBox Text="{value: FirstName}" class="btn btn-primary" placeholder="{value: FirstNamePlaceholder}" />
```

This produces the following HTML:

```DOTHTML
<input type="text" data-bind="value: FirstName, attr: { 'placeholder': FirstNamePlaceholder }" class="btn btn-primary" />
```

You can see that the `class` attribute has been added to the output, and the `placeholder` attribute was translated to Knockout JS `attr` binding.

> DotVVM allows to compose multiple CSS classes or inline styles on one element. See [Class Attribute](/docs/tutorials/basics-class-attribute/{branch}) for more information.

## Form Controls

DotVVM also adds a property `FormControls.Enabled` which can enabled or disable all form controls in the element. The controls may override this property by setting their own `Enabled` property. 

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
