using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.BarChart.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<SalesPoint> Sales { get; set; } = new()
        {
            new() { Month = "January", Revenue = 12400, Cost = 8100 },
            new() { Month = "February", Revenue = 13900, Cost = 8600 },
            new() { Month = "March", Revenue = 15200, Cost = 9200 }
        };

        public string SelectedMonth { get; set; }

        public void Select(string month) => SelectedMonth = month;
    }

    public class SalesPoint
    {
        public string Month { get; set; }
        public decimal Revenue { get; set; }
        public decimal Cost { get; set; }
    }
}
