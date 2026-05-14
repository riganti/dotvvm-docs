using DotVVM.Framework.ViewModel;
using System.Collections.Generic;

public class ViewModel : DotvvmViewModelBase
{
    public List<string> DashboardLinks { get; set; } = new()
    {
        "Analytics",
        "Reports",
        "Overview"
    };

    public List<NavLink> SettingsLinks { get; set; } = new()
    {
        new NavLink { Text = "Profile", Url = "#profile" },
        new NavLink { Text = "Security", Url = "#security" },
        new NavLink { Text = "Notifications", Url = "#notifications" }
    };

    public class NavLink
    {
        public string Text { get; set; } = "";
        public string Url { get; set; } = "#";
    }
}