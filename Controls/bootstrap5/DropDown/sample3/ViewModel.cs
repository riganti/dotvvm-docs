public class ViewModel : DotvvmViewModelBase
{
    public List<LinkItem> LinksDataSet { get; set; }

    private static IQueryable<LinkItem> GetData()
    {
        return new[]
        {
            new LinkItem() {  Enabled = true, IsSelected = false, NavigateUrl = "https://www.google.com/", Text = "Google"},
            new LinkItem() {  Enabled = true, IsSelected = false, NavigateUrl = "http://www.w3schools.com/html/", Text = "W3Schools"},
            new LinkItem() {  Enabled = true, IsSelected = true, NavigateUrl = "https://www.microsoft.com/en-us/", Text = "Microsoft"},
            new LinkItem() {  Enabled = false, IsSelected = false, NavigateUrl = "https://github.com/riganti/dotvvm", Text = "DotVVM Github"}
        }.AsQueryable();
    }

    public ViewModel()
    {
        LinksDataSet = new List<LinkItem>();
        foreach (LinkItem l in GetData())
        {
            LinksDataSet.Add(l);
        }
    }


}
public class LinkItem
{
    public string Text { get; set; }
    public string NavigateUrl { get; set; }
    public bool IsSelected { get; set; }
    public bool Enabled { get; set; }

}