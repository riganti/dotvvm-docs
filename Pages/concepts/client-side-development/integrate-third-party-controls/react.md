# Integrate React controls

DotVVM supports integrating **React** components in the page. You can pass values from the viewmodel to component _props_ using the [value binding](~/pages/concepts/data-binding/value-binding) and you can trigger [commands](~/pages/concepts/respond-to-user-actions/commands) or [static-commands](~/pages/concepts/respond-to-user-actions/static-commands).

## Using the React component

DotVVM page can use any React component that is exposed in the [JS module](../js-directive/overview) in the `$controls` object.

You can do it like this:

```JAVASCRIPT
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { registerReactControl } from 'dotvvm-jscomponent-react';

// React component
function ReactButton(props: any) {
    return (
        <input
            type="button"
            value={props.text}
            onClick={(_) => props.onClick(s)} />
    );
}

export default (context: any) => new MyModule(context);

class MyModule {
    constructor(context) {
        this.context = context;
    }

    $controls: {
        ReactButton: registerReactControl(ReactButton, { 
            context: this.context, // make `context` available as property in the control
            onClick() { /* default value for props.onClick */ } 
        })
    }
}
```

The page can then reference the JS component like this:

```DOTHTML
...
@js MyModuleWithComponent

<!-- NOTE: property names here are case-sensitive -->
<js:ReactButton text={value: ButtonText} 
                onClick={command: Clicked()} />
```

> Please note that `<js:ReactButton ...>` is just a syntax shortcut for `<dot:JsComponent Name="ReactButton" ...>`.

## Wrap React component for DotVVM

As mentioned in the previous section, you need to expose the React component in the JS module's `$controls` object. DotVVM will instantiate the React control and allow to bind values and commands into its `props` object.

### Install the required npm packages

In order to implement React component in the page, you need to install the `dotvvm-jscomponent-react` package. Also, you'll need the React packages to be installed.

1. If you don't have the `package.json` file in the root of your website, run `npm init` first and follow the instructions. 

2. Run `npm install --save dotvvm-jscomponent-react react react-dom`.

### Install Typescript and rollup to bundle React in the component

This step will allow you to bundle the JS module, the component, and React libraries in one bundle.
While you could use plain view modules without using a JS compiler, it will not work without it if you need to use npm libraries and React JSX syntax.
We show how to setup Rollup with TypeScript, but you can use any JS bundler you like, as long as it can produce ES modules.

1. First, follow the instructions from the [Use TypeScript to declare modules](../js-directive/use-typescript-to-declare-modules) to configure **TypeScript** and **rollup** module bundler.

2. Add the following property in the `tsconfig.json` file:

```
{
    ...
    "jsx": "react",
    ...
}
```

3. Declare your module as a `.tsx` file and register it as an input in your `rollup.config.js` file. You should be able to use "HTML" elements in the module to work with React components.

4. Register the output file as a `ScriptModuleResource` in the `DotvvmStartup.cs` class.

```CSHARP
config.Resources.RegisterScriptModuleFile("react-components-js", "./script/react-components-bundle.js");
```

### Expose the React component

Now, you can expose your component using the `registerReactComponent` function in your JS module:

```JAVASCRIPT
import { registerReactControl } from 'dotvvm-jscomponent-react';
import { YourReactComponent } from ...;

export default (context: any) => ({
    $controls: {
        YourComponent: registerReactControl(YourReactComponent, { 
            context: this.context, // always pass the context to the component
            
            // specify default values for all parameters
            text: "default text",
            onClick() { /* do nothing when onClick is not set */ } 
        }),
        ...
    }
})
```


## Integration API

DotVVM will take any properties set on the `JsComponent` in dothtml and transfer them into props of the React component.

* Primitive types are untouched, no conversions are applied. Note that date types are represented as ISO 8601 strings without timezone.
* Complex objects are also transferred untouched from the `dotvvm.state`. You will not see any knockout observables in the objects.
* Commands and Static Commands are represented as async functions. If the command had parameters, the function accepts them.
* Content properties can also be used in the JsComponent, those are represented as string ids of the corresponding `<template>` element.
    * Use `<KnockoutTemplateReactComponent templateName={props.MyTemplate} />` to use this template in the React DOM.
    * Alternatively, you can inspect the HTML template yourself using `document.getElementById(props.MyTemplate)`

```DOTHTML
<js:TheComponent>
   <MyTemplate>  <span>{{value: SomePropertyInTheViewModel}}</span> </MyTemplate>
</js:TheComponent>
```

The props includes a `setProps` function which can be used to write values back into the value binding.
It will throw an exception if writing into a one-way binding (or into command/template property).
For example, you can use it for writing custom interactive input components in React:

```DOTHTML
<js:FancyEditor text={value: Text} />
```

```JAVASCRIPT
function FancyEditorWrapper({ text, setProps }) {
    function onChange(event) {
        setProps({ text: event.newText })
    }
    return <FancyEditor text={text} onChange={onChange} />
}
```


## See also

* [JS directive](../js-directive/overview)
* [Use TypeScript to declare modules](../js-directive/use-typescript-to-declare-modules)
* [Sample app: React integration](https://github.com/riganti/dotvvm-samples-react-integration)
* [TypeScript declarations](../typescript-declarations)
* [Read and modify viewmodel from JS](../read-and-modify-viewmodel-from-js)

