using System.Collections.Generic;
using DotVVM.Controls.Tailwind;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.DonutChart.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<Channel> Channels { get; set; } = new() { new() { Name = "Search", Visits = 420, Color = TailwindColor.Primary }, new() { Name = "Email", Visits = 280, Color = TailwindColor.Success }, new() { Name = "Social", Visits = 160, Color = TailwindColor.Warning } };
        public string SelectedChannel { get; set; }
        public void Select(string name) => SelectedChannel = name;
    }
    public class Channel { public string Name { get; set; } public int Visits { get; set; } public TailwindColor? Color { get; set; } }
}
