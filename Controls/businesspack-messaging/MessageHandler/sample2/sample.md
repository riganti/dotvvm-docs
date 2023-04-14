## Sample 1: MessageHandler inside MessagingConnection

Sometimes, the [MessagingConnection](./MessagingConnection) control is defined on some other place (for example in the master page), so it is not possible to place the `MessageHandler` inside.

You can define `ConnectionId` property on both `MessagingConnection` and `MessageHandler` controls which will bind these two controls together.
