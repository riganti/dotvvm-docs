using DotVVM.Framework.ViewModel;
using System.Collections.Generic;

namespace DotvvmWeb.Views.Docs.Controls.bootstrap5_select2.Select2MultiSelect.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<string> Technologies { get; set; } = new List<string>
        {
            "C#", "DotVVM", "TypeScript"
        };

        public List<string> SelectedTechnologies { get; set; } = new List<string> { "DotVVM" };
        public string TechnologiesOnServer { get; set; } = "";

        public void OnSelectionChanged()
        {
            TechnologiesOnServer = string.Join(", ", Technologies);
        }
    }
}
