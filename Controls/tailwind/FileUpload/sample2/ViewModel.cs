using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.FileUpload.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public UploadedFilesCollection Files { get; set; } = new UploadedFilesCollection();

        public bool IsEnabled { get; set; } = true;
    }
}
