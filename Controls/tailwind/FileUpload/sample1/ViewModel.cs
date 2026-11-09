using System.Linq;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.FileUpload.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public UploadedFilesCollection Files { get; set; } = new UploadedFilesCollection();

        public int FileCount { get; set; }

        public void OnChanged()
        {
            FileCount = Files.Files.Count(f => f.IsAllowed);
        }
    }
}
