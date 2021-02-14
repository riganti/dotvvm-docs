# Postback Handlers

Imagine you have a button in your page which removes some data. In many cases, you need to display a confirmation dialog before the data are deleted.
This is a situation when you shall use a feature called **postback handler**.

In general, the postback handler is a mechanism which allows to intercept postbacks on any control and execute your own custom logic before the postback
is actually sent to the server. It is even possible to cancel the postback entirely.

You can attach a postback handler to any control using the `PostBack.Handlers` property. You have to declare it as an inner element, because it is a collection of objects. 

```DOTHTML
<dot:Button Text="Submit" Click="{command: Submit()}">
    <PostBack.Handlers>
        <dot:ConfirmPostBackHandler Message="Do you really want to submit the form?" />
    </PostBack.Handlers>
</dot:Button>
```

The `ConfirmPostBackHandler` is a built-in handler which displays the default JavaScript confirmation box. In the example, this handler is applied to all events on the `Button` control. If the user declines the confirmation, the postback won't be initiated.

If the control has multiple events with command bindings and you need to apply the handler only on a specific event, you can use the `EventName` property. 
If the `EventName` property is not specified, the handler is applied to all events the control can make.

You can add multiple handlers in the collection. They will chain in the exact same order they were specified.

The default `ConfirmPostBackHandler` provides the basic confirmation functionality using the JavaScript `confirm` function, but it doesn't provide
nice user experience.

However, the postback handler mechanism is extensible and it is not difficult to write your own confirmation dialog logic. You can find more information about that in the [Custom Postback Handlers](/docs/tutorials/control-development-creating-custom-postback-handlers/{branch}) chapter.

> There were some breaking changes in the implementation of postback handlers in DotVVM 2.0. If you have implemented your own postback handlers, they may require some updates. See [Upgrading to DotVVM 2.0](/docs/tutorials/how-to-start-upgrade-to-2-0/{branch}#postback-handlers).
