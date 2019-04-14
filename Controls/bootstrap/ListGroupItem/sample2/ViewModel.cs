using DotVVM.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotvvmWeb.Views.Docs.Controls.bootstrap.ListGroupItem.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string BadgeText { get; set; } = "Binded Badge Text From View Model";

        public string NavigateUrl { get; set; } = "https://www.dotvvm.com";
    }
}