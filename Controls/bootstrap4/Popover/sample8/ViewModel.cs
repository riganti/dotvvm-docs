using DotVVM.Framework.ViewModel;
using System.Collections.Generic;

namespace DotvvmWeb.Views.Docs.Controls.bootstrap.Tooltip.sample6
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<Tooltip> Tooltips { get; set; } = new List<Tooltip>
                {
                    new Tooltip()
                    {
                        Title = "Tooltip 1"
                    },
                    new Tooltip()
                    {
                        Title = "Tooltip 2"
                    }
                };
        
        
        public void DeleteItems()
        {
            Tooltips.Clear();
        }
    }    
    public class Tooltip
    {
        public string Title { get; set; }
    }
}