# Commands

The **command binding** allows to call a method in the viewmodel on the server.
 
Consider the following viewmodel:

```CSHARP
public class MyViewModel 
{
    public void Submit() 
    {
        ...
    }
}
```

In the DotHTML markup, you can use for example the [Button](~/controls/builtin/Button) control with this command binding:

```DOTHTML
<dot:Button Click="{command: Submit()}" Text="Submit Form" />
```

If you run the app and view the source HTML of the page in the browser, you'll see that **DotVVM** translated the binding to a snippet of JavaScript code. This code uses the `dotvvm.postBack` function defined in the client-side part of DotVVM, which is included in the page automatically.

```DOTHTML
<!-- DotVVM translates the Button with command binding to the following code -->
<input type="button" onclick="...dotvvm.postBack(...)..." value="Submit Form"/>
```

This function makes an AJAX request to the server using the browser `fetch` API. The request contains the viewmodel serialized in JSON, and some additional metadata. When the request arrives to the server, the server will create an instance of the viewmodel, populate it with the serialized data, and invoke the method specified in the command binding. 

After that, the changes made to the viewmodel will be serialized to JSON and sent back to the browser. The page will be updated to correspond with the new viewmodel state.

You can find more information in the [viewmodels](~/pages/concepts/viewmodels/overview) chapter.

## Method signature

If the method called from the command binding has some arguments, you have to specify them in the command binding. 

The method in the viewmodel must be `public`, and should either return `Task`, or be a plain `void` method. Using asynchronous methods can significantly improve the throughput of the application because the web server can reuse waiting threads to process other HTTP requests.

```CSHARP
public class MyViewModel {
     
    public void SynchronousCommand() {
        ...
    }
   
    public async Task AsynchronousCommand() {
        await ...
    }
}
```

> You should always use `async Task` instead of `async void` in asynchronous commands - it would probably end with an exception because DotVVM wouldn't know when the asynchronous action is complete. 

## Supported expressions

The following items are examples of what can be used in the command binding.

* `MyFunction()`
* `MyFunction(Argument)`
* `MyFunction(Argument.Collection[5].Property * 2)`
* `Property.MyFunction(42, "test")`
* `Collection[3].Property.MyFunction(Argument)`

Refer to the [value binding](~/pages/concepts/data-binding/value-binding) chapter to see what kind of expressions are supported.

## See also

* [Optimize command performance](optimize-command-performance)
* [Concurrency mode](concurrency-mode)
* [Static commands](static-commands)
* [Static command services](static-command-services)
* [JS directives](~/pages/concepts/client-side-development/js-directive/overview)

