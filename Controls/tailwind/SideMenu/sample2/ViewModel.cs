using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.SideMenu.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<NavItem> SettingsLinks { get; set; } = new List<NavItem>
        {
            new NavItem { Text = "Profile", Url = "#profile" },
            new NavItem { Text = "Security", Url = "#security" }
        };

        public List<NavItem> QuickLinks { get; set; } = new List<NavItem>
        {
            new NavItem { Text = "Help", Url = "#help" }
        };
    }

    public class NavItem
    {
        public string Text { get; set; }

        public string Url { get; set; }
    }
}
