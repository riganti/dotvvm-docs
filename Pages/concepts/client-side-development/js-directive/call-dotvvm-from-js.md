# Call DotVVM from JS

> The JS directive feature is new in DotVVM 3.0. 

> The JS directive functionality is not supported in Internet Explorer 11. 

The JS module imported by the [JS directive](overview) often needs to call [commands](~/pages/concepts/respond-to-user-actions/commands) or [static commands](~/pages/concepts/respond-to-user-actions/static-commands) declared in the markup file.

In previous versions of DotVVM, this has often been done by clicking on a hidden button, which had many disadvantages. It was not possible to wait for the action to be completed, or to detect whether the action succeeded or failed, and so on. 

## NamedCommand control

From DotVVM 3.0, a recommended way for invoking commands from JavaScript is using the JS directive and the [NamedCommand](~/controls/builtin/NamedCommand) control. This control can register any command or static command under a custom name which is exposed in the _module context_ variable.

```DOTHTML
<dot:NamedCommand Name="RefreshGrid" 
                  Command="{command: _root.RefreshGrid()}" />
```

The control can be declared anywhere in the markup, but its name must be unique. The command is only available if it is actually present in the page DOM. For example, it if is placed in a [Repeater](~/controls/builtin/Repeater) control, the name must be calculated using a binding expression, and if there are no items in the data source collection, the command won't be present in the page at all.

## Call the command from module

If you have imported a module for the markup file, you can use the `context` variable to call the declared command.

```JS
export default context => new MyModule(context);

class MyModule {
    
    constructor(context) {
        this.context = context;

        // register some JS event handler
        addEventListener(..., () => this.onSomethingHappened());
    }

    onSomethingHappened() {
        // call the named command in the page
        this.context.namedCommands["RefreshGrid"]();
    }
}
```

## Command arguments

Often you may need to pass some arguments to the command. This can be done by using _lambda expression_ in the `NamedCommand`:

```DOTHTML
<dot:NamedCommand Name="CourierSelected" 
                  Command="{staticCommand: (int id) => _root.SelectedCourierId = id}" />
```

```JS
let courierId = ...;
this.context.namedCommands["CourierSelected"](courierId);
```

> DotVVM has no way to verify that the passed arguments are correct. Make sure you pass correct number of arguments and correct types.

## Wait for the result

The calls to `context.namedCommand` methods return a [Promise](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise) which can be used to determine whether the action succeeded or failed:

```JS
async onSomethingHappened() {
    // call the named command in the page
    try {
        await this.context.namedCommands["RefreshGrid"]();
        
        // do something after the data was refreshed

    } catch (err) {
        // handle error states
        dotvvm.patchState({ ErrorMessage: "The grid refresh failed!" });
    }
}
```

## See also

* [JS directive overview](overview)
* [Call JavaScript from DotVVM](call-js-from-dotvvm)
* [Use TypeScript to declare modules](use-typescript-to-declare-modules)
* [Sample app: JS directive](https://github.com/riganti/dotvvm-samples-js-integration)

