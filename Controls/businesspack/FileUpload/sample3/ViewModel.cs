using System.IO;
using System.Threading.Tasks;
using DotVVM.BusinessPack.Controls;
using DotVVM.Core.Storage;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.businesspack.FileUpload.sample3
{
    public class ViewModel : DotvvmViewModelBase
    {
        private readonly IUploadedFileStorage fileStorage;

        public ViewModel(IUploadedFileStorage fileStorage)
        {
            this.fileStorage = fileStorage;
        }

        public UploadData Upload { get; set; } = new UploadData();

        public async Task Process()
        {
            var folderPath = GetFolderPath();

            // save all files to disk
            foreach (var file in Upload.Files)
            {
                var filePath = Path.Combine(folderPath, file.FileId + ".bin");
                await fileStorage.SaveAsAsync(file.FileId, filePath);
                await fileStorage.DeleteFileAsync(file.FileId);
            }

            // clear the data so the user can continue with other files
            Upload.Clear();
        }

        private string GetFolderPath()
        {
            var folderPath = Path.Combine(Context.Configuration.ApplicationPhysicalPath, "MyFiles");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath;
        }
    }
}
