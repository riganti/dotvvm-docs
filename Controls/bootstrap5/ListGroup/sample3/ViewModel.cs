 public class ViewModel : DotvvmViewModelBase
    {
        public List<LinkItem> LinksDataSet => new List<LinkItem>()
        {
            new LinkItem() { IsEnabled = true, IsSelected = false, NavigateUrl = "/bootstrap4-4.0/?url=https://www.google.com/", Text = "Google", Color = ListGroupItemColor.Info },
            new LinkItem() { IsEnabled = true, IsSelected = false, NavigateUrl = "/bootstrap4-4.0/?url=http://www.w3schools.com/html/", Text = "W3Schools", Color = ListGroupItemColor.Success },
            new LinkItem() { IsEnabled = true, IsSelected = true, NavigateUrl = "/bootstrap4-4.0/?url=https://www.microsoft.com/en-us/", Text = "Microsoft", Color = ListGroupItemColor.Warning },
            new LinkItem() { IsEnabled = false, IsSelected = false, NavigateUrl = "/bootstrap4-4.0/?url=https://github.com/riganti/dotvvm", Text = "DotVVM Github", Color = ListGroupItemColor.Danger }
        };

    }

    public class LinkItem
    {
        public string Text { get; set; }
        public string NavigateUrl { get; set; }
        public bool IsSelected { get; set; }
        public bool IsEnabled { get; set; }
        public ListGroupItemColor Color { get; set; }

    }
