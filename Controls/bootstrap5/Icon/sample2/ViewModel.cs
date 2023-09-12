public class ViewModel : DotvvmViewModelBase
{
    public Icons PostbackIcon { get; set; } = Icons.Clipboard;

    public string ButtonText { get; set; } = "Copy to clipboard";
    public void ChangeIcon()
    {
        if (PostbackIcon == Icons.Clipboard)
        {
            PostbackIcon = Icons.Check;
        }

        ButtonText = "Copied!";
    }
}