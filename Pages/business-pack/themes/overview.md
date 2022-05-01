# Business Pack themes overview

The [DotVVM Business Pack](../overview) ships wit two built-in themes you can use in your application:

* `Enterprise` theme is optimized for business applications with large and complicated forms where you need to utilize space efficiently. The controls don't use rounded corners and their margins are quite small so many fields can fit on the screen. You can see how the controls look like in [DotVVM Business Pack Gallery](https://www.dotvvm.com/gallery/business-pack/).

* `Bootstrap4` theme is emulating the look & feel from the [Bootstrap 4](https://getbootstrap.com/docs/4.6/getting-started/introduction/) library so the controls can be used together with Bootstrap widgets and design elements.

> Starting with the **version 4**, **DotVVM Business Pack doesn't support Internet Explorer**.

## Choosing the theme

By default, the `Enterprise` theme is selected when you add the following line to your `DotvvmStartup.cs`:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    ...
    options.AddBusinessPack();      // Enterprise is the default theme
}
```

If you want to switch to the **Bootstrap 4** theme, change the line to this:

```CSHARP
options.AddBusinessPack(theme: BusinessPackTheme.Bootstrap4);
```

The `Boostrap4` theme doesn't require Boostrap 4 CSS files to be present in the application - all controls are still using the Business Pack CSS styles and the Boostrap 4 look & feel is just emulated.

## Customize the theme

**DotVVM Business Pack** is using [CSS variables](https://developer.mozilla.org/en-US/docs/Web/CSS/Using_CSS_custom_properties) to allow easy customization of visual appearance of the controls.

See the [Customize Business Pack theme](customize) section for more information.

## See also

* [DotVVM Business Pack overview](../overview)
* [Exporting data](../exporting-data)
* [Business Pack controls](~/controls/businesspack/Alert)
* [Release notes](../release-notes)