## Sample 2: Connection events

To handle various connection lifecycle events, you can bind to the `Connected` and `Disconnected` events, and also to `Reconnecting` and `Reconnected` events.

The `Error` event will be triggered whenever there is an error (connecting to the hub, sending a message). Since any error means that the connection is broken, this event is typically accompanied by one of the other events. 

> It is recommended to use [static commands](~/pages/concepts/respond-to-user-actions/static-commands) since the events may be fired in a quick sequence.