using DotVVM.Framework.ViewModel;
using DotVVM.Controls.Tailwind;
using DotVVM.Controls.Tailwind.Controls;
using System.Collections.Generic;

public class ViewModel : DotvvmViewModelBase
{
    public List<NotificationData> Notifications { get; set; } = new();

    public void ShowSuccess() => Notifications.Add(new() { Type = NotificationType.Success, Text = "Operation completed successfully!" });
    public void ShowError() => Notifications.Add(new() { Type = NotificationType.Danger, Text = "An error occurred. Please try again." });
    public void ShowInfo() => Notifications.Add(new() { Type = NotificationType.Info, Text = "This is an informational message." });
    public void ShowWarning() => Notifications.Add(new() { Type = NotificationType.Warning, Text = "This is a warning notification." });
}
