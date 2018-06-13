# Markup Control Registration

To be able to use custom markup controls, you need to register them in the `DotvvmStartup.cs` file. 

The registration of a markup control looks like this:

```CSHARP
config.Markup.AddMarkupControl("cc", "AddressEditor", "Controls/AddressEditor.dotcontrol");
```

We have registered the `Controls/AddressEditor.dotcontrol` control under a tag name `<cc:AddressEditor>`.

> If you use [DotVVM for Visual Studio](https://www.dotvvm.com/landing/dotvvm-for-visual-studio), you need to rebuild the project after registering the control, otherwise the IntelliSense won't display the control in the suggestion list.  

## Using the Markup Control

We have registered our control with the `cc` tag prefix and `AddressEditor` name, so we can just write this:

```DOTHTML
<fieldset>
    <legend>Billing Address</legend>
    <cc:AddressEditor DataContext="{value: BillingAddress}" />
</fieldset>
<fieldset>
    <legend>Delivery Address</legend>
    <cc:AddressEditor DataContext="{value: DeliveryAddress}" />
</fieldset>
```

Note that both objects `BillingAddress` and `DeliveryAddress` must implement the `IAddress` interface. If they don't, DotVVM will show an error page.
