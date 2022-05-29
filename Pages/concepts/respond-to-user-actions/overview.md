# Responding to user actions overview

The users of your app can interact with [controls](~/pages/concepts/dothtml-markup/builtin-controls) in the page. 

The most common action is clicking on a [Button](~/controls/builtin/Button) or [LinkButton](~/controls/builtin/LinkButton) control. However, there are other kinds of events which the app will want to handle, like changing the value in the [TextBox](~/controls/builtin/TextBox) or selecting an item in a [ComboBox](~/controls/builtin/ComboBox).

In **DotVVM**, there are two main ways of handling such events:

* [Commands](commands): If you use command, the entire viewmodel is sent to the server and the command method will be invoked. If the method changes the state of the viewmodel, these changes are sent back to the client, and the page is updated accordingly.

# [CommandSample.dothtml](#tab/command-view)

```DOTHTML
<dot:ComboBox SelectedValue="{value: Location}" ... />

<p>Current temperature: {{value: Weather.Temperature}} °C</p>
<p>Conditions: {{value: Weather.Conditions}}</p>

<dot:Button Text="Refresh weather" Click="{command: Refresh()}" />
```

# [CommandSampleViewModel.cs](#tab/command-viewmodel)

```CSHARP
public class CommandSampleViewModel 
{
    public string Location { get; set; }

    public WeatherInfo Weather { get; set; }

    public async Task Refresh() 
    {
        // you can access any information from the viewmodel
        Weather = _weatherService.GetWeather(Location);
    }
}
```

***

* [Static commands](static-commands): Static commands can make local changes to the viewmodel (assign values to properties, call methods that can be translated to JavaScript by DotVVM, or call methods on the server and use the result to update some viewmodel properties). 

# [CommandSample.dothtml](#tab/staticcommand-view)

```DOTHTML
@service _weatherService = MyApp.Services.WeatherService

<dot:ComboBox SelectedValue="{value: Location}" ... />

<p>Current temperature: {{value: Weather.Temperature}} °C</p>
<p>Conditions: {{value: Weather.Conditions}}</p>

<!-- You need to pass all arguments to the called method - the viewmodel is not sent to the server -->
<dot:Button Text="Increment" Click="{staticCommand: Weather = _weatherService.GetWeather(Location)}" />
```

# [CommandSampleViewModel.cs](#tab/staticcommand-viewmodel)

```CSHARP
public class CommandSampleViewModel 
{
    public string Location { get; set; }

    public WeatherInfo Weather { get; set; }
}
```

***

DotVVM handles the client-to-server communication automatically, and uses the browser `fetch` API. There are no full page reloads or classic form POSTs in DotVVM - both commands and static commands communicate via AJAX. This is a difference between DotVVM and [Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-5.0&tabs=visual-studio) which also offer MVVM way of development. 

## When to use command and static command

Commands and static commands are completely different concepts which both have their advantages and disadvantages. Some users prefer one of these concepts, others like to combine and use them together.

### Commands

[Commands](commands) treat the viewmodel as one "atomic" piece of information. When you invoke a command, the viewmodel is sent to the server where the command method is invoked. The changes made to the viewmodel are then sent back to the browser, and applied to the page. 

Commands are present in the framework from its first version, and they are popular mostly because they are simple to use. Also, commands offer plenty of features like [validation](~/pages/concepts/validation/overview), [action filters](~/pages/concepts/viewmodels/filters/overview), which are not available when you use static commands.

However, commands come with some overhead:

* In most cases, it isn't necessary to transmit the entire viewmodel to the server - the invoked method typically needs just a few properties. This can be eliminated by using [server-side viewmodel cache](~/pages/concepts/viewmodels/server-side-viewmodel-cache) or by using the [Bind attribute](~/pages/concepts/viewmodels/binding-direction) which help DotVVM to reduce the amounts of data transmitted.

* Because the changes in the viewmodel may affect the hierarchy of the controls in the page (like adding rows to a [GridView](~/controls/builtin/GridView)), DotVVM re-builds the entire control tree before the command method is executed. It is also possible to re-render some controls and send the new HTML to the browser. However, you don't need any of these things in many cases, and because DotVVM performs these operations for every command, the commands are slower.

In reasonably-sized pages (one `GridView` with up to 100 rows, forms with ~50 fields), the overhead is not significant. That's why many developers prefer commands as their default choice, and use static commands only on places where the performance of commands would not be sufficient.

See the [commands](commands) chapter for more info.

### Static commands

In contract to the commands, static commands are evaluated on the client-side. They can still invoke methods on the server too, but they need to pass all the information the methods will need, because the viewmodel is not transmitted to the server. 

Static commands can perform the following kinds of operations:

* **Make changes to the viewmodel** - e. g. `{staticCommand: MyProperty = true}`. This can be done solely in the browser and doesn't require any communication with the server.

* **Call methods on the server and assign the result to a viewmodel property** - e. g. `{staticCommand: MyProperty = SomeMethodOnTheServer(OtherProperty, ...)}`. The method can be a static method, or an instance method declared in a [static command service](static-command-services). 

* **Invoke JavaScript methods** - e. g. `{staticCommand: MyProperty = _js.Invoke<string>("someJsMethod", OtherProperty, ...)}`, see [JS directives](~/pages/concepts/client-side-development/js-directive/overview).

* **Invoke REST API bindings** - e. g. `{staticCommand: _myApi.Delete(CustomerId)}`, see [REST API bindings](rest-api-bindings/overview).

Expression in the static command binding is translated to the JavaScript. If the expression contains a method that is on the server, DotVVM will make a request and call the method. You can pass any arguments to the method, and you can use the result of the method to update some viewmodel properties. 

Static commands are much more light-weight - they don't need to transmit the viewmodel to the server, they don't build the control tree, or call the [viewmodel lifecycle events](~/pages/concepts/viewmodels/overview). 

The disadvantage of static commands is the fact that you need to pass all the data they need as method arguments, and you get only one result value. If you need to perform multiple actions which need many inputs from the viewmodel, or make complex changes to the viewmodel, it is not comfortable to do with static commands - the binding expressions will be long and difficult to read.

```DOTHTML
<!-- Example of a method which does many things, needs a lot of inputs, and needs to change a lot of things in the viewmodel -->
<dot:Button Click="{staticCommand: var result = WeatherService.GetWeatherAndSaveUserPreferences(Location, DateInterval, DisplaySettings);
                                   Weather = result.Weather;
                                   LastUpdated = result.LastUpdated;
                                   Alerts = result.Alerts;
                                   UserPreferencesDialog.IsDisplayed = false}" />
```

Additionally, there are some missing features in static commands, like [validation](~/pages/concepts/validation/overview), [action filters](~/pages/concepts/viewmodels/filters/overview), and others. 

See the [static commands](static-commands) chapter for more info.

## See also

* [Commands](commands)
* [Optimize command performance](optimize-command-performance)
* [Static commands](static-commands)
* [Static command services](static-command-services)
* [JS directives](~/pages/concepts/client-side-development/js-directive/overview)