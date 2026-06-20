using DotVVM.Framework.ViewModel;
using System.Collections.Generic;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.ComboBox.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<Country> Countries { get; set; } =
        [
            new() { Code = "cz", Name = "Czech Republic" },
            new() { Code = "de", Name = "Germany" },
            new() { Code = "us", Name = "United States" }
        ];

        public string? SelectedCountry { get; set; } = "cz";

        public bool IsEnabled { get; set; } = true;

        public int ChangeCount { get; set; }

        public void IncrementChanges()
        {
            ChangeCount++;
        }
    }

    public class Country
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
