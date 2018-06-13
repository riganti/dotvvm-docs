# Command Binding

The **command binding** allows to call a method in the viewmodel on the server.
 
Consider the following viewmodel:

```CSHARP
public class MyViewModel {
    ...
    public void Submit() {
        ...
    }
    ...
}
```

In the DOTHTML markup, we can use the [Button](/docs/controls/builtin/Button/{branch}) control and this command binding:

```DOTHTML
<dot:Button Click="{command: Submit()}" Text="Submit Form" />
```

If you run the page and view the page source code, you'll see that **DotVVM** translates the binding to a snipped of JavaScript code.

This code uses the `dotvvm.postBack` function defined in the `DotVVM.js` file, which is referenced in the page automatically by DotVVM.

```DOTHTML
<!-- DotVVM translates the Button with command binding to the following code -->
<input type="button" onclick="...dotvvm.postBack(...)..." value="Submit Form"/>
```

This function makes an AJAX HTTP POST request to the server. The request contains the current state of the viewmodel serialized in JSON. 
When it arrives to the server, the server loads it and invokes the method specified in the command binding. 

After that, the viewmodel is serialized back to JSON and sent to the client's browser as a response. The changes made to the viewmodel 
on the server are applied and displayed in the page.

## Method Signature

If the method called from the command binding has some arguments, you have to specify them in the command binding. 
You can use any expressions that are supported in the [Value Bindings](/docs/tutorials/basics-value-binding/{branch}).

The method must be `public` and should be `void`, or return `Task` if you plan to make it asynchronous.
In some cases, this can significantly improve the performance of the app because the web server can reuse waiting threads to process other HTTP requests.

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

> You should always use `async Task` instead of `async void` in asynchronous commands - it would probably end with an exception. 


## Supported Expressions

The following items are examples of what can be used in the **command** binding.

* `MyFunction()`
* `MyFunction(Argument)`
* `MyFunction(Argument.Collection[5].Property * 2)`
* `Property.MyFunction(42, "test")`
* `Collection[3].Property.MyFunction(Argument)`
