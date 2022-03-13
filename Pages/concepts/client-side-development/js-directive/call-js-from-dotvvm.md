# Call JS from DotVVM

> The JS directive feature is new in DotVVM 3.0. 

> The JS directive functionality is not supported in Internet Explorer 11. 

After the JS module is imported using the [JS directive](overview), you can export functions to be called from DotVVM using [static commands](~/pages/concepts/respond-to-user-actions/static-commands).

## Declare functions in module

To export a function, just declare it in the module class. 

The function can take any number of arguments, and can return a value which may be used in DotVVM binding expression to modify the viewmodel. 

```JS
export default context => new MyModule(context);

class MyModule {
    ...

    sum(number1, number2) {
        return number1 + number2;
    }
}

// ---
// alternative declaration without using the class
export default context => {
    ...
    
    return {
        sum(number1, number2) {
            return number1 + number2;
        },
        ...
    }
}
```

## Call the function from DotVVM

Once you use the `@js` directive in the markup file, you can use the `_js` variable in binding expressions. 

It provides a generic `Invoke` method which can be used to invoke functions in modules:

`_js.Invoke<TReturnValue>("functionName", arg1, arg2, ...)`

If the function doesn't return anything, you can use just `_js.Invoke("functionName", ...)`.

The `sum` function declared above can be called like this:

```DOTHTML
<dot:TextBox Text="{value: Number1}" />
<dot:TextBox Text="{value: Number2}" />
<dot:Button Text="Calculate" 
            Click="{staticCommand: Result = _js.Invoke<double>("sum", Number1, Number2)}" />
```

> DotVVM has no way to verify that the function exists, gets the correct arguments, and that it returns the value of the specified type. If the function call doesn't work, check out the _Developer Tools_ (F11) console in your browser for more info.

> From DotVVM 4.0, this function also checks whether the underlying JavaScript code doesn't return Promise. If it does, `InvokeAsync` must be used, or `TReturnValue` must be `Task` or `Task<T>`. See the following section for information about async functions. 

### Async functions

If the function needs to perform an asynchronous action, you can return a [Promise](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise), and use `_js.InvokeAsync<T>` function instead. It is just a syntax helper for calling `_js.Invoke<Task<T>>` - the signature and usage is the same.

In order to retrieve the value, you need to access the `Result` of the task.

```JS
class MyModule {
    ...

    // this function returns a Promise
    async getNumberOfAttendees() {
        let response = await fetch("/api/attendees");
        let attendees = await response.json();
        return attendees.length;
    }
}
```

```DOTHTML
<dot:Button Text="Refresh" 
            Click="{staticCommand: Count = _js.InvokeAsync<int>("getNumberOfAttendees").Result}" />
```

## See also

* [JS directive overview](overview)
* [Call DotVVM from JavaScript](call-dotvvm-from-js)
* [Use TypeScript to declare modules](use-typescript-to-declare-modules)
* [Sample app: JS directive](https://github.com/riganti/dotvvm-samples-js-integration)