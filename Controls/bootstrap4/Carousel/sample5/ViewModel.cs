public class ViewModel : DotvvmViewModelBase
{
    public ImageData[] Images { get; set; } = new[]
    {
        new ImageData("../../images/LA.jpg", "Los Angeles"),
        new ImageData("../../images/NY.jpg", "New York", true),
        new ImageData("../../images/Miami.jpg", "Miami")
    };
}

public class ImageData
{
    public ImageData()
    {
    }

    public ImageData(string url, string caption, bool active = false)
    {
        IsActive = active;
        Url = url;
        Caption = caption;
    }

    public string Caption { get; set; }
    public bool IsActive { get; set; }
    public string Url { get; set; }
}