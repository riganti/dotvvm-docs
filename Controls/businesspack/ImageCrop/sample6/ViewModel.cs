using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.businesspack.ImageCrop.sample6
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string ImagePath { get; set; } = "../images/imagecrop.webp";

        public ImageOperations ImageOperations { get; set; } = new ImageOperations {
            Crop = new CropRectangle {
                Left = 150,
                Top = 150,
                Width = 100,
                Height = 100
            },
            Resize = 400,
            Rotate = 45,
            Round = 50
        };
    }
}
