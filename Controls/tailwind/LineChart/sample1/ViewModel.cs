using System;
using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.LineChart.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<Visit> Visits { get; set; } = new()
        {
            new() { Date = new DateTime(2026, 1, 1), Count = 420 },
            new() { Date = new DateTime(2026, 2, 1), Count = 680 },
            new() { Date = new DateTime(2026, 3, 1), Count = 810 }
        };
    }

    public class Visit { public DateTime Date { get; set; } public int Count { get; set; } }
}
