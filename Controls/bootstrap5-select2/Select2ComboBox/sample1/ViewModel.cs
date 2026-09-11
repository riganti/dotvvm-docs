using DotVVM.Framework.ViewModel;
using System.Collections.Generic;

namespace DotvvmWeb.Views.Docs.Controls.bootstrap5_select2.Select2ComboBox.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<Country> Countries { get; set; } = new List<Country>
        {
            new Country { Code = "CZ", Name = "Czech Republic" },
            new Country { Code = "DE", Name = "Germany" },
            new Country { Code = "PL", Name = "Poland" },
            new Country { Code = "SK", Name = "Slovakia" }
        };

        public string SelectedCountry { get; set; }

        public class Country
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }
    }
}