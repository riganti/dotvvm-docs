using System.Collections.Generic;
using DotVVM.Controls.Tailwind;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.BarChart.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<SalesPoint> Sales { get; set; } = new()
        {
            new() { Month = "January", Revenue = 12400, Cost = 8100 },
            new() { Month = "February", Revenue = 13900, Cost = 8600 }
        };
        public List<Metric> Metrics { get; set; } = new()
        {
            new() { Name = "Revenue", Color = TailwindColor.Primary, IsRevenue = true },
            new() { Name = "Cost", Color = TailwindColor.Warning }
        };
        public bool IsStacked { get; set; }
        public bool IsHorizontal { get; set; }
    }

    public class SalesPoint { public string Month { get; set; } public decimal Revenue { get; set; } public decimal Cost { get; set; } }
    public class Metric { public string Name { get; set; } public TailwindColor Color { get; set; } public bool IsRevenue { get; set; } }
}
