# Static Command Binding

If you use the [Command Binding](/docs/tutorials/basics-command-binding/{branch}), the viewmodel must be serialized and sent to the server where the 
method is executed. 

The problem is that the viewmodel can be too large and it wouldn't be efficient to transfer it to the server and back.

It is really not an issue on pages with simple UI because the viewmodel would be small. Also, DotVVM allows to configure which properties will be 
transferred from the client to the server and back (see the `Bind` attribute in the [ViewModels](/docs/tutorials/basics-viewmodels/{branch}) page.

However, it may not be a wise solution to send the whole viewmodel to the server over and over in complicated pages with heavy data grids, modal dialogs or any other UI constructs which require a complex viewmodel. 

That's why DotVVM contains the **static command binding**. See [Optimizing PostBacks](/docs/tutorials/basics-optimizing-postbacks/{branch}) for more information.

A **static command** allows to call a method on the server and use its result to update a particular property of the viewmodel. 
You can pass any arguments to the method and the method can return a result. You can assign the result to any property in the viewmodel.

Static commands can perform invoke different methods:

* Static method in any class
* Perform a client-side assignment
* Call a [REST API method](/docs/tutorials/basics-rest-api-bindings/{branch})
* Call a method on a service in the view - [Static Command Services](/docs/tutorials/basics-static-command-services/{branch})

## Calling a Static Method

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

The method must be marked with the `AllowStaticCommand` attribute. DotVVM needs the methods to be explicitly allowed for static commands; otherwise, anyone would be able to call any static method (e.g. `File.Delete`) with any arguments.

> Be careful. There is no way for DotVVM to determine whether the arguments passed to the command weren't tampered with. Always validate that the values are correct and that the user has appropriate permissions to perform the operation. 

The binding in the page looks like this:

```DOTHTML
<dot:Button Text="Something" Click="{staticCommand: MyClass.MyMethod(Name)}" />
```

Also, you may want to use the method result to update some viewmodel property.

```DOTHTML
<dot:Button Text="Something" Click="{staticCommand: SomeProperty = MyClass.MyMethod(Name)}" />
```

You need to import the namespace of the `MyClass` using the `@import` directive.

> We recommend to use [Static Command Services](/docs/tutorials/basics-static-command-services/{branch}) instead of static methods. Static methods have many limitations and do not support dependency injection. 

## Client-only Assignments

You can also use static commands to execute simple operations on the viewmodel without making any communication with the server.
It is useful e.g. when you need to switch some property to `false` or something like that. 

```DOTHTML
<dot:Button Text="Something" Click="{staticCommand: SomeProperty = 'Hello ' + Name + '!'}" />
```

Only basic expressions are supported here. See the [Value Binding](/docs/tutorials/basics-value-binding/{branch}) page for more information.

## Other Methods

Static command can be used to call [REST API methods](/docs/tutorials/basics-rest-api-bindings/{branch}) or [Static Command Services](/docs/tutorials/basics-static-command-services/{branch}).

We recommend to use these methods instead of calling static methods using a static command.
