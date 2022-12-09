using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.businesspack.ImageCrop.sample5
{
    public class ViewModel : DotvvmViewModelBase
    {
        public int ChangeCount { get; set; }
        public string ImagePath { get; set; } = "../../images/imagecrop.webp";
        public ImageOperations ImageOperations { get; set; } = new ImageOperations();

        public void Changed()
        {
            ChangeCount++;
        }
    }
}
