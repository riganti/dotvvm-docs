# DotHTML markup overview

The pages in DotVVM are defined in files with `.dothtml` extension. 

The syntax used in DotVVM is called **DotHTML**. It is a standard HTML syntax with three flavors: **directives**, **binding expressions**, and **DotVVM controls**.

There are also special kinds of files with the `.dotmaster` or `.dotcontrol` extensions ([master pages](~/pages/concepts/layout/master-pages) and [markup controls](~/pages/concepts/control-development/markup-controls)). The syntax used in these files is the same as in `.dothtml` files.

## Directives

Directives are placed at the top of the file. They are used to specify various page-related information.

There is one required directive which must be present in each `.dothtml` file - the `@viewModel` directive:

```DOTHTML    
    @viewModel Namespace.ClassName, Assembly
```

## Binding expressions

Binding expressions allow to bind properties declared in the viewmodel to the specified place in UI, or to invoke commands.

```DOTHTML
<p>Hi, my name is {{value: UserName}}!</p>
<p><img src="{value: ProfileImageUrl}" alt="Profile Image"/></p>
```

If the binding expression is used inside element, as a plain text, you need to enclose it in double curly braces: `{{...}}`.

If the binding expression is passed into an attribute, only one curly braces are required: `attribute="{...}"`. You can use double curly braces if you want.

You cannot combine text and binding in the same attribute - the following construct is not allowed:

```DOTHTML
<!-- do not combine binding with text in attributes -->
<element attribute="{value: this_is_not_allowed}_this-is-not-allowed" />
```

## DotVVM controls

DotVVM controls appear in the markup as HTML elements with a namespace prefix. 

These prefixes are not registered using the xmlns attributes like in XML-like languages. Instead, DotVVM looks in its configuration class (`DotvvmStartup.cs`) for registered namespaces, and looks for the controls in these namespaces.

Each DotVVM control can specify properties using attributes or child elements:

```DOTHTML
<dot:Repeater DataSource="..." style="display: none">
    <ItemTemplate>
        some content...
    </ItemTemplate>
</dot:Repeater>
```

* The `DataSource` property of the [Repeater](~/controls/builtin/Repeater) control is specified as an attribute.

* The `ItemTemplate` property is specified as a child element. Some controls specify a _default content property_, which means that the element with the property name (`<ItemTemplate>` in our case) can be omitted. The following syntax is valid for `Repeater` because `ItemTemplate` is the default content property:

```DOTHTML
<dot:Repeater DataSource="..." style="display: none">
    the ItemTemplate element can be omitted
</dot:Repeater>
```

Whether the property is used as an attribute or as a child element, is defined by the developer of the control. 

It is possible to apply any other HTML attribute to most controls. Notice the `style` attribute applied to the 
`Repeater` control in our example. The most common use case for this is to add a `class` attribute to DotVVM controls.

> The [DotVVM for Visual Studio](https://www.dotvvm.com/products/visual-studio-extensions) extension supports IntelliSense
which will help you discover DotVVM controls and their properties.

## See also

* [Data-binding](~/pages/concepts/data-binding/overview)
* [Respond to user actions](~/pages/concepts/respond-to-user-actions/overview)
* [Built-in controls](~/pages/concepts/dothtml-markup/builtin-controls)
* [Common control properties](~/pages/concepts/dothtml-markup/common-control-properties)

