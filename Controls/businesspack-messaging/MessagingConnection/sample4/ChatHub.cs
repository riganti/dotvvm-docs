public class ChatHub : Hub 
{

    public async Task SendMessage(string autor, string text) 
    {
        await this.Clients.AllExcept(Context.ConnectionId)
            .SendAsync("IncomingMessage", new ChatMessageDTO() { Author = author, Text = text });
    }

}