Displays previews of all completed files in an `UploadedFilesCollection`. Each file is rendered using the [FilePreview](~/controls/tailwind/FilePreview) control, and the control can optionally render a button which removes the file from the collection.

The uploaded files do not have any URL from which they could be downloaded - the application needs to provide the URL of an endpoint which serves the files (and checks the permissions) using the `FileUrl` property. Without it, the files are displayed as chips with the file name.

> The `UploadedFilesPreview` control is new in DotVVM 5.1.
