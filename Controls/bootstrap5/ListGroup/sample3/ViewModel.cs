public class ViewModel : DotvvmViewModelBase
{
    public List<LinkItem> LinksDataSet => new List<LinkItem>()
    {
        new LinkItem() { IsEnabled = true, IsSelected = false, NavigateUrl = "https://www.google.com/", Text = "Google", Color = BootstrapColor.Info },
        new LinkItem() { IsEnabled = true, IsSelected = false, NavigateUrl = "http://www.w3schools.com/html/", Text = "W3Schools", Color = BootstrapColor.Success },
        new LinkItem() { IsEnabled = true, IsSelected = true, NavigateUrl = "https://www.microsoft.com/en-us/", Text = "Microsoft", Color = BootstrapColor.Warning },
        new LinkItem() { IsEnabled = false, IsSelected = false, NavigateUrl = "https://github.com/riganti/dotvvm", Text = "DotVVM Github", Color = BootstrapColor.Danger }
    };
    
}

public class LinkItem
{
    public string Text { get; set; }
    public string NavigateUrl { get; set; }
    public bool IsSelected { get; set; }
    public bool IsEnabled { get; set; }
    public BootstrapColor Color { get; set; }

}