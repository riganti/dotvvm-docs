# DotVVM JavaScript events

Every DotVVM page includes `Dotvvm.js` which defines `dotvvm` in the global scope. This object can be used to access viewmodel and react to various page events.

DotVVM publishes the following events:

* `init` - occurs as soon as the page and DotVVM scripts is loaded and the viewmodel is applied to the page DOM
* `beforePostback` - occurs before every postback
* `afterPostback` - occurs after a postback is completed successfully
* `error` - occurs when a postback fails with an error
* `spaNavigating` - occurs before navigation to a new page in SPA mode
* `spaNavigated` - occurs after navigated to a new page in SPA mode
* `redirect` - occurs when a postback results in a redirect to a new page

The following events are new in **DotVVM 2.0**:

* `postbackHandlersStarted` - occurs before the first postback handler is triggered
* `postbackHandlersCompleted` - occurs after last postback handler is triggered
* `postbackResponseReceived` - occurs right after the response for a postback is received
* `postbackCommitInvoked` - occurs before the postback response is applied to the viewmodel
* `postbackViewModelUpdated` - occurs after the postback response is applied to the viewmodel
* `postbackRejected` - occurs whenever the postback processing is cancelled

* `staticCommandMethodInvoking` - occurs before static command method is invoked
* `staticCommandMethodInvoked` - occurs after static command method is invoked and returns a successful response
* `staticCommandMethodFailed` - occurs before static command method fails with an error

DotVVM events contain `subscribe` and `unsubscribe` methods:

```JS
var handler = function (args) {
    // handler code
};

dotvvm.events.beforePostback.subscribe(handler);
dotvvm.events.beforePostback.unsubscribe(handler);
```

The `init` event has a special behavior - when you subscribe to it after the event occured, the handler will be invoked immediately.