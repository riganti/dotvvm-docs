using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.RadarChart.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<Skill> Skills { get; set; } = new() { new() { Name = "Speed", Alice = 82, Bob = 64 }, new() { Name = "Accuracy", Alice = 71, Bob = 88 }, new() { Name = "Teamwork", Alice = 90, Bob = 76 } };
    }
    public class Skill { public string Name { get; set; } public int Alice { get; set; } public int Bob { get; set; } }
}
