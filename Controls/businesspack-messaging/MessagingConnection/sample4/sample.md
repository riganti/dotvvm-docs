## Sample 4: Send messages to the hub

You can call hub methods on the server by using the `_messaging` variable in the [static commands](~/pages/concepts/respond-to-user-actions/static-commands) bindings.

In order to reference the connection which you want to send the message, you need to set the `ConnectionId` of the `MessagingConnection` control to some value. Then, you can use this id in the binding.