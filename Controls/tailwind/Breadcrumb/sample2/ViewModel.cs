using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public List<BreadcrumbItemData> BreadcrumbItems { get; set; } = new()
    {
        new() { Text = "Home", Url = "?step=home" },
        new() { Text = "Products", Url = "?step=products" },
        new() { Text = "Details", Url = "?step=details", IsActive = true }
    };

    public class BreadcrumbItemData
    {
        public string Text { get; set; } = "";
        public string Url { get; set; } = "";
        public bool IsActive { get; set; }
    }
}
