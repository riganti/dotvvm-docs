## Sample 1: Previewing uploaded files

Bind the `Files` property to the same `UploadedFilesCollection` which is used by the [FileUpload](~/controls/tailwind/FileUpload) control. Set `AllowRemove` to `true` to let the user remove files from the collection.

The `FileUrl` binding is evaluated in the context of each `UploadedFile` object, so you can use its `FileId` or `FileName` properties to build a URL to your own download endpoint. Return `null` for files which should be displayed as a chip.

The preview is often placed in the `StartTemplate` of the [TextBoxGroup](~/controls/tailwind/TextBoxGroup) control.
