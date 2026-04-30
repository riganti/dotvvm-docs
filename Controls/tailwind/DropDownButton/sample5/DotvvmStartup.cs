using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotVVM.Framework.Configuration;

namespace DotvvmWeb.Views.Docs.Controls.builtin.RouteLink.sample1
{
    public class Startup : IDotvvmStartup
    {
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("SampleA", "SampleA", "SampleA.dothtml", null);
        }
    }
}