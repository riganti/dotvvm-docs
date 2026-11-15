using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.MultiSelect.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        private static readonly List<Country> AllCountries =
        [
            new() { Code = "at", Name = "Austria" },
            new() { Code = "cz", Name = "Czech Republic" },
            new() { Code = "de", Name = "Germany" },
            new() { Code = "pl", Name = "Poland" },
            new() { Code = "sk", Name = "Slovakia" },
            new() { Code = "us", Name = "United States" }
        ];

        // only a few items are loaded initially, the rest is found by the search
        public List<Country> Countries { get; set; } = AllCountries.Take(2).ToList();

        public List<Country> SelectedCountries { get; set; } = [];

        [AllowStaticCommand]
        public static async Task<List<Country>> SearchCountries(string query)
        {
            await Task.Delay(200); // simulate a database query
            return AllCountries
                .Where(c => c.Name.Contains(query ?? "", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }

    public class Country
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
