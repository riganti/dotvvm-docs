# PostBack Concurrency Mode

> This feature is new in **DotVVM 2.0**. 

Any control in DotVVM may specify the `PostBack.Concurrency` property. This property changes behavior for concurrent postbacks for the control and all its descendants.

The property can have the following values: 

* `Default`
* `Deny`
* `Queue`

## Default behavior

```DOTHTML
<dot:Button Text="Test" Click="{command: Test()}" PostBack.Concurrency="Default" />
``` 

This setting keeps the default behavior of postbacks. For both [Command Binding](/docs/tutorials/basics-command-binidng/{branch}) and [Static Command Binding](/docs/tutorials/basics-static-command-binidng/{branch}), starting a new postback is allowed while another postback is waiting for response. 

For __command binding__, the changes in the viewmodel received from the server are applied only if no other postback was started after the current one. When DotVVM receives a response from the server and another postback has already started, the response will not be applied to the page because the result may not be up-to-date. 

For __static command bindings__, all responses are applied to the viewmodel in the order they were received from the server.

This behavior works for many scenarios and is the only strategy available in **DotVVM 1.x**.

## Deny: One postback at a time

```DOTHTML
<dot:Button Text="Save" Click="{command: Save()}" PostBack.Concurrency="Deny" />
``` 

This setting will not initiate a postback when another one is waiting for response. 

This is a good setting for double-postback prevention and should be used on all operations which insert data (save buttons and so on).

## Queue: Wait until other postbacks end

```DOTHTML
<dot:Button Text="Refresh" Click="{command: Refresh()}" PostBack.Concurrency="Queue" />
``` 

This setting will add postbacks in a queue and dispatch them immediately after the other postbacks are completed. It preserves the order of postbacks.

This is a good setting for low-priority postbacks, long running operations or periodic tasks (refreshing every 15 seconds) which might otherwise interfere with actions made by the user at the same time.

## Concurrency Queues

You may need several independent groups of controls which may be used simultaneously. For example, you may need two separate postback queues for some kinds of actions.

You can set `PostBack.ConcurrencyQueue` property to any string value which will identify the queue. The property is applied to the control itself and all its descendants. 

Controls with the same queue identifier will be treated as one group - the `Deny` or `Queue` options will be applied on them, however control with different queue name will be independent on these controls and their postbacks.

The following code snippet contains two buttons that refresh two grids - if the first button is pressed twice, only the first postback will be started. But if you click the first button, clicking the second button will initiate postback even if the first postback wasn't completed. 

```DOTHTML
<div PostBack.Concurrency="Deny">
    <dot:Button Text="Refresh Grid 1" Click="{staticCommand: Grid1 = ...}" PostBack.ConcurrencyQueue="Grid1" />
    <dot:Button Text="Refresh Grid 2" Click="{staticCommand: Grid2 = ...}" PostBack.ConcurrencyQueue="Grid2" />
</div>
```

