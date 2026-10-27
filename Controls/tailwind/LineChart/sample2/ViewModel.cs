using System.Collections.Generic;
using DotVVM.Controls.Tailwind;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.LineChart.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<Sale> Sales { get; set; } = new() { new() { Month = "Jan", Revenue = 12, Cost = 8 }, new() { Month = "Feb", Revenue = 14, Cost = 9 } };
        public List<Metric> Metrics { get; set; } = new() { new() { Name = "Revenue", Color = TailwindColor.Primary, IsRevenue = true }, new() { Name = "Cost", Color = TailwindColor.Warning, Visible = false } };
    }
    public class Sale { public string Month { get; set; } public int Revenue { get; set; } public int Cost { get; set; } }
    public class Metric { public string Name { get; set; } public TailwindColor Color { get; set; } public bool IsRevenue { get; set; } public bool Visible { get; set; } = true; }
}
