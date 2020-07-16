using DotVVM.Framework.ViewModel;
using System.Collections.Generic;

namespace DotvvmWeb.Views.Docs.Controls.bootstrap.Tooltip.sample6
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<Popover> Popovers { get; set; } = new List<Popover>
                {
                    new Popover()
                    {
                        Title = "Tooltip 1",
                        Content = "Content 1"
                    },
                    new Popover()
                    {
                        Title = "Tooltip 2",
                        Content = "Content 2"
                    }
                };
        
        
        public void DeleteItems()
        {
            Popovers.Clear();
        }
    }    
    public class Popover
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}