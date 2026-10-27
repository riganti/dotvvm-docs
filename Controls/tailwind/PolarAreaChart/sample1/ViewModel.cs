using System.Collections.Generic;
using DotVVM.Controls.Tailwind;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.PolarAreaChart.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<Channel> Channels { get; set; } = new() { new() { Name = "Email", Leads = 34, Color = TailwindColor.Primary }, new() { Name = "Ads", Leads = 21, Color = TailwindColor.Warning }, new() { Name = "Events", Leads = 12, Color = TailwindColor.Success } };
    }
    public class Channel { public string Name { get; set; } public int Leads { get; set; } public TailwindColor? Color { get; set; } }
}
