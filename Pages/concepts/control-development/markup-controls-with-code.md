# Markup Controls with Code

Sometimes you need to pass some parameters in the markup control.

For example, you don't need to display the *Phone* field in the Billing address, and you want also the control from the last sample to generate the `fieldset` and `legend` tags itself, so the control needs to know the text you need to display in the `legend` element.

You can create a markup control using the _Add > New Item_ in the _Solution Explorer_ context menu and after you choose the name of the control, click on the checkbox which generates the _code behind file_ for you.

<p><img src="{imageDir}control-development-markup-controls-with-code-1.png" alt="Creating the DOTCONTROL with code" /></p>

The code behind class will look like that. Notice that it derives from `DotvvmMarkupControl` which is required for all markup controls.

```CSHARP
public class AddressEditor : DotvvmMarkupControl
{

}
```

## Declaring Control Properties

Now we want to add two properties into the control. The first one is `Title`, which will appear inside the `legend` tag.
The second is `DisplayPhoneNumber` property which will show or hide the Phone field. 

The properties in a **DotVVM** control cannot be simple C# properties with default getter and setter. The reason is that the properties can contain data-bindings. If you declare for example a `int` property, it is not possible to store data-binding inside.

To make data-binding work, you have to expose those properties as `DotvvmProperty` objects which contains metadata about the property. It is similar to dependency properties in WPF.

[DotVVM for Visual Studio](https://www.dotvvm.com/landing/dotvvm-for-visual-studio) contains an easy-to-use code snippet, so you only need to type `dotprop` and press Tab. The property declaration will be generated for you.

> If you are using Resharper and type `dotprop`, it will not see the code snippet and it will match the `DotvvmProperty` class instead. If this happens, press Escape before pressing Tab and the snippet will work.

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

## ControlProperty Binding

Now, you can access the value of these properties using the `{controlProperty: Title}` binding in the markup. 

Notice that the markup control must declare the `@baseType` directive that specifies the code behind class.

The `.dotcontrol` file will look like this:

```DOTHTML
@viewModel DotvvmDemo.Model.IAddress, DotvvmDemo
@baseType DotvvmDemo.Controls.AddressEditor, DotvvmDemo

<fieldset><legend>{{controlProperty: Title}}</legend>
    <table>
        <tr>
            <td>Street: </td>
            <td><dot:TextBox Text="{value: Street}" /></td>
        </tr>
        ...
        <tr Visible="{controlProperty: DisplayPhoneNumber}">
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

## Important fact about control properties

The properties of markup controls do not store the value. They are only references to the value or the data-binding specified on the place where the control is used.

If you set a value of the `Title` property from the code-behind, there are 3 situations that can happen:

1. In the page, the `Title` property is not set: `<cc:AddressEditor DataContext="{value: DeliveryAddress}" />`.

The value will not be stored anywhere and the `Title` will have its default value on the next postback.

2. In the page, the `Title` property is set to a static value: `<cc:AddressEditor DataContext="{value: DeliveryAddress}" Title="Delivery Address" />`.

The value will not be stored anywhere and the `Title` will have the value `"Delivery Address"` on the next postback.

3. In the page, the `Title` property is bound to some property in the viewmodel: `<cc:AddressEditor DataContext="{value: DeliveryAddress}" Title="{value: DeliveryAddressTitle}" />`.

Only in this case the value will be persisted. If you set the `Title` property from the code-behind, the value will be written into the `DeliveryAddressTitle` property in the viewmodel and you will find it there on the next postback.

> If you need to persist any state information in the markup control, it must be done by data-binding to some viewmodel property.

## ControlCommand Binding

If you need to add custom logic in the markup control, you can declare a method (e.g. `ClearAddress`) in the code behind file and invoke it using `controlCommand: ClearAddress()` binding.

Alternatively, we could have this method in the viewmodel, but in that case the `IAddress` interface would have to declare this method and all classes that implement the interface would have to implement also the `ClearAddress` method. 

In this case, the `ClearAddress` can be declared in the code behind file because it does the same thing in all of the implementations.

We can add a button in the `AddressEditor.dotcontrol` file:

```DOTHTML
<dot:Button Text="Clear Address" Click="{controlCommand: ClearAddress()}" />
````

And then, add this method to the `AddressEditor` code behind class:

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


## Updating the ViewModel Properties

Data-binding in **DotVVM** can do one more thing - update the source property. 

Imagine we have a `NumericUpDown` control which has one textbox and two buttons. The buttons increase or decrease the value of a number inside the textbox.

In the page, the control can be used like this:

```DOTHTML
<cc:NumericUpDown Value="{value: MyNumber}" />
```

The buttons in the control are using `controlCommand` bindings to call the `Up` and `Down` methods.
The `Up` method looks like this:

```CSHARP
public void Up()
{
    Value++;
}
```

Because the `Value` property is bound to a property in the viewmodel, DotVVM will update the `MyNumber` property too. 
