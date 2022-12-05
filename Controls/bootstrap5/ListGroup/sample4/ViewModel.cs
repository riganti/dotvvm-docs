using DotVVM.Framework.Controls.Bootstrap4;
using DotVVM.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotvvmWeb.Views.Docs.Controls.bootstrap.ListGroup.sample4
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<string> Values => new List<string>() { "one", "two", "three" };

        public string SelectedValue { get; set; }
        
        
        public void Select(string value)
        {
            SelectedValue = value;
        }
    }
    
}