using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.bootstrap.ComboBoxGroup.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string SelectedAnimal { get; set; }
        public string[] Animals { get; set; } = { "Cat", "Dog", "Pig", "Mouse", "Rabbit" };
    }
}
