using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.Form.sample4
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string Title { get; set; }

        public List<string> Labels { get; set; } = ["Bug", "Feature", "Documentation"];

        public List<string> SelectedLabels { get; set; } = [];
    }
}
