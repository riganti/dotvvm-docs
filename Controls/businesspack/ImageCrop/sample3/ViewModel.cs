using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.ViewModel;


namespace DotvvmWeb.Views.Docs.Controls.businesspack.ImageCrop.sample3
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string ImagePath { get; set; } = "../images/imagecrop.webp";
        public ImageOperations ImageOperations { get; set; } = new ImageOperations();
    }
}
