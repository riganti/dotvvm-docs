using System.Collections.Generic;
using DotVVM.Controls.Tailwind;
using DotVVM.Controls.Tailwind.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.Notifications.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<NotificationData> PersistentNotifications { get; set; } = new();

        public void ShowReminder()
        {
            PersistentNotifications.Add(new NotificationData { Type = NotificationType.Info, Text = "Remember to review the draft before publishing." });
        }

        public void ShowPersistentWarning()
        {
            PersistentNotifications.Add(new NotificationData { Type = NotificationType.Warning, Text = "Manual confirmation is required for this action." });
        }
    }
}
