using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.DropDownButton.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string LastAction { get; set; } = "(none)";

        public List<DropDownOption> ActionItems { get; set; } = new List<DropDownOption>
        {
            new DropDownOption { Text = "Save draft" },
            new DropDownOption { Text = "Publish" },
            new DropDownOption { Text = "Archive" }
        };

        public List<DropDownOption> SectionItems { get; set; } = new List<DropDownOption>
        {
            new DropDownOption { Text = "Drafts", Url = "#drafts" },
            new DropDownOption { Text = "Reports", Url = "#reports" }
        };

        public void RecordAction(string action)
        {
            LastAction = action;
        }
    }

    public class DropDownOption
    {
        public string Text { get; set; }

        public string Url { get; set; }
    }
}
