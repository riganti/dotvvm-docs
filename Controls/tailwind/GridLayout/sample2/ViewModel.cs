using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.GridLayout.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public int? ColumnCount { get; set; } = 3;

        public List<string> Items { get; set; } = Enumerable.Range(1, 12).Select(i => $"Item {i}").ToList();
    }
}
