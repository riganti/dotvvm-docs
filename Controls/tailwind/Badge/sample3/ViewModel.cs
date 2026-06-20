using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public int NotificationCount { get; set; } = 5;

    public void IncrementNotifications()
    {
        NotificationCount++;
    }

    public void DecrementNotifications()
    {
        if (NotificationCount > 0) NotificationCount--;
    }

    public void ResetNotifications()
    {
        NotificationCount = 0;
    }
}

