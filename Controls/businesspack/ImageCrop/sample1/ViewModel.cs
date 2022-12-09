using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.Utils;
using DotVVM.Framework.ViewModel;


namespace DotvvmWeb.Views.Docs.Controls.businesspack.ImageCrop.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string ImagePath { get; set; } = "../../images/imagecrop.webp";
        public string Result { get; set; }
        public ImageOperations ImageOperations { get; set; } = new ImageOperations();

        public void Save()
        {
            Result = $"/App_Images/{SecureGuidGenerator.GenerateGuid()}.png";

            using (var factory = new DefaultImageFactory())
            {
                factory
                    .Load(GetPhysicalPath(ImagePath))
                    .Apply(ImageOperations)
                    .Save(GetPhysicalPath(Result));
            }
        }

        private string GetPhysicalPath(string url)
        {
            return Context.Configuration.ApplicationPhysicalPath + url.Substring(1).Replace("/", "\\");
        }
    }
}
