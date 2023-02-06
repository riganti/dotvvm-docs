public class ViewModel : DotvvmViewModelBase
{
    public Icons PostbackIcon { get; set; } = Icons.Clipboard;

    public string ButtonText { get; set; } = "Copy to clipboard";
    public void ChangeIcon()
    {
        PostbackIcon = PostbackIcon == Icons.Clipboard ? Icons.Check : Icons.Clipboard;
        ButtonText = "Copied!";
    }
}