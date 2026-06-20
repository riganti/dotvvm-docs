using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.DropDownButton.sample3
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<RouteOption> RouteOptions { get; set; } = new List<RouteOption>
        {
            new RouteOption { Title = "Orders", Description = "Open the details page." },
            new RouteOption { Title = "Invoices", Description = "This item uses the same route target." }
        };
    }

    public class RouteOption
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
