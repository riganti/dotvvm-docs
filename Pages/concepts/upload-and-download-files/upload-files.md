# Upload files

DotVVM contains the [FileUpload](~/controls/builtin/FileUpload) control which implements the asynchronous file upload mechanism.

The upload works on the background and starts immediately when the user selects the files. The progress is reported to the user (via a CSS-styleable user interface), and the user can work with the page while the files are being uploaded. 

When the upload is complete, unique IDs of the files stored on the server are written in a `UploadedFilesCollection` object in the viewmodel, which can be used to access the file contents.

### Upload configuration

To make file uploads work, you need to specify where the temporary files will be uploaded.

The recommended strategy is to store the uploaded files in your application directory, or in some temp directory (if your app have the appropriate permissions).

The default project template specifies the default configuration of the uploaded files storage in `DotvvmStartup.cs`. It registers a storage for uploaded files, as well as a storage for [returned files](return-file-from-viewmodel).

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    options.AddDefaultTempStorage("temp");

    // this is equivalent to registering both uploaded and returned file storage

    // options.AddUploadedFileStorage("temp");
    // options.AddReturnedFileStorage("temp");
}
```

The default storage creates a `temp` folder in your app directory, and stores uploaded files there. In order to prevent unlimited growth of this folder, the files are deleted after 30 minutes. You can change the default timeout to some other value:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    options.AddDefaultTempStorage("temp", TimeSpan.FromMinutes(10));
}
```

Alternatively, you can provide a different implementation of `IUploadedFileStorage` - for example store files in [Azure Blob Storage](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blobs-introduction).

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    options.Services.AddSingleton<IUploadedFileStorage, MyCustomUploadedFileStorage>();
}
```

## UploadedFilesCollection

The [FileUpload](~/controls/builtin/FileUpload) control needs to specify a `UploadedFilesCollection`. 

This object contains several properties:

* `Files` property is a list with entries for all uploaded files
  * `FileId` property contains a unique id of the file on the server
  * `FileName` property contains a name of the file on the user's computer
  * `FileSize` provides info about the size of the file (number of bytes, and a human-readable value representing the file size )
* `IsBusy` and `Progress` properties indicate whether there something being uploaded at the moment, and the progress in percents (0 to 100)
* `Error` property contains an error message indicating if there was a problem during the upload

While files are being uploaded, DotVVM regularly updates the contents of the collection. 

## Process the stored files

The files are saved to a temporary location on the server. The `UploadedFilesCollection` holds only unique IDs of the files. 

To access the file contents, you need to retrieve them using the `IUploadedFileStorage` object. 

The simplest way to interact with `IUploadedFileStorage` service is to request it as a parameter in the viewmodel constructor.

```CSHARP
public class MyViewModel : DotvvmViewModelBase
{

    private readonly IUploadedFileStorage storage;

	  public MyViewModel(IUploadedFileStorage storage)
	  {
	      this.storage = storage;
	  }
	
	  ...
}
```

You can then use the `GetFileAsync` method to retrieve the `Stream` to access the file contents.

```CSHARP
foreach (var file in UploadedFiles.Files)
{
    // get the stream of the uploaded file and do whatever you need to do
    var stream = await storage.GetFileAsync(file.FileId);

    // OR you can just move the file from the temporary storage somewhere else
    await storage.SaveAsAsync(file.FileId, "some-target-path/myfile.bin");
    
    // it is a good idea to delete the file from the temporary storage 
    // the file would be deleted automatically after the timeout set in the DotvvmStartup.cs (default is 30 minutes)
    await storage.DeleteFileAsync(file.FileId);
}
```

**Be very careful if you use the `FileName` property** - an attacker may tamper with the file name in attempt to submit executable code to your server to a location where it will be executed. We recommend to always generate file names on your own, and use the `FileName` property supplied by the user only in the `Content-Disposition` header when you want to allow the file be downloaded later. 

> The `FileUpload` control allows to restrict the uploaded files to only specific content types, and also allows to limit the size of the file. However, the check happens in the browser, so you should **always validate files** that come to your application.

## Request body limits in ASP.NET

When files are uploaded, the default limits of the ASP.NET platform come in place.

# [ASP.NET Core](#tab/aspnetcore)

In ASP.NET Core, there are two limits which can prevent the files from being uploaded:

* [Kestrel maximum request body size](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-2.1#kestrel-maximum-request-body-size-2) is 30 MB by default. 

You can change the limit in `Program.cs`:

```CSHARP
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .ConfigureKestrel((context, options) =>
        {
            // Handle requests up to 50 MB
            options.Limits.MaxRequestBodySize = 52428800;
        });
```

* [Multipart body length limit](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-2.1#multipart-body-length-limit-2) is 128 MB by default.

You can change the limit in the `ConfigureServices` method in `Startup.cs`:

```CSHARP
public void ConfigureServices(IServiceCollection services)
{
    ...

    services.Configure<FormOptions>(options =>
    {
        // Set the limit to 256 MB
        options.MultipartBodyLengthLimit = 268435456;
    });
}
```

* If you are running on IIS, make sure to configure the request filtering limit too:

```
<configuration>
  <system.webServer>
    <security>
      <requestFiltering>
        <!-- 256 -->
        <requestLimits maxAllowedContentLength="268435456" /> // specified in bytes
      </requestFiltering>
    </security>
  </system.webServer>
</configuration>
```

# [OWIN](#tab/owin)

In OWIN, the request body size is configured in the `web.config` file:

```
<configuration>
  <system.web>
    <!-- 256 MB -->
    <httpRuntime maxRequestLength="262144" /> // specified in kBytes
  </system.web>

  <system.webServer>
    <security>
      <requestFiltering>
        <!-- 256 -->
        <requestLimits maxAllowedContentLength="268435456" /> // specified in bytes
      </requestFiltering>
    </security>
  </system.webServer>
</configuration>
```

***

If you have problems uploading large files, check if there are any other limits on request length imposed by the web server or a reverse proxy.

## See also

* [FileUpload](~/controls/builtin/FileUpload)
* [Return file from viewmodel](return-file-from-viewmodel)