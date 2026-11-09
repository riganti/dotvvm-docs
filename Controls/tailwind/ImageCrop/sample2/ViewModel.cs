using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.ImageCrop.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string SourceImage { get; set; } = "https://avatars.githubusercontent.com/u/9575026?v=4";

        public string Banner { get; set; }

        public string Photo { get; set; }

        public bool IsEnabled { get; set; } = true;
    }
}
