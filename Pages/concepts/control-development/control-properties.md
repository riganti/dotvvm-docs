# Control properties

The properties in [markup controls with code-behind](markup-controls-with-code) and [code-only controls](code-only-controls) cannot be simple C# properties with default getter and setter. Such properties could only contain values, but they couldn't store [data-binding expressions](~/pages/concepts/data-binding/overview). 

To make the data-binding work, you have to expose those properties as `DotvvmProperty` objects which contain metadata about the property and which can store binding expressions. It is similar to [dependency properties](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/dependency-properties-overview?view=netframeworkdesktop-4.8) known from Windows Presentation Foundation.

## Declare the property in markup-controls with code or code-only controls

[DotVVM for Visual Studio](https://www.dotvvm.com/products/visual-studio-extensions) adds an easy-to-use code snippet, which makes declaration of these properties simple.

To declare a DotVVM property, **type `dotprop` and press Tab**. The property declaration will be generated for you.

> If you are using Resharper and type `dotprop`, it will not see the code snippet and it will match the `DotvvmProperty` class instead. If this happens, press Escape before pressing Tab, and the snippet will work.

After you invoke the `dotprop` code snippet, you can change the name of the property, its type, the containing class, and the default value:

```CSHARP
public string Title
{
    get { return (string)GetValue(TitleProperty); }
    set { SetValue(TitleProperty, value); }
}
public static readonly DotvvmProperty TitleProperty
    = DotvvmProperty.Register<string, AddressEditor>(c => c.Title, "Address");
```

> Until DotVVM 3.0, the declaration of static `DotvvmProperty` field was optional - DotVVM inferred the field automatically. However, this behavior had many limitations and we decided to drop this feature. From DotVVM 4.0, the declaration of `DotvvmProperty` is **required**.

## Declare the property in composite controls

Because declaring properties like this is uncomfortable and the code is hard to maintain, DotVVM 4.0 introduced the [composite controls](composite-controls) which declare the properties as parameters of the `GetContents` method:

```CSHARP
public class MyControl : CompositeControl 
{
    public static DotvvmControl GetContents(
        ValueOrBinding<string> title,
        ...
    )
    {
        ...
    }
}
```

## Specify markup options

The properties can be decorated via the `MarkupOptions` attribute. It allows to specify, whether the property is required, whether it supports [value binding](../data-binding/value-binding) or a hard-coded value in the markup, and whether it is mapped as an attribute or the inner element.

The attribute is applied on the property like this:

```CSHARP
[MarkupOptions(AllowHardCodedValue = false)]
public string Text
{
    get { return (string)GetValue(TextProperty); }
    set { SetValue(TextProperty, value); }
}
public static readonly DotvvmProperty TextProperty
    = DotvvmProperty.Register<string, TextBoxWithLabel>(c => c.Text, null);
```

The attribute in the example above tells DotVVM to accept only the value binding as the value of this property:

```DOTHTML
<!-- only value binding is allowed -->
<cc:TextBoxWithLabel Text="{value: SomeProperty}" ... />

<!-- WRONG - this won't work -->
<cc:TextBoxWithLabel Text="Hello" ... />

<!-- WRONG - the resource binding is evaluated on the server and behaves as a hard-coded value -->
<cc:TextBoxWithLabel Text="{resource: SomeProperty}" ... />
```

### MarkupOptions properties

Use the properties of the attribute to specify the behavior:

* `AllowBinding` (default `true`) - specifies whether the value binding is allowed for this property.
* `AllowHardCodedValue` (default `true`) - specifies whether the hard-coded value or resource binding is allowed for this property.
* `Required` (default `false`) - specifies whether the property must be set in the markup
* `MappingMode` (default `MappingMode.Attribute`) - specifies whether the property value is set as an attribute or inner element (e. g. `ItemTemplate` property of the [Repeater](~/controls/builtin/Repeater) control). 

The attribute is commonly used in [markup controls with code-behind](markup-controls-with-code) and [code-only controls](code-only-controls). 

The [composite controls](composite-controls) specify the support for binding or hard-coded values by using special property types described in the following section. 

## Special property types

If the property accepts only the value binding, you can use the `IValueBinding` type to represent its value. The same applies to `ICommandBinding` for command (or static command) properties.

In [composite controls](composite-controls), the type `ValueOrBinding<T>` is often use it indicates that the property can contain both value binding or a value, and it allows to work with binding expressions in a more elegant way.

To represent templates, the `ITemplate` type is commonly used. 

The control can also have properties of collection types. For example, a collection of strings can be mapped from the attribute with comma-separated values. A collection of `DotvvmBindableObject` or its descendants can be mapped as inner elements (e. g. `Columns` property of the [GridView](~/controls/builtin/GridView) control).

## Control markup options

By default, all DotVVM controls can contain child elements. If you put something inside your control element, it will be placed in the control's `Children` collection.

Some controls may not support inner content, or want to redirect this content to another property. For example, the `Repeater` control doesn't support content (its inner content specified in the markup is not placed in the `Children` collection) - instead, it is using the `ItemTemplate` property to hold the inner content. Also, the property is marked as default, so it is not necessary to write the `<ItemTemplate>` element inside the `<dot:Repeater>` control.

```CSHARP
[ControlMarkupOptions(AllowContent = false, DefaultContentProperty = nameof(Repeater.ItemTemplate))]
public class Repeater : DotvvmControl
{
    ...
}
```

```DOTHTML
<dot:Repeater ...>
    <ItemTemplate>  <!-- this line is optional - ItemTemplate is the DefaultContentProperty-->
        My item {{value: Title}}
    </ItemTemplate>  <!-- this line is optional - ItemTemplate is the DefaultContentProperty-->
</dot:Repeater>
```

## See also

* [Markup controls with code](markup-controls-with-code)
* [Code-only controls](code-only-controls)
* [Composite controls](composite-controls)