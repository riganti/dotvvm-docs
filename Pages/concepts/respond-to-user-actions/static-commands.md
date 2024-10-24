# Static commands

Static commands are a very powerful way to manipulate with the viewmodel on the client-side. In contract to [commands](commands), static command doesn't need to send the viewmodel to the server, and in many cases it doesn't even talk to the server. 

Static command can call methods on the server. In such case, only the method arguments are sent to the server, and the return value of the method can be used to update some viewmodel properties.

## Assign values to properties

You can use static commands to perform simple operations on the viewmodel without making any communication with the server.

It is useful in many simple scenarios, like assigning values into properties. 

```DOTHTML
<dot:Button Text="Something" 
            Click="{staticCommand: SomeProperty = 'Hello ' + Name + '!'}" />
```

You can put multiple statements in the static command bindings and separate them by the `;` operator. If you call a method which can be translated into JavaScript, the entire expression will run locally in the browser.

See the [supported expressions](~/pages/concepts/data-binding/supported-expressions) page for more information.

## Call server methods

Static commands can call methods which are not translated into JavaScript. 

The method can be:

* a static method marked with `[AllowStaticCommand]` attribute
* a method declared in a [static command services](static-command-services) which is imported using the `@service` directive
* a JavaScript method imported by a [JS directive](~/pages/concepts/client-side-development/js-directive/overview)
* a [REST API method](rest-api-bindings/overview)

Asynchronous methods (returning `Task`) are supported, but you need to call `.Result` in the static command binding to access the result.
The static command is executed client-side and the `.Result` property will be translated into non-blocking JavaScript code.

### Static methods

First, you have to declare a static method. It can be in the viewmodel or in any other class. 

The method must be `static` and can accept any number of arguments which are JSON-serializable. 

The method must be marked with the `AllowStaticCommand` attribute. DotVVM needs the methods to be explicitly allowed for static commands; otherwise, anyone would be able to call any static method (e.g. `File.Delete`) with any arguments.

Optionally, the method can return a result.

```CSHARP
[AllowStaticCommand]
public static string MyMethod(string name)
{
    // ...
    return result;
}
```

> Be careful. There is no way for DotVVM to determine whether the arguments passed to the command weren't tampered with. Always validate that the values are correct and that the user has appropriate permissions to perform the operation.

The binding expression in the page looks like this:

```DOTHTML
<dot:Button Text="Something" Click="{staticCommand: MyClass.MyMethod(SomeArg)}" />
```

Also, you may want to use the method result to update some viewmodel property.

```DOTHTML
<dot:Button Text="Something" Click="{staticCommand: SomeProperty = MyClass.MyMethod(SomeArg)}" />
```

If the `MyClass` is not in the same namespace as the viewmodel, use the `@import` directive.

### Dependency injection

For non-trivial methods, we generally recommend using [static command services](static-command-services) instead of static methods, as it enables [dependency injection](~/pageS/concepts/configuration/dependency-injection/overview) of other services.
However, DotVVM also allows you to fill any method arguments with services imported from `@service` directives, making it possible to use DI with static methods.

Note that in static commands, `IDotvvmRequestContext` is only available as an injected service.
The `Context` property is not populated, and accessing it might trigger the transfer of the entire viewmodel.

### JavaScript methods

If you import a view module using the [JS directive](~/pages/concepts/client-side-development/js-directive/overview), you can call exported methods using the `_js.Invoke` method:

```DOTHTML
<dot:Button Text="Call JS method" Click="{staticCommand: SomeProperty = _js.Invoke<string>("myMethod", SomeArg, ...)}" />
```

Server-side methods and `_js.Invoke` may be arbitrarily combined.
For instance, we can extract a value using JS function `m1`, send it to the server and pass the result: `_js.Invoke("m2", MyClass.MyMethod(_js.Invoke<string>("m1"))`

### REST API methods

See [REST API bindings](rest-api-bindings/overview) for more information.

## See also

* [Static command services](static-command-services)
* [JS directive](~/pages/concepts/client-side-development/js-directive/overview)
* [REST API bindings](rest-api-bindings/overview)
* [Optimize command performance](optimize-command-performance)
* [Concurrency mode](concurrency-mode)

