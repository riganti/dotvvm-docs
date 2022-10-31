public class ViewModel : DotvvmViewModelBase
{
    public bool Dismissed { get; set; } = false;

    public string Text { get; set; }

    public void AlertDismissed()
    {
        Text = "The alert was dismissed.";
    }

    public void Dismiss()
    {
        Dismissed = !Dismissed;
    }
}
