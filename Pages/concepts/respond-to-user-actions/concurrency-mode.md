# Concurrency mode

Any control in DotVVM may specify the `PostBack.Concurrency` property which defines the behavior of concurrent [commands](commands) or [static commands](static-commands). This property changes the behavior for the control itself, and for all its descendants.

The property can have the following values: 

* `Default`
* `Deny`
* `Queue`

## Default mode

```DOTHTML
<dot:Button Text="Test" Click="{command: Test()}" 
            PostBack.Concurrency="Default" />
``` 

For both [commands](commands) and [static commands](static-commands), starting a new postback is allowed while another postback is waiting for response. 

For __command binding__, the changes in the viewmodel received from the server are applied only if no other postback was started after the current one. When DotVVM receives a response from the server and another postback has already started, the response will not be applied to the page because the result may not be up-to-date. This is also known as the _the last postback wins_.

For __static command bindings__, all responses are applied to the viewmodel in the order they were received from the server. If two different static commands update the same property, again - _the last request wins_.

## Deny mode

```DOTHTML
<dot:Button Text="Save" Click="{command: Save()}"
            PostBack.Concurrency="Deny" />
``` 

The `Deny` mode will not start a new postback when another one is waiting for response. 

This is a good setting for double-postback prevention, and should be used on all operations which insert data (submit buttons and similar).

## Queue mode

```DOTHTML
<dot:Button Text="Refresh" Click="{command: Refresh()}"
            PostBack.Concurrency="Queue" />
``` 

This setting will add postbacks in a queue, and dispatch them immediately after the previous postbacks are completed. The order of postbacks is preserved.

This is a good setting for low-priority postbacks, long running operations, or periodic tasks (refreshing data every 15 seconds) which might otherwise interfere with actions made by the user at the same time.

## Concurrency queues

Sometimes, you might need several independent groups of controls which may be used simultaneously. For example, you may need two separate postback queues for some kinds of actions.

You can set `PostBack.ConcurrencyQueue` property to any string value which will identify the queue. The property is applied to the control itself and all its descendants. 

Controls with the same queue identifier will be treated as one group - the `Deny` or `Queue` options will be applied on them, however control with different queue name will be independent on these controls and their postbacks.

The following code snippet contains two buttons that refresh two grids - if the first button is pressed twice, only the first postback will be started. But if you click the first button, clicking the second button will initiate postback even if the first postback wasn't completed. 

```DOTHTML
<div PostBack.Concurrency="Deny">
    <dot:Button Text="Refresh Grid 1" Click="{staticCommand: Grid1 = ...}" 
                PostBack.ConcurrencyQueue="Grid1" />
    <dot:Button Text="Refresh Grid 2" Click="{staticCommand: Grid2 = ...}" 
                PostBack.ConcurrencyQueue="Grid2" />
</div>
```

> Using multiple concurrency queues is quite rare, and makes sense only if you use [static commands](static-commands). Since the commands post the entire viewmodel to the server, and the server can change any property in the viewmodel, multiple concurrent commands will interfere with each other in may cases.

## See also

* [Commands](commands)
* [Optimize command performance](optimize-command-performance)
* [Static commands](static-commands)
* [Static command services](static-command-services)
* [JS directives](~/pages/concepts/client-side-development/js-directive/overview)

