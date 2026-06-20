using DotVVM.Framework.Configuration;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.DropDownButton.sample3
{
    public class Startup : IDotvvmStartup
    {
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("SampleA", "", "SampleA.dothtml", null);
            config.RouteTable.Add("SampleB", "details", "SampleB.dothtml", null);
        }
    }
}
