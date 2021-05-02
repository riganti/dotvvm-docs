# Adding interactivity using Knockout binding handlers

In the previous tutorial, we have written a simple version of the [TextBox](~/controls/builtin/TextBox) control. However, we have many controls that require some JavaScript logic on the client.

As an example, let's write a date picking control using [this Bootstrap DatePicker plugin](http://www.eyecon.ro/bootstrap-datepicker).

To use the plugin, you need to do several things:

1. Link the script and CSS file in the page.

2. Render the `<input type="text" />` control.

3. Run `$('selector').datepicker()`.

4. Watch the `changeDate` event and update the viewmodel when the value is changed. The plugin doesn't trigger the change event on the input, so we have to help Knockout to notice the value has changed.

## Avoid inline scripts in the page

If you don't know Knockout JS well, read [their documentation](http://knockoutjs.com/documentation/introduction.html). You'll get better understanding how DotVVM works inside - it uses Knockout JS to provide the MVVM experience in the browser.

The first solution that comes to anyone's mind is to autogenerate some ID to the input, and render a script element right after the input to make things work. 
Well, it's a really bad idea. If you use this control multiple times on the page, it would generate many script elements, and it could cause many complications when these controls are generated dynamically in the page (e.g. in the [Repeater](~/controls/builtin/Repeater) control).

But Knockout JS supports creating [custom binding handlers](https://knockoutjs.com/documentation/custom-bindings.html). It would be just awesome, if we could render something like this, and extend Knockout to do the javascript stuff which our date picker needs:

```DOTHTML
<input type="text" data-bind="myDatePicker: Expression" />
```

The good news is that we can do it almost always. The server part of the control will be very easy to implement. The magic will be done in the Knockout binding handler. 

## Implement the binding handler

The Knockout binding handler has two phases - `init` and `update`. Basically, these are two functions.

The `init` is called when the binding is applied on the control. It will be called as soon as the control appears in the page - even after postback when new items are added to a data source collection of a `Repeater`.

The `update` is called once right after `init`, and then every time the value in the viewmodel is changed.

So what we have to do?

1. In the `init` phase, we should definitely call `$(element).datepicker();`. This will turn our input into the DatePicker.

2. In the `init` phase, we should also subscribe to the control's `dateChanged` event. When the user changes the date in the control, we have to update the viewmodel property.

3. In the `update` phase, we should read the new value from the viewmodel, and put it in the DatePicker. This method will be called when the date was changed externally, e. g. during the postback on the server.

The binding handler can look like this:

```CSHARP
dotvvm.events.init.subscribe(function () {
    ko.bindingHandlers["myDatePicker"] = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            $(element)
                .datepicker({ format: "yyyy/mm/dd" })
                .on('changeDate', function (e) {
                    // this will retrieve the property from the viewmodel
                    var prop = valueAccessor();        
                    
                    // if the property is ko.observable, we'll set the value
                    if (ko.isObservable(prop)) {    
                        prop(e.date);
                    }
            })        
            .on('change', function (e) {            
                if (!$(element).val()) {
                    // if someone deletes the value from the textbox, set null to the viewmodel property
                    var prop = valueAccessor();
                    if (ko.isObservable(prop)) {                    
                        prop(null);
                    }
                }
            });
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            // get the value from the viewmodel
            var value = ko.unwrap(valueAccessor());
            
            // if the value in viewmodel is string, convert it to Date
            if (value && typeof value === "string") {
                value = new Date(value);
            }
            
            // set the value to the control
            if (value) {
                $(element).datepicker("setValue", value);
            }
        }
    };
});
```

## JavaScript and CSS resources

The last thing we have to do is to bundle the script so it is included with the control. Whenever the control is used on the page, DotVVM will add the scripts and CSS automatically.

First, we have to [register a resource](~/pages/concepts/script-and-style-resources/overview) to the `DotvvmConfiguration`. Set the __Build Action_ of the JavaScript and CSS files to _Embedded Resource_, and then add this snippet to `Startup.cs`:

```CSHARP
// plugin css file
configuration.Resources.Register("myDatePicker-css", new StylesheetResource()
{
	EmbeddedResourceAssembly = "",	// use your project name
	Url = "ProjectName.Folder.Folder2.datepicker.css"	// full name of the embedded resource 
});

// plugin javascript file
configuration.Resources.Register("myDatePicker-js", new ScriptResource()
{
	EmbeddedResourceAssembly = "",	// use your project name
	Url = "ProjectName.Folder.Folder2.datepicker.js",	// full name of the embedded resource 
	Dependencies = new[] { "jquery", "bootstrap" }
});

// custom knockout handler file
configuration.Resources.Register("myDatePicker", new ScriptResource()
{
	EmbeddedResourceAssembly = "",	// use your project name
	Url = "ProjectName.Folder.Folder2.myDatePicker.js",	// full name of the embedded resource 
	Dependencies = new[] { "dotvvm", "myDatePicker-js", "myDatePicker-css" }
});
```

Be careful about the dependencies of the resources. The plugin JS file expects that jQuery and Bootstrap will already be loaded - so the dependencies of the plugin JS file has to contain Bootstrap and jQuery. The knockout handler file requires also DotVVM global object, so it is also in the dependencies.

If you are not sure about the name of the embedded resource, open your project assembly in Reflector, ILSpy, or a similar tool. You'll see the names of embedded resources in the assembly.

The last thing is to tell our DatePicker control that it requires the "myDatePicker" resource. Add this line to the `OnPreRender` method:

```CSHARP
protected override void OnPreRender(IDotvvmRequestContext context)
{
    context.ResourceManager.AddRequiredResource("myDatePicker");
    base.OnPreRender(context);
}
```

Check out our [DotVVM Contrib](https://github.com/riganti/dotvvm-contrib) repo for more examples of writing custom Knockout binding handlers.

## See also

* [Control development overview](overview)
* [Markup controls](markup-controls)
* [Code-only controls](code-only-controls)
* [Binding system extensibility](binding-extensibility)
* [Binding extension parameters](binding-extension-parameters)
* [Custom JavaScript translators](custom-javascript-translators)






