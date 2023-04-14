public class SampleViewModel : DotvvmViewModelBase
{
    public List<ChatMessageDTO> Messages { get; set; } = new();
}

public class ChatMessageDTO 
{
    public string Text { get; set; }
}