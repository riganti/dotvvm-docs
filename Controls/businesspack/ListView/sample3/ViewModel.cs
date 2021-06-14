using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.businesspack.ListView.sample3
{
    public class ViewModel : DotvvmViewModelBase
    {
        public int RemainingSelections { get; set; } = 3;

        public List<string> Fruit { get; set; } = new List<string> {
            "Apple",
            "Banana",
            "Orange",
            "Watermelon"
        };

        public List<string> SelectedFruit { get; set; } = new List<string>();

        public void SelectionChanged()
        {
            RemainingSelections = 3 - SelectedFruit.Count;
        }
    }
}