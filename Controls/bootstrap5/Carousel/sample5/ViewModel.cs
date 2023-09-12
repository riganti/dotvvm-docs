using DotVVM.Framework.ViewModel;

namespace Dotvvm.Samples.Carousel.sample5
{
    public class ViewModel : DotvvmViewModelBase
    {
        public ImageData[] Images { get; set; } = new[]
        {
            new ImageData("/Images/LA.jpg", "Los Angeles"),
            new ImageData("/Images/NY.jpg", "New York", true),
            new ImageData("/Images/Miami.jpg", "Miami")
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

}