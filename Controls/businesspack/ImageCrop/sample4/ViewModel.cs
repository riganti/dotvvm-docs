using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.businesspack.ImageCrop.sample4
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string ImagePath { get; set; } = "https://www.dotvvm.com/docs/samples/images/imagecrop.webp";
        public ImageOperations ImageOperations { get; set; } = new ImageOperations();
    }
}
