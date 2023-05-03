## Sample 1: MessageHandler inside MessagingConnection

You can define handlers for individual methods inside the [MessagingConnection](./MessagingConnection) control. 

Use the `MethodName` property to specify the name of the method on the client hub   (case-sensitive).

The `Command` property defines the command which will be triggered when the client method is called from the server. 

> Make sure the command is a lambda function with correct argument types. DotVVM has no way of checking the type safety on this place. 

To send message in the hub, you can use the following code snippet (in the server-side code):

```CSHARP
public async Task SendMessage(IHubContext<ChatHub> chatHub, ChatMessageDTO message) 
{
    await chatHub.Clients.All.SendAsync("IncomingMessage", message);
}
```