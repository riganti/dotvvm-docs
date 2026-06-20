using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.TopMenu.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string LastClicked { get; set; } = "(none)";

        public List<MenuOption> ActionItems { get; set; } = new List<MenuOption>
        {
            new MenuOption { Text = "Refresh" },
            new MenuOption { Text = "Export" }
        };

        public List<MenuOption> SectionItems { get; set; } = new List<MenuOption>
        {
            new MenuOption { Text = "Release notes", Url = "#release" }
        };

        public void RecordClick(string action)
        {
            LastClicked = action;
        }
    }

    public class MenuOption
    {
        public string Text { get; set; }

        public string Url { get; set; }
    }
}
