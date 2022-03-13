# Markup controls with code-behind

Sometimes you need to pass some parameters in the markup control. 

For example, you don't need to display the *Phone* field in the billing address. Also, you want the control from the sample on the [markup control registration](markup-control-registration) page to generate the `fieldset` and `legend` tags itself. Thus, it will need to know the text you need to display in the `legend` element.

## Create markup control with code-behind

You can create a markup control using the _Add > New Item_ in the _Solution Explorer_ context menu, and after you choose the name of the control, click on the checkbox which generates the _code behind file_ for you.

![Creating a markup with code](markup-controls-with-code_img1.png)

The code behind file `AddressEditor.cs` will be created. Notice that it derives from `DotvvmMarkupControl` which is required for all markup controls.

```CSHARP
public class AddressEditor : DotvvmMarkupControl
{

}
```

## Add code-behind file to a previously created markup control

If you've already created the control without the code-behind file, you can add it later.

1. Create the code-behind class and make it inherit from `DotvvmMarkupControl`.

2. Then, reference this class using the `@baseType` directive:

```DOTHTML
@viewModel DotvvmDemo.Model.IAddress, DotvvmDemo
@baseType DotvvmDemo.Controls.AddressEditor, DotvvmDemo

...
```

## Declare control properties

In our example, we want to add two properties into the control. 

The first property is called `Title` - it will be printed out inside the `legend` tag.

The second property is `DisplayPhoneNumber` property which will show or hide the _Phone_ field. 

The properties in a DotVVM control cannot be simple C# properties with default getter and setter - in order to support [data-binding expressions](~/pages/concepts/data-binding/overview), they need to be exposed as `DotvvmProperty` objects which contain metadata about the property and can store binding expressions. 

