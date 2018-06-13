# Returning Files

Sometimes you want to generate a file and let the user download it, e.g. export data in GridView in an Excel file. Normally, you just redirect user to some URL
and if the `Content-Type` and `Content-Disposition` headers indicate that it is a file for downloading, the browser will offer the user to download the file.  

However, if you need to generate the file in a response to a button click, it is not so easy. Postbacks in DotVVM are done by AJAX and browsers cannot treat 
the response of an AJAX call as a file download. You would have to write a custom OWIN middleware or a [custom presenter](/docs/tutorials/advanced-custom-presenters/{branch})
that generates the file and writes it to the response stream. Then you'll redirect the user to this presenter and that's it.

The process described above is not much convenient. That's why DotVVM implements a mechanism which lets you just generate the file in your viewmodel command and 
deliver it to the client using the `Context.ReturnFile` method.


## Registering the IReturnedFileStorage

Because you generate the file in the viewmodel and the browser needs to do an additional HTTP request to retrieve the file, you need some kind of a storage for temporary files.
In DotVVM, there is the `IReturnedFileStorage` interface which handles files returned from the viewmodel.

All we have to do is to register it in the `Startup.cs` file. If you create a new DotVVM project, you will find the following line there:

```CSHARP
app.UseDotVVM<DotvvmStartup>(applicationPath, options =>    
    options.AddDefaultTempStorages("temp");
);
``` 

> Please note that the configuration of DotVVM services has changed in DotVVM 1.1. 

This registers storages for uploaded and returned files that store the files in local filesystem. You can find more information about uploading files in the [FileUpload](/docs/controls/builtin/FileUpload/{branch}) documentation.

If you want to register only the returned file storage, you can use `options.AddReturnedFileStorage`.

If you decide to write your own storage (e.g. use Azure blob storage, in-memory file store etc, you can create a custom class that implement `IReturnedFileStorage` and register it on your own:

```CSHARP
options.Services.AddSingleton<IReturnedFileStorage, MyCustomReturnedFileStorage>();
```

## Using context.ReturnFile

The usage is pretty easy then. You just call `Context.ReturnFile` in your viewmodel method. The file is saved in the temporary storage and the user is redirected to 
a special URL that returns the file to him. The ID of the file is a random Guid so it's not possible to retrieve a file that was generated for someone else.

```CSHARP
Context.ReturnFile(file, "export.pdf", "application/pdf");
```

The method accepts a byte array or a stream, the file name, the MIME type and a dictionary with additional response headers.
