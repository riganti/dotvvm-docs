Allows the user to upload one or multiple files asynchronously.

### Upload Configuration

The upload works on the background and starts immediately when the user selects the files. To make file uploading work, 
you have to specify where the temporary files will be uploaded.

The recommended strategy is to store the uploaded files in your application directory or in the temp directory (if your app have the appropriate permissions).
To define this, you have to register the **UploadedFileStorage** in your **Startup.cs** file.

```CSHARP
// OWIN
app.UseDotVVM<DotvvmStartup>(appPath, options: o => o.AddUploadedFileStorage("App_Data/Temp"));

// ASP.NET Core
services.AddDotVVM(o => o.AddUploadedFileStorage("App_Data/Temp"));
```

### Using the Control

Then, you need to bind the **FileUpload** control to an **UploadData** object. It holds references to the files 
the user has selected and uploaded.

It has a handy property `IsBusy` of **boolean** which indicates whether the file upload is still in progress. You can use it e.g. on the button's `Enabled` property to disallow the user to continue until the upload is finished.

### Retrieving the Stored Files

The **UploadData** object holds only unique IDs of uploaded files. To get the file, you have to retrieve it using the **UploadedFileStorage** object.

```CSHARP
foreach (var file in UploadData.Files)
{
  if (file.Allowed)
  {
    // get the stream of the uploaded file and do whatever you need to do
    var stream = storage.GetFile(file.FileId);

    // OR you can just move the file from the temporary storage somewhere else
    var targetPath = Path.Combine(uploadPath, file.FileId + ".bin");
    storage.SaveAs(file.FileId, targetPath);
    
    // it is a good idea to delete the file from the temporary storage 
    // it is not required, the file would be deleted automatically after the timeout set in the DotvvmStartup.cs
    storage.DeleteFile(file.FileId);
  }
}
```

A file is not `Allowed` when it's type does not match the `AllowedFileTypes` definition or when it's size exceeds the `MaxFileSize`. See also the `FileTypeAllowed` and `MaxSizeExceeded` properties. You can use them to find out why the file is not allowed. But please note that these value are sent from client-side and can't be trusted in security critical scenarios.

&nbsp;