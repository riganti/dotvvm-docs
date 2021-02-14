# Redefining Bootstrap CSS classes

If you are using [Bootstrap for DotVVM](/landing/bootstrap-for-dotvvm) and you need to modify which css classes (classes on outer control element or classes on inner control structure) are used on Bootstrap 4 controls, than you can use bootstrap Custom CSS API to do so.

## Basic usage
For example, let's say that you want to redefine `<bs:ModalDialog ... />` body to also use css class `custom-body` instead of standard `modal-body`.

1. Create derived class from `ModalDialogCss` with overridden ModalDialogBody property.

```CSHARP
public class CustomModalDialogCss : ModalDialogCss
{
    public override string ModalDialogBody => "custom-body";
}
```

2. Create derived class from `BootstrapCss` with overridden `ModalDialog` property.

```CSHARP
public class CustomBootstrapCss : BootstrapCss
{
    public override IModalDialogCss ModalDialog => new CustomModalDialogCss();
}
```

3. Register your `CustomBootstrapCss` class in `DotvvmStartup`.

```CSHARP
config.Styles.Register<ModalDialog>().RegisterBootstrapCss(new CustomBootstrapCss(),dotvvmConfig);
```

Now every modal dialog would have `custom-body` css class instead of default `modal-body`.
Because we have derived our classes from default implementations than all other classes would be the same. 

We also could provide custom CSS for everything by implementing `IBootstrapCss` interface for our css classes aggregate and `IControlNameCss` (`IModalDialogCss` , ... ) for each control.

If we would want to register such custom css for all types of controls, than the registration would look like this:
```CSHARP
config.Styles.RegisterBootstrapCss(new CustomBootstrapCss(), dotvvmConfig);
```

## Restricting where would our custom CSS be used
If you don't wont our custom classes to be applied to every control, than we can restrict on which controls and under which conditions it would be used by [Server-side Styles API](/docs/tutorials/basics-server-side-styles).

For example if we want to limit our custom classes to only modal dialogs in admin section (has view in folder `~/Views/admin/`) than we can register our `CustomBootstrapCss` this way:

```CSHARP
config.Styles.Register<ModalDialog>().WithCondition(context => context.HasViewInDirectory("~/Views/admin/")).RegisterBootstrapCss(new CustomBootstrapCss(), dotvvmConfig);
```

## Optional settings of Bootstrap Custom CSS

Behavior of Bootstrap Custom CSS can be further customised by setting some properties in `DotvvmBootstrapOptions`.

### InheritCustomCssFromParentControl
When set to *true* (it's true by default), than all control  which does not have `IBootstrapCss` set will try to find some `IBootstrapCss` instance on their ancestors. If no instance is found, than the `DefaultBootstrapCustomCss` is used.

When set to *false* than no inheritance is performed. The control used explicitly set instance of `IBootstrapCss` or `DefaultBootstrapCustomCss`.

### DefaultBootstrapCustomCss
Instance of `IBootstrapCss` which will be used when no other css class is specified for control **AND** if InheritCustomCssFromParentControl is set to *false*.

If InheritCustomCssFromParentControl is *true* than Bootstrap custom CSS class from nearest ancestor with would be used.

If no custom css is found on any of the ancestors than DefaultBootstrapCustomCss is used.
