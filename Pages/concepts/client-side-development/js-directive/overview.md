# JS directive overview

> The JS directive feature is new in DotVVM 3.0. 

> The JS directive functionality is not supported in Internet Explorer 11. 

**JS directive** is a set of features which offer a rich ways to interact between DotVVM controls and JavaScript code. It allows to import a [ES6 module](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Guide/Modules) in the page, and provides mechanisms to [invoke DotVVM commands from JS code](call-dotvvm-from-js), as well as to [call functions in the module](call-js-from-dotvvm) from the DotVVM page. 

The modules may be written in plain JavaScript (using the ES6 syntax), or using [TypeScript](use-typescript-to-declare-modules).

## Declare the module

In order to import the module in the page, the module must provide a factory function as a default export. 

The function will be invoked with one argument (the _module context_ which provides access to the DotVVM commands, and more). 
It is expected to return an object (the _module instance_). 

```JS
// the function takes one parameter (context), and returns an object (the module instance)
export default context => new MyModule(context);

class MyModule {
    
    constructor(context) {
        this.context = context;
        ...
    }

    exportedMethod(arg1, ...) {
        ...
    }
    ...
}
```

For C# developers, it can be useful to use the `class` syntax, but it is not required - the function can return any object:

```JS
// alternative declaration without using the class
export default context => {

    var privateThing = ...

    function privateMethod() {
        ...
    }

    return {
        exportedMethod(arg1, ...) {
            ...
        },
        ...
    }
}
```

## Register the module

The module needs to be registered in `DotvvmStartup.cs` file. Make sure you use `ScriptModuleResource` instead of plain `ScriptResource`:

```CSHARP
config.Resources.Register("dashboard-module", new ScriptModuleResource(new UrlResourceLocation("~/app/dashboard-module.js"))
{
    Dependencies = new [] { ... }
});
```

## Import the module

Finally, the module can be imported into any markup file ([DotHTML page](~/pages/concepts/dothtml-markup/overview), [master page](~/pages/concepts/layout/master-pages), or [markup control](~/pages/concepts/control-development/markup-controls)).

```DOTHTML
@js dashboard-module
```

## Module scope and lifecycle

The module is scoped to the particular markup file. Thus, the module imported to a page cannot interact with the module imported to the master page. Also, if a markup control is used in the page, its module cannot see functions exported by the module imported by the page.

Multiple instances of the same module may exist at the same time - if you use a markup control on a page twice, each instance of the control will have its own instance of its module. Also, if you are using [SPA](~/pages/concepts/layout/single-page-applications-spa), the module instances may exist even if the page which was using them, is not loaded any more. 

### Initialization

The module instance is created after the markup appears in the browser and DotVVM viewmodel is available. 

When the module is imported by a page or a master page, the module instance is created after the `initComplete` [client-side event](../dotvvm-javascript-events) - at that moment, the DOM is fully loaded and the viewmodel is already ready to be used.

When the module is imported by a markup control, it may appear in the page right from the beginning (then it is instantiated immediately after the page / master page modules). However, it may be added later - for example, if the markup control is used in [Repeater](~/controls/builtin/Repeater). In such case, the module instance is created after the DOM of the markup control is added in the page.

If you declare the module as a class, we recommend to pass the context parameter in the constructor and store it in the class instance, because you may need it later.

```JS
export default context => new MyModule(context);

class MyModule {

    constructor(context) {
        // store the context so it can be accessed later
        this.context = context;

        // place the initialization code here
    }

}
```

### Cleanup

If you are using SPA, or you import a module in a markup control, you may need to run some code when the module instance is no longer needed (you are navigating to another page, or the markup control was removed from the page).

The module instance can declare a function called `$dispose` which is be called when the module instance shall be disposed. 

```JS
class MyModule {

    constructor(context) {
        ...
    }

    $dispose() {
        // place the cleanup code here
    }
}
```

## Export functions for DotVVM

To create functions that can be [called from DotVVM binding expressions](call-js-from-dotvvm), just declare them in the module class.

```JS
class MyModule {
    ...

    exportedMethod(arg1, ...) {
        // put your code here
    }
}
```

## Call commands in DotVVM markup

You can use the `context` parameter to [call commands in the page from the module](call-dotvvm-from-js):

```JS
this.context.namedCommands["MyCommand"](arg1, ...);
```

## Export JavaScript components

DotVVM can use JavaScript components exported from the module. Currently, we support [React components](../integrate-third-party-controls/react) via the `registerReactComponent` function, but the integration is generic and more frameworks will be supported in the future.

The controls are exported by exporting the `$controls` object from the module. Each member of the object is treated as a component:

```JS
class MyModule 
{
    ...

    $controls: {
        MyButton: registerReactComponent(...),
        ...
    }
}
```

The controls can be instantiated using `<js:MyButton ...>` in the page.

See [Integrate React components](../integrate-third-party-controls/react.md) chapter for more info.

## See also

* [Call JavaScript from DotVVM](call-js-from-dotvvm)
* [Call DotVVM from JavaScript](call-dotvvm-from-js)
* [Use TypeScript to declare modules](use-typescript-to-declare-modules)
* [Sample app: JS directive](https://github.com/riganti/dotvvm-samples-js-integration)
* [Integrate React components](../integrate-third-party-controls/react)
* [Read and modify viewmodel from JS](../read-and-modify-viewmodel-from-js)
* [DotVVM client-side events](../dotvvm-javascript-events)
* [ES6 modules](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Guide/Modules)
