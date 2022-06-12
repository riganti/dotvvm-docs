# DotVVM JavaScript events

Every DotVVM page fires client-side events which can be subscribed to using the `dotvvm.events` global object. 

The events can tell you when the page gets initialized, when a [command](~/pages/concepts/respond-to-user-actions/commands) or a  [static command](~/pages/concepts/respond-to-user-actions/static-commands) starts or completes, when an error occurs, and so on.

The event can be subscribed like this:

```JS
dotvvm.events.init.subscribe(function () {
    // your code here
});
```

Some functions also pass an event arguments object to the callback.

> There were some breaking changes in DotVVM 3.0 in the event arguments, and in the order of some events that were being raised during the postback. See the **breaking changes** section in the [Release notes](https://github.com/riganti/dotvvm/releases/tag/v3.0) for more details.

## Page lifecycle

The following events represent page-level state changes:

* `init` occurs as soon as the page and DotVVM script is loaded, right before the viewmodel is applied to the page DOM. This event is commonly used to register custom Knockout binding handlers, or other things that are needed before DotVVM starts working with the viewmodel. _If you subscribe to the event after it has been raised, the callback will be fired immediately._ 

* `initCompleted` occurs after the viewmodel has been applied to the page. _If you subscribe to the event after it has been raised, the callback will be fired immediately._ 

* `error` occurs when a postback, static command, or a SPA navigation fails with an unhandled error. You can use this event to display a generic error message dialog, or to perform any global error-handling routine.

* `redirect` occurs when a DotVVM received a request to redirect to a new page. 

* `newState` occurs every time the viewmodel state is changed. See the [Read and modify the viewmodel](read-and-modify-viewmodel-from-js) chapter for more info.

## Postback events

The following sequence of events occur every time a [command](~/pages/concepts/respond-to-user-actions/commands) is triggered.

* `postbackHandlersStarted` occurs before the first [postback handler](~/pages/concepts/respond-to-user-actions/postback-handlers) is triggered
* `postbackHandlersCompleted` occurs after the last postback handler is triggered
* `beforePostback` occurs before the postback is sent to the server
* `postbackResponseReceived` occurs right after the response for a postback is received
* `postbackCommitInvoked` occurs before the changes from the server are applied to the viewmodel
* _the `newState` occurs because the viewmodel was updated_
* `postbackViewModelUpdated` occurs after the changes from the server are applied to the viewmodel
* `afterPostback` occurs after a postback is completed (no matter of the result)

If the postback is rejected due to the validation errors, or from a postback handler, the following event is raised.

* `postbackRejected` occurs whenever the postback processing is canceled

Also, the `error` event is raised in case of any error.

## Static command events

The sequence of events triggered for [static commands](~/pages/concepts/respond-to-user-actions/static-commands) starts like this:

* `postbackHandlersStarted` occurs before the first [postback handler](~/pages/concepts/respond-to-user-actions/postback-handlers) is triggered
* `postbackHandlersCompleted` occurs after the last postback handler is triggered

If the static command contains only local changes, no other events are fired (except for `newState` when the viewmodel is changed). If the static command calls a method on the server, the following events are triggered:

* `staticCommandMethodInvoking` occurs before each server method is invoked
* `staticCommandMethodInvoked` occurs after each server method is invoked (when it returns a successful response)

If the static command calls several methods on the server, each invocation will fire these events.

If calling the method on the server fails, the following event is invoked:

* `staticCommandMethodFailed` - occurs before static command method fails with an error

Also, the `error` event is raised in case of any error.

## SPA navigation

If you are navigating to another page in a [single-page app (SPA)](~/pages/concepts/layout/single-page-applications-spa), the following events are triggered:

* `spaNavigating` occurs before navigation to a new page in SPA mode
* _the `newState` occurs because the viewmodel was updated_
* `spaNavigated` occurs after navigated to a new page in SPA mode

In case the SPA navigation fails (for example because of a network error), the following event is raised:

* `spaNavigationFailed` occurs when the navigation to a new page cannot be performed

Also, the `error` event is raised in case of any error.

## See also

* [Client-side development overview](overview)
* [Read and modify viewmodel from JS](read-and-modify-viewmodel-from-js)
* [Access validation errors from JS](access-validation-errors-from-js)
* [JS directive overview](js-directive/overview)
* [Control development](~/pages/concepts/control-development/overview)
