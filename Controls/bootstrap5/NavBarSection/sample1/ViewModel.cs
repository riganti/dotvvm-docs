public class ViewModel
{
    public List<NavigationItem> LinksDataSet { get; set; }

    private static IQueryable<NavigationItem> GetData()
    {
        return new[]
        {
            new NavigationItem() { IsEnabled = true, IsSelected = false, NavigateUrl = "https://www.google.com/", Text = "Google" },
            new NavigationItem() { IsEnabled = true, IsSelected = false, NavigateUrl = "http://www.w3schools.com/html/", Text = "W3Schools" },
            new NavigationItem() { IsEnabled = true, IsSelected = true, NavigateUrl = "https://www.microsoft.com/en-us/", Text = "Microsoft" },
            new NavigationItem() { IsEnabled = false, IsSelected = false, NavigateUrl = "https://github.com/riganti/dotvvm", Text = "DotVVM Github" }
        }.AsQueryable();
    }

    public ViewModel()
    {
        LinksDataSet = new List<NavigationItem>();
        foreach (NavigationItem l in GetData())
        {
            LinksDataSet.Add(l);
        }
    }

}


public class NavigationItem
{
    public string Text { get; set; }
    public string NavigateUrl { get; set; }
    public bool IsSelected { get; set; }
    public bool IsEnabled { get; set; }
}