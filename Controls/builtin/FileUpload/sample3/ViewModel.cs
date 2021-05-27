using System.IO;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Core.Storage;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.builtin.FileUpload.sample3
{
    public class ViewModel : DotvvmViewModelBase
    {
        private IUploadedFileStorage storage;
        
        public UploadedFilesCollection Files { get; set; }


        public ViewModel(IUploadedFileStorage storage)
        {
            // use dependency injection to request IUploadedFileStorage
            this.storage = storage;
            
            Files = new UploadedFilesCollection();
        }


        public async Task Process()
        {
            var uploadPath = GetUploadPath();

            // save all files to disk
            foreach (var file in Files.Files)
            {
                var targetPath = Path.Combine(uploadPath, file.FileId + ".bin");
                await storage.SaveAsAsync(file.FileId, targetPath);
                await storage.DeleteFileAsync(file.FileId);
            }

            // clear the uploaded files collection so the user can continue with other files
            Files.Clear();
        }

        private string GetUploadPath()
        {
            var uploadPath = Path.Combine(Context.Configuration.ApplicationPhysicalPath, "MyFiles");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            return uploadPath;
        }
    }
}