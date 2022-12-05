using DotVVM.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotvvmWeb.Views.Docs.Controls.bootstrap.ListGroup.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string Text { get; set; } = "Data-bound text of the item.";
        public bool Enabled { get; set; }
        public bool IsSelected { get; set; }
         public void ChangeListGroup()
        {
            Enabled = !Enabled;
            IsSelected = !IsSelected;
            Text = "Changed";
        }

    }
}