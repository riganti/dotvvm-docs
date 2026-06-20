using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public List<BreadcrumbActionData> Actions { get; set; } = new()
    {
        new() { Text = "Catalog" },
        new() { Text = "Details" },
        new() { Text = "Review" }
    };

    public string LastSelected { get; set; } = "Catalog";

    public void Select(string text)
    {
        LastSelected = text;
    }

    public class BreadcrumbActionData
    {
        public string Text { get; set; } = "";
    }
}
