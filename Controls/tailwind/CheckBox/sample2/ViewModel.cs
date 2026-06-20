using DotVVM.Framework.ViewModel;
using System.Collections.Generic;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.CheckBox.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<string> SelectedFruits { get; set; } = new() { "banana" };
    }
}
