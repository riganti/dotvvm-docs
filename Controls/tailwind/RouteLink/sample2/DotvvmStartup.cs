using DotVVM.Framework.Configuration;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.RouteLink.sample2
{
    public class Startup : IDotvvmStartup
    {
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("SampleA", "", "SampleA.dothtml", null);
            config.RouteTable.Add("SampleB", "detail/{Id?}", "SampleB.dothtml", null);
        }
    }
}
