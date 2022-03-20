# Client-side development overview

Most of the time, you will be spending your time writing a server-side code in C# [viewmodels](~/pages/concepts/viewmodels/overview). Thanks to the MVVM experience, it is possible to implement interactive pages with client-side without writing any JavaScript code. 

However, in order to create more advanced experiences for the users, or if you want to integrate third-party libraries or components into DotVVM pages, you will need to work with the client-side part of DotVVM. 

> If you plan to use TypeScript, check out the [TypeScript declarations](typescript-declarations) chapter on how to obtain the definition files and get IntelliSense and type checking for interactions with DotVVM client-side API.

## Value binding and static commands

Even without digging into JavaScript, there is a way to invoke actions in the browser without the need to communicate with the server - the [static commands](~/pages/concepts/respond-to-user-actions/static-commands). They can be used to perform simple client-side manipulations to the viewmodel:

```DOTHTML
<dot:Button Text="Show modal" 
            Click="{staticCommand: MyModalDialog.IsDisplayed = true}" />
```

Also, thanks to the translation of [value binding](~/pages/concepts/data-binding/value-binding) and MVVM, you can use complex expressions to manipulate with the user interface components:

```DOTHTML
<!-- This button will be visible only when at least one category is selected -->
<dot:Button Text="Delete selected categories"
            Visible="{value: SelectedCategories.Count > 0}" 
            Click="..." />

<!-- Checkboxes which are checked, put theirs CheckedValue into the SelectedCategories collection -->
<dot:Repeater DataSource="{value: AllCategories}">
    <dot:CheckBox Text="{value: CategoryName}" 
                  CheckedValue="{value: Id}"
                  CheckedItems="{value: _parent.SelectedCategories}" />
</dot:Repeater>
```

## DotVVM JavaScript API

If you need to do something serious on the client, e. g. integrate a third-party JavaScript component in the page, DotVVM offers a public client-side API that can be used to [access the viewmodel](read-and-modify-viewmodel-from-js), or [subscribe to various DotVVM events](dotvvm-javascript-events).

The following code snippet shows how to read a value from the viewmodel, and run JavaScript code after the DotVVM is initialized:

```JS
// run code after DotVVM is initialized
dotvvm.events.initCompleted.subscribe(function () {
    var map = new google.maps.Map(document.getElementById('map'),
    {
        center: {
            lat: dotvvm.state.Latitude,  // read a value from the viewmodel
            lng: dotvvm.state.Longitude
        },
        zoom: 8
    });
});
```

## JS directive

The previous approach is great for simple, one-time interactions. If you need something more sophisticated, you can organize your JavaScript code as an ES module, and allow two-way interaction with DotVVM code.

You can register a module in the page using the `@js` directive:

```DOTHTML
@js MapsModule
```

The module looks like this:

```JS
export default context => new Page(context);
class Page {
    constructor(context) {
        this.map = new google.maps.Map(...);
        this.map.addListener("zoom_changed", () => {
            // you can call a NamedCommand in DotVVM page
            context.namedCommands["ZoomChanged"](this.map.getZoom());
        });
    }

    // you can expose functions which can be called from DotVVM using _js.Invoke
    setPosition(latitude, longitude) {
        this.map.setCenter({ lat: latitude, lng: longitude });
    }
```

The module exposes an object which can:
* call commands defined using the `NamedCommand` control in the DotVVM page
* be called from static commands using `_js.Invoke("functionName", args...)`

```DOTHTML
<!-- This can be called from the JS module using 
     context.namedCommands["ZoomChanged"](args...) -->
<dot:NamedCommand Name="ZoomChanged" 
                  Command="{staticCommand: (int zoom) => ...}" />

<!-- You can call functions in the module from static commands-->
<dot:Button Text="Set position"
            Click="{staticCommand: _js.Invoke("setPosition", Latitude, Longitude)"} />
```

See the [JS directive](js-directive/overview) chapter for more info.

## Integrating React controls

DotVVM supports integrating components from other client-side frameworks.

Currently, there is a wrapper for **React components** - they can be hosted in DotVVM pages, access the viewmodel and trigger commands.

The support for integrating components from other frameworks is generic so we expect the support for other popular client-side frameworks to come in the next versions of DotVVM. 

See the [Integrate React controls](integrate-third-party-controls/react) chapter for more info.

> This feature was added in DotVVM 4.0.

## Other options

There are even more advanced options which may be helpful when developing custom controls or extending DotVVM. See the [Control development](~/pages/concepts/control-development/overview) section for more info.

## See also

* [TypeScript declarations](typescript-declarations)
* [JS directive](js-directive/overview)
* [Integrate React components](integrate-third-party-controls/react)
* [Read and modify viewmodel from JS](read-and-modify-viewmodel-from-js)
* [DotVVM JavaScript events](dotvvm-javascript-events)
* [Access validation errors from JS](access-validation-errors-from-js)
* [Control development](~/pages/concepts/control-development/overview)