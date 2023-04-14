## Sample 3: Reconnect settings

The `MessagingConnection` control has the `TryReconnect` property which specifies whether the control should try to reconnect automatically when the connection is lost.

The `ReconnectTimeout` property specifies the number of seconds after which the reconnect will be performed (default is 30 seconds). 

The `ReconnectAttempts` property can be used to restrict the number of attempts for reconnection (default is 30 attempts, 0 means that the reconnects will be attempted infinitely).