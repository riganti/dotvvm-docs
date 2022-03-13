# Control capabilities

Control capabilities are a way to declare DotVVM [control properties](control-properties) in a concise and reusable way. 

If you have written a custom control, you know that declaring properties is quite verbose. Also, when making a thin wrapper around other controls, it's painful that most of their properties have to be re-declared and then manually copied onto the child control. Control capabilities are designed to fix those two problems.

A capability is a normal C# record or a plain data (POCO) class, which has the `[DotvvmControlCapability]` attribute:

```csharp
[DotvvmControlCapability]
public sealed record ExampleCapability
{
    public string? MyProperty { get; init; } // will default to null
    public string MyAnotherProperty { get; init; } = "default-value";
}
```

This declares a reusable set of control properties which any control can "import" with the following registration:

```CSHARP
public static readonly DotvvmProperty ExampleCapabilityProperty =
    DotvvmCapabilityProperty.RegisterCapability<ExampleCapability, MyControl>();
```

`MyControl` will now allow both properties from the `ExampleCapability`: 

```DOTHTML
<cc:MyControl MyProperty="A" MyAnotherProperty="something" />
``` 

To access the properties from C# code, you can use the `control.GetCapability<ExampleCapability>()` method. Alternativelly, to get only one of the properties, use `control.GetValue<string>(nameof(ExampleCapability.MyProperty))`. 

To set a capability into a control, the `control.SetCapability(...)` method is available.

## Property types in capabilities

Note that capabilities may contain properties of:

* primitive types - they will be mapped to HTML attributes
* `ITemplate`, `DotvvmControl` or a collection of `DotvvmControl` - those will be mapped as inner elements
* other capabilities - will be registered recursively and their properties will be available to the control as well.

## Prefixes

To prevent name conflicts and to allow multiple registered capabilities of the same type, you can specify a prefix for all properties in the capability. 

The prefix is specified as a parameter to the `RegisterCapability("prefix")` method, or as a parameter of the `[DotvvmControlCapability("prefix")]` attribute (use this when declaring nested capabilities or when declaring capability for composite controls).

```CSHARP
public static readonly DotvvmProperty ExampleCapabilityProperty =
    DotvvmCapabilityProperty.RegisterCapability<ExampleCapability, MyControl>("Example-");
```

In this case, `MyControl` will allow both properties from the `ExampleCapability`, but prefixed with `Example-`: 

```DOTHTML
<cc:MyControl Example-MyProperty="A" Example-MyAnotherProperty="something" />
``` 

## Built-in capabilities in the framework

The framework contains a few standard capabilities which help with frequently used tasks - `HtmlCapability` and `TextOrContentCapability`.

### HtmlCapability

The `HtmlCapability` allows the control to have `ID`, `class` attributes (including the `class-name={value: expression}` property groups), `style` (including the `style-name={value: expression}` property groups), and any other HTML attributes.

If you declare a [composite control](composite-control) which should support setting these attributes, you can just add `HtmlCapability` as a parameter to the `GetContents` method. You can then pass it e. g. to the `HtmlGenericControl` so it is rendered:

```CSHARP
public class MyControl : CompositeControl
{
    public static DotvvmControl GetContents(
        HtmlCapability htmlCapability,
        ...
    )
    {
        return new HtmlGenericControl("div", htmlCapability) 
            ...
    }
}
```

### TextOrContentCapability

The `TextOrContentCapability` contains two properties - `Text` and `Content`, while one of them needs to be set. It is useful for controls which can either specify a `Text` properties, or specify arbitrary content as an inner content.

For example, the [Button](~/controls/builtin/Button) controls uses this pattern:

```DOTHTML
<!-- Specify the text on the button -->
<dot:Button Text="Delete" ButtonTagName="button" Click="..." />

<!-- ...or specify the contents inside the element -->
<dot:Button ButtonTagName="button" Click="...">
    <img src="delete.png" /> 
    Delete
</dot:Button>

<!-- WRONG - specifying both properties is not allowed -->
<dot:Button Text="Delete" ButtonTagName="button" Click="...">
    <img src="delete.png" /> 
    Delete
</dot:Button>
```

Such control can be declared like this:

```CSHARP
public class MyButton : CompositeControl
{
    public static DotvvmControl GetContents(
        TextOrContentCapability textOrContentCapability,
        ... 
    )
    {
        return new HtmlGenericControl("button", textOrContentCapability)
            ...
    }
}
```

When both properties are set, the capability will throw an exception as it is unclear which has priority.

## See also

* [Control properties](control-properties)
* [Markup controls with code](markup-controls-with-code)
* [Code-only controls](code-only-controls)
* [Composite controls](composite-controls)