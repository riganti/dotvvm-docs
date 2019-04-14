using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.bootstrap.ComboBoxGroup.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string SelectedAnimal { get; set; }
        public string SelectedAnimal2 { get; set; }
        public string[] Animals { get; set; } = { "Cat", "Dog", "Pig", "Mouse", "Rabbit" };
        public bool TrueValue { get; set; } = true;
        public bool FalseValue { get; set; } = false;
    }
}
