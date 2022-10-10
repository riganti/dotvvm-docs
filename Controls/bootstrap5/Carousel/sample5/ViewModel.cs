public class ViewModel : DotvvmViewModelBase
{
    public ImageData[] Images { get; set; } = new[]
    {
        new ImageData("~/Images/LA.jpg", "Los Angeles", 1000),
        new ImageData("~/Images/NY.jpg", "New York", 2000, true),
        new ImageData("~/Images/Miami.jpg", "Miami", 500)
    };
}

public class ImageData
{
    public ImageData()
    {
    }

    public ImageData(string url, string caption, int Delay, bool active = false)
    {
        IsActive = active;
        Url = url;
        Caption = caption;
        Delay = Delay;
    }

    public string Caption { get; set; }
    public bool IsActive { get; set; }
    public string Url { get; set; }
    public int Delay { get ;set; }
}