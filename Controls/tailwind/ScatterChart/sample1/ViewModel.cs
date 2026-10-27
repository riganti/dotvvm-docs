using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.ScatterChart.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<Person> GroupA { get; set; } = new() { new() { Name = "A1", Height = 162, Weight = 58 }, new() { Name = "A2", Height = 175, Weight = 71 } };
        public List<Person> GroupB { get; set; } = new() { new() { Name = "B1", Height = 166, Weight = 73 }, new() { Name = "B2", Height = 186, Weight = 95 } };
        public string SelectedPoint { get; set; }
        public void Select(string name) => SelectedPoint = name;
    }
    public class Person { public string Name { get; set; } public double Height { get; set; } public double Weight { get; set; } }
}
