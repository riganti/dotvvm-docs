using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.List.sample4
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<NotificationItem> Notifications { get; set; } =
        [
            new() { Title = "Build succeeded", Text = "The main branch was built in 2 minutes.", Badge = "CI" },
            new() { Title = "New comment", Text = "Jane commented on your pull request.", Badge = "PR" },
            new() { Title = "Release published", Text = "Version 5.1 is now available.", Badge = "New" }
        ];
    }

    public class NotificationItem
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string Badge { get; set; }
    }
}
