using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.DraggableList.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<string> Backlog { get; set; } = ["Design the logo", "Prepare the release", "Update the docs"];

        public List<string> Done { get; set; } = ["Fix the login page"];

        public string Summary { get; set; }

        public void UpdateSummary()
        {
            Summary = $"{Backlog.Count} tasks in the backlog, {Done.Count} done.";
        }
    }
}
