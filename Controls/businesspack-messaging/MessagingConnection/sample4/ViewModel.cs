public class SampleViewModel : DotvvmViewModelBase
{
    public List<ChatMessageDTO> Messages { get; set; } = new();

    public string AutorName { get; set; } = "user";

    public string NewMessage { get; set; }
}

public class ChatMessageDTO 
{
    public string Text { get; set; }
    public string Author { get; set; }
}