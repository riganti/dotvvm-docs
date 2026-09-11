using DotVVM.Framework.ViewModel;
using System.Collections.Generic;

namespace DotvvmWeb.Views.Docs.Controls.bootstrap5_select2.Select2MultiSelect.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<Technology> Technologies { get; set; } = new List<Technology>
        {
            new Technology { Id = 1, Name = "C#" },
            new Technology { Id = 2, Name = "DotVVM" },
            new Technology { Id = 3, Name = "TypeScript" },
            new Technology { Id = 4, Name = "Bootstrap" }
        };

        public List<int> SelectedTechnologies { get; set; } = new List<int> { 1, 2 };

        public class Technology
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}