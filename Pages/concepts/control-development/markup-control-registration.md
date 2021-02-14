# Markup Control Registration

To be able to use custom markup controls, you need to register them in the `DotvvmStartup.cs` file. 

The registration of a markup control looks like this:

```CSHARP
config.Markup.AddMarkupControl("cc", "AddressEditor", "Controls/AddressEditor.dotcontrol");
```

We have registered the `Controls/AddressEditor.dotcontrol` control under a tag name `<cc:AddressEditor>`.

> If you use [DotVVM for Visual Studio](https://www.dotvvm.com/landing/dotvvm-for-visual-studio), you need to run the project after registering the control, otherwise the IntelliSense won't display the control in the suggestion list.  

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

## Embedding Markup Controls in Class Libraries

If you need to share the control in multiple DotVVM projects, you can place the `.dothtml` file in a class library project (DLL) and reference it from the web application.

The `.dothtml` file must be marked as _Embedded Resource_ (select the file in the Solution Explorer, press F4 to display the Properties window and set the Build Action property to Embedded Resource). 

When registering the control, use the following format of the path: `embedded://AssemblyName/EmbeddedResourceName`

```CSHARP
config.Markup.AddMarkupControl("cc", "AddressEditor", "embedded://Your.Assembly/Path.To.File.dotcontrol");
```

The last part of the URI (Embedded Resource Name) is a relative path to the file in the project with slashes replaced with dots - `Controls/AddressEditor.dotcontrol` will be translated to `Controls.AddressEditor.dotcontrol`. 

If the resource cannot be found, we recommend to open the DLL file in [ILSpy](https://github.com/icsharpcode/ILSpy/releases) or a similar tool - you will see all embedded resources names.

> Embedding markup files in DLLs is supported starting from DotVVM 2.0.