# Creating Custom Postback Handlers

If you want to create a custom [Postback Handler](/docs/tutorials/basics-postback-handlers/{branch}), you need to do three things:

1. Create a **postback handler definition** - a C# class declaring which parameters the postback handler accepts.

2. Write the javascript implementation of the postback handler logic.

3. Register the postback handler to be used from the markup.



## Step 1: Creating the Postback Handler Definition

First, you need to create a class that inherits from the `PostBackHandler` class.

```CSHARP
public class ConfirmPostBackHandler : PostBackHandler
{
    protected override string ClientHandlerName => "confirm";

    protected override Dictionary<string, object> GetHandlerOptions()
    {
        return new Dictionary<string, object>()
        {
            ["message"] = GetValueRaw(MessageProperty)
        };
    }

    public string Message
    {
        get { return (string)GetValue(MessageProperty); }
        set { SetValue(MessageProperty, value); }
    }
    public static readonly DotvvmProperty MessageProperty
        = DotvvmProperty.Register<string, ConfirmPostBackHandler>(c => c.Message, null);	
}
```

This class contains several things:

* The `ClientHandlerName` property specifies the name of the postback handler in the javascript. You'll need the value later in Step 2 to register the handler.

* The `GetHandlerOptions` returns a key-value collection of postback handler parameters. This collection will be serialized and placed in the HTML page. Thanks to this, you can read the values of the postback handler parameters on the client side. If you need to pass a binding in the dictionary, use the `GetValueRaw` method. If you evaluate this expression on the client side, it'll provide the value of the property.

* The `Message` property is a postback handler parameter which you can use in the DOTHTML markup to pass a value to the handler.



## Step 2: Implementing the Postback Handler Logic in Javascript

A postback handler on the client side is just a function which returns an object with one function `execute(callback, sender)`. 
The `callback` is a function without parameters which you can call to make the postback.
The `sender` is a DOM element which represents the control making the postback.

You can register a postback handler to be used on the client side using this code snippet:

```JAVASCRIPT
dotvvm.events.init.subscribe(function () {
    dotvvm.postbackHandlers["confirm"] = function ConfirmPostBackHandler(options) {

    var message = options.message; // you'll get the parameters passed to the handler in the options object

    return {
        execute: function(callback) {
            return new Promise(function (resolve, reject) {
                // do whatever you need and if you need to do the postback, invoke the 'callback()' function
                if (confirm(message)) {
                    callback().then(resolve, reject);
                } else {
                    // signalize that the postback was cancelled
                    reject({type: "handler", handler: this, message: "The postback was aborted by user."});
                }
            });
        },

        // optional settings
        after: ["xxx"],        // you can specify that this handler should be launched after some other handler
        before: ["xxx"]        // you can specify that this handler should be launched before some other handler
    };
    };
});
```

If you need to display a div instead of the default Javascript confirmation, you can do something like this:

```JAVASCRIPT
dotvvm.events.init.subscribe(function () {
    dotvvm.postbackHandlers["customConfirm"] = function ConfirmPostBackHandler(options) {
        this.execute = function(callback) {
            return new Promise(function (resolve, reject) {
                // set the message to the confirmation dialog
                $("#confirm-dialog p.message").text(options.message);

                // display the confirmation dialog
                $("#confirm-dialog").show();

                // do the postback when the OK button is clicked
                // (unbind first because a previous callback could be attached to the event)
                $("#confirm-dialog button.ok-button").unbind("click").on("click", function() { callback().then(resolve, reject) });
            });

        };
    };
});
```



## Step 3: Register the PostBack Handler

The postback handler is registered in the same way as any other user control. You can find more information about the custom 
control registration in the [Code-only Controls](/docs/tutorials/control-development-code-only-controls/{branch}) tutorial.



## Other Usages

Using the postback handler mechanism you can implement many useful things, not only the confirmations.
In some cases you may want to delay the postback for a little while. This may be useful when the user types into the `TextBox` and you need to
show him the search suggestions. It won't make a good user experience to do the postback immediately on each keystroke. 

Instead, you can wait e.g. 500 milliseconds before you issue the postback. It might help the performance of the application as the server won't get
many requests. It can look like this:

```JAVASCRIPT
dotvvm.events.init.subscribe(function () {
    dotvvm.postbackHandlers["delay"] = function ConfirmPostBackHandler(options) {
        this.execute = function(callback, sender) {
            return new Promise(function (resolve, reject) {
                // increment the counter and remember the state
                sender.counter = (sender.counter | 0) + 1;
                var currentCounterState = sender.counter;

                // wait
                window.setTimeout(function () {
                    // if the counter hasn't changed, do the postback
                    if (sender.counter === currentCounterState) {
                        callback().then(resolve, reject);
                    }
                }, options.delay);
            });
        };
    };
});
```

When the `execute` method is called, we increment the counter on the `sender` DOM element and remember its state.
Then we call `window.setTimeout` which executes the function passed as the first argument after the specified delay.

In the function we have to check whether the counter state has changed. If it has, it means that another event was 
triggered and we shouldn't make the postback because we might not have the current data.

If the counter state is the same as it was before the waiting procedure, we just invoke the `callback` and that's it.
