using DotVVM.Framework.ViewModel;

public class  ViewModel : DotvvmViewModelBase
{
    public List<BreadcrumbItemData> BreadcrumbItems { get; set; } = new()
    {
        new() { Text = "Home", Url = "/", IsActive = false },
        new() { Text = "Products", Url = "/products", IsActive = false },
        new() { Text = "Details", Url = "", IsActive = true }
    };

    public class BreadcrumbItemData
    {
        public string Text { get; set; } = "";
        public string Url { get; set; } = "";
        public bool IsActive { get; set; }
    }
}