# Return a file from viewmodel

Sometimes you want to generate a file and let the user download it, e. g. export data in GridView to an Excel file. 

Normally, you just redirect user to some URL, and if the `Content-Type` and `Content-Disposition` headers indicate that it is a file for downloading, the browser will offer the user to download the file.  

However, if you need to generate the file in a response to a button click, it is not so easy. Postbacks in DotVVM are done by AJAX, and browsers cannot treat 
the response of an AJAX call as a file download. You would have to write a custom middleware or a [custom presenter](~/pages/concepts/routing/custom-presenters)
that generates the file and writes it to the response stream. Then you'll redirect the user to this presenter and that's it.

The process described above is not much convenient in case that you need a lot of information from the viewmodel in order to generate the file. Since the file would be generated in a separate HTTP request, you would need to pass all the information in a URL, or use session or a similar mechanism. 

That's why DotVVM implements a feature which lets you just generate the file in your viewmodel command, and deliver it to the client using the `Context.ReturnFile` method.

## IReturnedFileStorage

Because you generate the file in the viewmodel, and the browser needs to make an additional HTTP request to retrieve the file, you need some kind of a storage for temporary files. In DotVVM, there is a `IReturnedFileStorage` interface which stores files returned from the viewmodel.

If you create a new DotVVM project, you will find the following line in `DotvvmStartup.cs`:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    options.AddDefaultTempStorage("temp");
}
``` 

This registers storages for uploaded and returned files that store the files in local file system. You can find more information about uploading files in the [upload files](upload-files) chapter.

If you want to register only the returned file storage, you can use `options.AddReturnedFileStorage` method instead.

You can implement your own storage (using e. g. Azure blob storage) by creating a custom class that implement `IReturnedFileStorage` and register it on your own:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    options.Services.AddSingleton<IReturnedFileStorage, MyCustomReturnedFileStorage>();
}
```

## context.ReturnFile

To return a file as a response for a [command](~/pages/concepts/respond-to-user-actions/commands) or [static command](~/pages/concepts/respond-to-user-actions/static-commands), you can just call the `ReturnFile` method on the [request context](~/pages/concepts/viewmodels/request-context). 

The file is saved in the temporary storage and the user is redirected to a special URL that returns the file. The ID of the file is a random Guid, so it should not be easy to guess a name of a file which belongs to someone else.

```CSHARP
Context.ReturnFile(file, "export.pdf", "application/pdf");
```

The method accepts a byte array or a stream, the file name, the MIME type and a dictionary with additional response headers.

## See also

* [Upload files](upload-files)
* [Request context](~/pages/concepts/viewmodels/request-context)