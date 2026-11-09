using System.Collections.Generic;
using DotVVM.Controls.Tailwind;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.RadarChart.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<Skill> Skills { get; set; } = new() { new() { Name = "Speed", Alice = 82, Bob = 64 }, new() { Name = "Accuracy", Alice = 71, Bob = 88 } };
        public List<Person> People { get; set; } = new() { new() { Name = "Alice", Color = TailwindColor.Primary, IsAlice = true }, new() { Name = "Bob", Color = TailwindColor.Success } };
    }
    public class Skill { public string Name { get; set; } public int Alice { get; set; } public int Bob { get; set; } }
    public class Person { public string Name { get; set; } public TailwindColor Color { get; set; } public bool IsAlice { get; set; } }
}