[DotVVM for Visual Studio](https://www.dotvvm.com/products/visual-studio-extensions) adds an easy-to-use code snippet, which makes declaration of these properties simple.

To declare a DotVVM property, **type `dotprop` and press Tab**. The property declaration will be generated for you.

> If you are using Resharper and type `dotprop`, it will not see the code snippet and it will match the `DotvvmProperty` class instead. If this happens, press Escape before pressing Tab, and the snippet will work.

After you invoke the `dotprop` code snippet, you can change the name to `Title`, the type to `string`, the containing class to `AddressEditor` and the default value to `"Address"`:

```CSHARP
public string Title
{
    get { return (string)GetValue(TitleProperty); }
    set { SetValue(TitleProperty, value); }
}
public static readonly DotvvmProperty TitleProperty
        = DotvvmProperty.Register<string, AddressEditor>(c => c.Title, "Address");
```

The second property called `DisplayPhoneNumber` of type `bool` with default value `true` will look like this:

```CSHARP
public bool DisplayPhoneNumber
{
    get { return (bool)GetValue(DisplayPhoneNumberProperty); }
    set { SetValue(DisplayPhoneNumberProperty, value); }
}
public static readonly DotvvmProperty DisplayPhoneNumberProperty
    = DotvvmProperty.Register<bool, AddressEditor>(c => c.DisplayPhoneNumber, true);
```

See the [Control properties](control-properties) chapter for more information.

## Access the properties from the control

Now, you can access the value of these properties using the `{value: _control.Title}` binding in the markup. 

Notice that the markup control must declare the `@baseType` directive that specifies the code behind class.

The `.dotcontrol` file will look like this:

```DOTHTML
@viewModel DotvvmDemo.Model.IAddress, DotvvmDemo
@baseType DotvvmDemo.Controls.AddressEditor, DotvvmDemo

<fieldset><legend>{{value: _control.Title}}</legend>
    <table>
        <tr>
            <td>Street: </td>
            <td><dot:TextBox Text="{value: Street}" /></td>
        </tr>
        ...
        <tr Visible="{value: _control.DisplayPhoneNumber}">
            <td>Phone: </td>
            <td><dot:TextBox Text="{value: Phone}" /></td>
        </tr>
    </table>
</fieldset>
```

In the page, we can now use the control like this:

```DOTHTML
    <cc:AddressEditor DataContext="{value: BillingAddress}" 
                      Title="Billing Address" 
                      DisplayPhoneNumber="false" />

    <cc:AddressEditor DataContext="{value: DeliveryAddress}" 
                      Title="Delivery Address" />
```

Note that you can also put a data-binding as a value of the `Title` and `DisplayPhoneNumber` properties.

> In previous versions of DotVVM, we suggested using the `{controlProperty: Title}` binding expression. It still works, but we recommend using `{value: _control.Title}`. The `controlProperty` binding will be deprecated in the future versions of DotVVM.

## Important fact about control properties

The properties of markup controls do not store the value. They are only references to the value or the data-binding specified on the place where the control is used.

If you set a value of the `Title` property from the code-behind, there are 3 situations that can happen:

1. In the page, the `Title` property is not set: `<cc:AddressEditor DataContext="{value: DeliveryAddress}" />`.

The value will not be stored anywhere, and the `Title` will have its default value on the next postback.

2. In the page, the `Title` property is set to a static value: `<cc:AddressEditor DataContext="{value: DeliveryAddress}" Title="Delivery Address" />`.

The value will not be stored anywhere, and the `Title` will have the value `"Delivery Address"` on the next postback.

3. In the page, the `Title` property is bound to some property in the viewmodel: 

```DOTHTML
<cc:AddressEditor DataContext="{value: DeliveryAddress}" Title="{value: DeliveryAddressTitle}" />
```

Only in the third case, the value will be persisted. If you set the `Title` property from the code-behind, the value will be written into the `DeliveryAddressTitle` property in the viewmodel, and you will find it there on the next postback.

> If you need to persist any state information in the markup control, it must be done by data-binding to some viewmodel property.

## Call methods in controls

If you need to add custom logic in the markup control, you can declare a method (e.g. `ClearAddress`) in the code behind file, and invoke it like this:

```DOTHTML
<dot:Button Text="Clear address" Click="{command: _control.ClearAddress()}" />
```

In this case, the `ClearAddress` can be declared in the code behind file because it does the same thing in all of the implementations. The implementation in the code-behind class will look like this:

```CSHARP
public void ClearAddress() 
{
    var target = (IAddress)DataContext;
    target.Street = "";
    target.City = "";
    ...
}
```

Notice that you can access the binding context using the `DataContext` property. We can safely cast it to `IAddress` because the `@viewModel` directive of the control specifies that the binding context must implement this interface. 

## Logic in controls vs viewmodel

We recommend to put only simple logic in the markup controls. If you need to do something more sophisticated, place the logic in the viewmodel.

In the previous example, you can declare the `ClearAddress` method in the viewmodel, and call just `{command: ClearAddress()}`. In such case, the `IAddress` interface would have to declare this method, and all classes that implement the interface would have to implement also the `ClearAddress` method. 

It is common for complex controls to ship with their own viewmodels - something like `AddressEditorViewModel`. Such viewmodel can be embedded in the page viewmodel, and it is easy to inject dependencies in these viewmodels (for example, to validate the ZIP codes or load the address from user profiles).

## Updating the viewmodel properties

Data-binding in **DotVVM** can do one more thing - update the source property. 

Imagine we have a `NumericUpDown` control which has one textbox and two buttons. The buttons increase or decrease the value of a number inside the textbox.

In the page, the control can be used like this:

```DOTHTML
<cc:NumericUpDown Value="{value: MyNumber}" />
```

The buttons in the control are using `command` bindings to call the `_control.Up` and `_control.Down` methods.

The `Up` method looks like this:

```CSHARP
public void Up()
{
    // breaking change in DotVVM 4.0 - you need to call SetValueToSource to update the original property
    // previously, calling Value++; was enough 
    SetValueToSource(ValueProperty, Value + 1);
}
```

The `SetValueToSource` will look up the property in the viewmodel (`MyNumber`) to which the `Value` property is bound, and will update its value accordingly. 

> Prior to DotVVM 4.0, the value in the viewmodel was updated just by calling the setter (which contains call to `SetValue`). However, this was not reliable in some cases, and when the property was not bound to anything, calling this had no effect. That's why DotVVM 4.0 added the explicit `SetValueToSource` method.

## See also

* [Markup controls](markup-controls)
* [Markup control registration](markup-control-registration)
* [Code-only controls](code-only-controls)
* [Control properties](control-properties)
* [Adding interactivity using Knockout binding handlers](interactivity)
