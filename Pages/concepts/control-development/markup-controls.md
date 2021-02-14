# Markup-Only Controls

It is very easy to create a markup control. First, you need to create a file with the `.dotcontrol` extension.

The markup control files must specify the `@viewModel` directive which tells DotVVM in which [binding context](/docs/tutorials/basics-binding-context/{branch}) the control will be used. It can be an interface or a base class, but the control needs to know on which type the data-bindings are evaluated. 

> Remember that neither markup controls nor master pages create an instance of the viewmodel. Only page has its own viewmodel instance. In master pages and user controls, the `@viewModel` directive specifies the "contract" - a base class or interface for the place that the viewmodel must implement or derive from.  

If your markup control doesn't contain any data-bindings and doesn't depend on a specific viewmodel, use `@viewModel System.Object, mscorlib`. It means that the binding context in which the control is used, can be anything.


## How to create Markup Controls

Simple markup controls don't need any C# code. In Visual Studio, right click on a project or folder in the *Solution Explorer* window, and select *Add > New Item*.

<p><img src="{imageDir}control-development-markup-only-controls-1.png" alt="Adding a DOTCONTROL file" /></p>

In the next window, don't change anything and proceed.

<p><img src="{imageDir}control-development-markup-only-controls-2.png" alt="Adding a DOTCONTROL file" /></p>

The `.dotcontrol` file can look like this:

```DOTHTML
@viewModel DotvvmDemo.Model.IAddress, DotvvmDemo

<table>
    <tr>
        <td>Street: </td>
        <td><dot:TextBox Text="{value: Street}" /></td>
    </tr>
    ...
    <tr>
        <td>Country: </td>
        <td>
        <dot:ComboBox DataSource="{value: Countries}" 
                      SelectedValue="{value: CountryId}" 
                      ItemTextBinding="{value: Name}" 
                      ItemValueBinding="{value: Id}" />
        </td>
    </tr>
</table>
```

You can see that the control requires a binding context of type `IAddress`. It can be used on any place where the `DataContext` implements this interface.

## Markup Controls WrapperTag

Markup controls are wrapped inside a `div` tag by default. You can specify the tag you want to use by adding `@wrapperTag` directive. Additionally if you don't want to use any wrapper tag you can add the `@noWrapperTag` directive.

```DOTHTML
@viewModel DotvvmDemo.Model.IAddress, DotvvmDemo
@wrapperTag table

<tr>
    <td>Street: </td>
    <td><dot:TextBox Text="{value: Street}" /></td>
</tr>
...
<tr>
    <td>Country: </td>
    <td>
        <dot:ComboBox DataSource="{value: Countries}" 
                      SelectedValue="{value: CountryId}" 
                      ItemTextBinding="{value: Name}" 
                      ItemValueBinding="{value: Id}" />
    </td>
</tr>
```

This will render the control inside the `table` tag.

>The `@noWrapperTag` cannot be used when the control is used with properties.
