# Postback handlers

A **postback handler** is a mechanism which allows to intercept [commands](commands) or [static commands](static-commands) on any control, and execute any custom logic before the command or static command is actually performed. A postback handler can decide to cancel the action completely, it can access the response from the server, and it can handle any errors that may occur.

## ConfirmPostBackHandler

Imagine you have a button in your page which removes some data. You will probably want to display a confirmation dialog before the data are actually deleted. This is exactly the use case for postback handlers.

You can attach a postback handler to any control using the `PostBack.Handlers` property. You have to declare it as an inner element, because it is a collection of objects. 

```DOTHTML
<dot:Button Text="Submit" Click="{command: Submit()}">
    <PostBack.Handlers>
        <dot:ConfirmPostBackHandler Message="Do you really want to submit the form?" />
    </PostBack.Handlers>
</dot:Button>
```

The `ConfirmPostBackHandler` is a built-in handler which displays the default JavaScript confirmation box. In the example, this handler is applied to all events on the `Button` control. If the user declines the confirmation, the postback won't be initiated.

## Restrict postback handlers to specific events

If the control has multiple events with command bindings and you need to apply the handler only on a specific event, you can use the `EventName` property. 
If the `EventName` property is not specified, the handler is applied to all events the control can make.

```DOTHTML
<dot:GridView ... SortChanged="{command: SortChanged}">
    <PostBack.Handlers>
        <dot:ConfirmPostBackHandler EventName="SortChanged" 
                                    Message="Do you really want to sort the table?" />
    </PostBack.Handlers>
    ...
</dot:GridView>
```

## Combine postback handlers

You can add multiple handlers in the collection. They will chain in the exact same order they were specified.

## Write custom postback handlers

The postback handler mechanism is extensible. You can find more information in the [custom postback handlers](~/pages/concepts/control-development/custom-postback-handlers) chapter.

## See also

* [Commands](commands)
* [Static commands](static-commands)
* [Concurrency mode](concurrency-mode)
* [Video: Loading animation with postback handler](https://www.youtube.com/watch?v=EeHSMaIEUWA&ab_channel=DotVVM)
* [Video: Modal confirm dialog with postback handlers](https://www.youtube.com/watch?v=m0B5aTQCn1Y&ab_channel=DotVVM)

