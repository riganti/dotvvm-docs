using DotVVM.Framework.ViewModel;
using System;
using System.Collections.Generic;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.Form.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string FullName { get; set; } = "Thomas Smith";

        public List<Country> Countries { get; set; } =
        [
            new() { Code = "cz", Name = "Czech Republic" },
            new() { Code = "fr", Name = "France" },
            new() { Code = "uk", Name = "United Kingdom" }
        ];

        public string? SelectedCountry { get; set; } = "cz";

        public DateTime? Arrival { get; set; } = new DateTime(2026, 10, 5);

        public bool Subscribe { get; set; } = true;

        public string ContactMethod { get; set; } = "email";

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
