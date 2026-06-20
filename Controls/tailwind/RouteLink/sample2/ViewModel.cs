using System;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.RouteLink.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string RouteId { get; set; } = "(none)";

        public override Task Init()
        {
            if (Context.Parameters.ContainsKey("Id"))
            {
                RouteId = Convert.ToString(Context.Parameters["Id"]);
            }

            return base.Init();
        }
    }
}
