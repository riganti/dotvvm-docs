# Markup control registration

To be able to use custom [markup controls](markup-controls), you need to register them in the `DotvvmStartup.cs` file. 

## Register a single control

The registration of a markup control looks like this - you need to specify the tag prefix, tag name, and the path to the control:

```CSHARP
public void ConfigureControls(DotvvmConfiguration config, string applicationPath)
{
    config.Markup.AddMarkupControl("cc", "AddressEditor", "Controls/AddressEditor.dotcontrol");
}
```

> If you use [DotVVM for Visual Studio](https://www.dotvvm.com/products/visual-studio-extensions), you need to run the project after registering the control, otherwise the IntelliSense won't display the control in the suggestion list.

## Register multiple controls

Registering the controls one-by-one may be bothering, so there is also a way to register all controls from the same folder at once.

```CSHARP
public void ConfigureControls(DotvvmConfiguration config, string applicationPath)
{
    config.Markup.AutoDiscoverControls(new DefaultControlRegistrationStrategy(config, "cc", "Controls"));
}
```

This will look for all `*.dotcontrol` files in the `Controls` directory, and register all controls under the `cc` prefix.

## Use the control

We have registered our control with the `cc` tag prefix and `AddressEditor` name, so we can now use it in the page:

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

Note that both objects `BillingAddress` and `DeliveryAddress` must be compatible with the type specified in the `@viewmodel` directive of the control. If the `DataContext` in which the control is used, doesn't inherit or implement from the control's binding context type, you will get an error.

## Embed markup control in a class library

If you need to share the control in multiple DotVVM projects, you can place the `.dotcontrol` file in a class library project (DLL), and reference it from the web application.

The `.dothtml` file must be marked as _Embedded Resource_ (select the file in the Solution Explorer window, press F4 to display the _Properties_ window, and set the _Build Action_ property to _Embedded Resource_). 

When registering the control, use the following format of the path: `embedded://AssemblyName/EmbeddedResourceName`

```CSHARP
config.Markup.AddMarkupControl("cc", "AddressEditor", "embedded://Your.Assembly/Path.To.File.dotcontrol");
```

The last part of the URI (Embedded Resource Name) is a relative path to the file in the project with slashes replaced with dots - `Controls/AddressEditor.dotcontrol` will be translated to `Controls.AddressEditor.dotcontrol`. 

If the resource cannot be found, we recommend to open the DLL file in [ILSpy](https://github.com/icsharpcode/ILSpy/releases) or a similar tool - you will see all embedded resources names.

It is common to provide an extension method for DotVVM configuration, that will register all controls embedded in the assembly. 

```CSHARP
public static class DotvvmConfigurationExtensions
{

    public static void AddMyAwesomeControls(this DotvvmConfiguration config)
    {
        config.Markup.AddMarkupControl("cc", "AddressEditor", "embedded://Your.Assembly/Path.To.File.dotcontrol");
    }

}
```

Then, you can just call `config.AddMyAwesomeControls()` in your `DotvvmStartup.cs`.

## See also

* [Markup controls](markup-controls)
* [Markup controls with code](markup-controls-with-code)
* [Code-only controls](code-only-controls)
* [Adding interactivity using Knockout binding handlers](interactivity)