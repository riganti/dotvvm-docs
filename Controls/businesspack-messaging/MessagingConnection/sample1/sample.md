## Sample 1: Basic connection

To establish a connection to a SignalR hub, place the `MessagingConnection` control in the page and specify the URL of the hub.

You can use the `IsConnected` property to detect whether the connection is active or not.

You can define one or more [MessageHandler](./MessageHandler) controls to run commands when  methods are called on the hub.

The `MessageHandler` controls can be defined:

* Inside this `MessagingConnection` control. See [MessageHandler - Sample 1](./MessageHandler#sample1)] for more info.

* Outside this control. In this case, you need to set the `ConnectionId` property on both `MessagingConnection` and `MessageHandler` controls to the same value. See [MessageHandler - Sample 2](./MessageHandler#sample2)] for more info.
