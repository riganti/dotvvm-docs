using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.MultiSelect.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<string> Tags { get; set; } = ["Bug", "Feature", "Documentation", "Question"];

        public List<string> SelectedTags { get; set; } = ["Feature"];

        public List<Country> Countries { get; set; } =
        [
            new() { Id = 1, Name = "Czech Republic" },
            new() { Id = 2, Name = "Germany" },
            new() { Id = 3, Name = "United States" }
        ];

        public List<int> SelectedCountryIds { get; set; } = [];

        public bool IsEnabled { get; set; } = true;

        public int ChangeCount { get; set; }

        public void OnChanged()
        {
            ChangeCount++;
        }
    }

    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
