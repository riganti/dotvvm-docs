# Static Command Binding

If you use the [Command Binding](/docs/tutorials/basics-command-binding/{branch}), the viewmodel is serialized and sent to the server where the 
method is executed. 

The problem is that the viewmodel can be too large and it wouldn't be efficient to transfer it to the server and back.

It is really not an issue on pages with simple UI because the viewmodel would be small. Also, DotVVM allows to configure which properties will be 
transferred from the client to the server and back (see the `Bind` attribute in the [ViewModels](/docs/tutorials/basics-viewmodels/{branch}) page.

However, in complicated pages with modal dialogs and complex viewmodel, it may not be a wise solution to send the whole viewmodel to the server over and over.
That's why DotVVM contains the **static command binding**.

## Calling a Static Method

A **static command** allows to call a static method on the server and use its result to update a property of the viewmodel. 
You can pass any arguments to the method and the method can return a result. You can assign the result to any property in the viewmodel.

First, you have to declare a method in your viewmodel. The method must be `static` and can accept any number of arguments.
The only requirement is that they must be JSON-serializable. 

Optionally, the method can return a result.

```CSHARP
[AllowStaticCommand]
public static string MyMethod(string name)
{
    // ...
    return result;
}
```

The method must be marked with the `AllowStaticCommand` attribute. Otherwise, anyone would be able to call any static method (e.g. `File.Delete`).

> Be careful. There is no way for DotVVM to determine whether the arguments passed to the command weren't tampered with. 
Always validate that the values are correct and that the user has appropriate permissions to perform the operation. 

## Using the Static Commands

The binding in the page looks like this:

```DOTHTML
<dot:Button Text="Something" Click="{staticCommand: MyClass.MyMethod(Name)}" />
```

Also, you may want to use the method result to update some viewmodel property.

```DOTHTML
<dot:Button Text="Something" Click="{staticCommand: SomeProperty = MyClass.MyMethod(Name)}" />
```

## Client-only Expressions

You may also execute simple operations on the viewmodel without making any communication with the server.
It is useful e.g. when you need to switch some property to `false` or something like that. 

```DOTHTML
<dot:Button Text="Something" Click="{staticCommand: SomeProperty = 'Hello ' + Name + '!'}" />
```

Only basic expressions are supported here. See the [Value Binding](/docs/tutorials/basics-value-binding/{branch}) page for more information.
