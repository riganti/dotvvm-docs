# Adding interactivity using custom Knockout binding handlers

In the previous tutorial, we have written a simple [TextBox](/docs/controls/builtin/TextBox/{branch}) control. But today, we have many controls that include a javascript part.
Let's write a DatePicker control using [this Bootstrap DatePicker plugin](http://www.eyecon.ro/bootstrap-datepicker).

To use the plugin, you need to do several things:

1. Link the script and CSS file in the page.

2. Render the `<input type="text" />` control.

3. Run `$('selector').datepicker()`.

4. Watch the "changeDate" event and update the viewmodel. The plugin doesn't trigger the change event on the input so we have to help Knockout to notice the value has changed.


## The First Rule: Avoid Javascript in the Page

If you don't know Knockout JS well, read [their documentation](http://knockoutjs.com/documentation/introduction.html). You'll get better understanding how *DotVVM* 
works inside - remember that DotVVM wraps Knockout JS.

The first solution that comes to anyone's mind is to autogenerate some ID to the input and render a script element right after the input to make things work. 
Well, it's a really bad idea. If you use this control multiple times on the page, it would generate many script elements and it could cause many complications when these
controls are generated dynamically in the page (e.g. in the [Repeater](/docs/controls/builtin/Repeater/{branch}) control).

But Knockout JS supports custom binding handlers. It would be just awesome, if we could render something like this, and extend Knockout to do the javascript stuff 
which our DatePicker needs:

```DOTHTML
<input type="text" data-bind="myDatePicker: Expression" />
```

The good news is that we can do it almost always. The server part of the control will be very easy to implement. The magic will be done in the Knockout binding handler. 

> Always try to solve the problem by creating a custom Knockout binding handler.



## Making the binding

The Knockout binding handler has two phases - `init` and `update`. Basically, these are two functions.

The `init` is called when the binding is applied on the control. It will be called for a new controls that appeared in the page, even after the postback or in the GridView etc.

The `update` is called once right after `init`, and then every time the appropriate value in the viewmodel is changed.

So what we have to do?

1. In the `init` phase we should definitely call `$(element).datepicker();`. This will turn the input into the DatePicker.

2. In the `init` phase we should also subscribe the control's **dateChanged** event. When the user changes the date in the control, we have to update the viewmodel.

3. In the `update` phase we should read the current value from the viewmodel and display it in the DatePicker.

Let's write it:

```CSHARP
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
```

## Resources

The last thing we have to do is to bundle the script so it is included with the control.

First, we have to register a resource to the `DotvvmConfiguration`. Set the **Build Action** of the javascript file
to **Embedded Resource** and then add this snippet to **Startup.cs**:

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
	Dependencies = new[] { "knockout", "myDatePicker-js", "myDatePicker-css" }
});
```

Be careful about the dependencies of the resources. The plugin JS file expects that jQuery and Bootstrap will already
be loaded - so the dependencies of the plugin JS file has to contain bootstrap and jquery.

The knockout handler file requires also Knockout JS, so it is also in the dependencies.

If you are not sure about the name of the embedded resource, open your project assembly in Reflector or a similar tool.
You'll see the names of embedded resources in the assembly.

The last thing is to tell our DatePicker control that it requires the "myDatePicker" resource. Add this line to the OnPreRenderComplete method:

```CSHARP
protected override void OnPreRenderComplete(IDotvvmRequestContext context)
{
    context.ResourceManager.AddRequiredResource("myDatePicker");
    base.OnPreRenderComplete(context);
}
```

> Now you should be able to wrap any control to be used in DotVVM.





