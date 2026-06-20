using System.Collections.Generic;
using DotVVM.Controls.Tailwind;
using DotVVM.Controls.Tailwind.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.Notifications.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<NotificationData> Notifications { get; set; } = new();

        public void ShowSuccess()
        {
            Notifications.Add(new NotificationData { Type = NotificationType.Success, Text = "The record was saved." });
        }

        public void ShowError()
        {
            Notifications.Add(new NotificationData { Type = NotificationType.Danger, Text = "The record could not be saved." });
        }

        public void ShowInfo()
        {
            Notifications.Add(new NotificationData { Type = NotificationType.Info, Text = "Background synchronization has started." });
        }

        public void ShowWarning()
        {
            Notifications.Add(new NotificationData { Type = NotificationType.Warning, Text = "Your session will expire soon." });
        }
    }
}
