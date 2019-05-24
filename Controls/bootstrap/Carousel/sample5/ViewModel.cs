using DotVVM.Framework.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmWeb.Views.Docs.Controls.bootstrap.Carousel.sample5
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
            Active = active;
            URL = url;
            Caption = caption;
        }

        public string Caption { get; set; }
        public bool Active { get; set; }
        public string URL { get; set; }
    }
}