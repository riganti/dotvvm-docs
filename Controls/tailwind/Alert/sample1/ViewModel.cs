public class AlertsViewModel
{
    public bool AlertDismissed { get; set; } = false;

    public void DismissAlert()
    {
        AlertDismissed = true;
    }

    public void ResetAlert()
    {
        AlertDismissed = false;
    }
}