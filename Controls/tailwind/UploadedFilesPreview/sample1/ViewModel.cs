using System;
using System.Collections.Generic;
using DotVVM.Core.Storage;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.UploadedFilesPreview.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        private static readonly Guid LogoId = Guid.Parse("258487ea-6680-4d7f-9f52-e13a4a467fe0");

        public UploadedFilesCollection Files { get; set; } = new()
        {
            Files = new List<UploadedFile>
            {
                new() { FileId = LogoId, FileName = "logo.png", IsFileTypeAllowed = true },
                new() { FileId = Guid.NewGuid(), FileName = "report.pdf", IsFileTypeAllowed = true }
            }
        };

        // In a real application, map the file IDs to your own download endpoint
        public Dictionary<Guid, string> ImageUrls { get; set; } = new()
        {
            [LogoId] = "https://avatars.githubusercontent.com/u/9575026?v=4"
        };
    }
}
