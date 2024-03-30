Allows the user to upload one or multiple files asynchronously.

### Upload Configuration

The upload works on the background and starts immediately when the user selects the files. To make file uploading work, 
you have to specify where the temporary files will be uploaded.

The recommended strategy is to store the uploaded files in your application directory or in the temp directory (if your app have the appropriate permissions).
To define this, you have to register the `UploadedFileStorage` in the `IDotvvmServiceConfigurator`.

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    options.AddUploadedFileStorage("App_Data/Temp");
}
```

### Using the Control

Then, you need to bind the `FileUpload` control to an `UploadData` object. It holds references to the files 
the user has selected and uploaded.

The `UploadData` object also has a useful property `IsBusy` which indicates whether the file upload is still in progress. You can use it e.g. on the button's `Enabled` property to disallow the user to continue until the upload is finished.

### Retrieving the Stored Files

The `UploadData` object contains only unique IDs of uploaded files. To get the file contents, you have to retrieve it using the `IUploadedFileStorage` service.

```CSHARP
public class UploadViewModel 
{
  private readonly IUploadedFileStorage storage;

  public UploadViewModel(IUploadedFileStorage storage)
  {
    this.storage = storage;
  }

  public async Task ProcessFiles()
  {
    foreach (var file in UploadData.Files)
    {
      if (file.IsAllowed)
      {
        // get the stream of the uploaded file and do whatever you need to do
        var stream = storage.GetFile(file.FileId);

        // OR you can just move the file from the temporary storage somewhere else
        var targetPath = Path.Combine(uploadPath, file.FileId + ".bin");
        await storage.SaveAsAsync(file.FileId, targetPath);
        
        // it is a good idea to delete the file from the temporary storage 
        // it is not required, the file would be deleted automatically after the timeout set in the DotvvmStartup.cs
        await storage.DeleteFileAsync(file.FileId);
      }
    }
  }
}
```

The FileUpload control checks whether the file extension or MIME type matches the `AllowedFileTypes` definition, and that the file size does not exceed the `MaxFileSize`.
You can use the `IsFileTypeAllowed` and `IsMaxSizeExceeded` properties of the file in the `UploadData` object to find out why the file was not allowed.

However, please note that the validation is essentially only performed client-side and cannot be trusted for anything beyond displaying error messages.
Any user able to press F12 can modify all unencrypted view model properties, including those in `UploadedFilesCollection`.
Make sure to validate the file characteristics based on the data from `IUploadedFileStorage` when it is important.

&nbsp;
