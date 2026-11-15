## Sample 1: Basic usage

The `UploadedFiles` property must be bound to an initialized `UploadedFilesCollection` in the viewmodel.

Set `Multiple` to allow selecting more files at once, and `MaxFiles` to limit the total number of files - when the limit is reached, the drop zone is hidden. The `AllowedFileTypes` property accepts a comma-separated list of file extensions or MIME types, and `MaxFileSizeMb` rejects larger files immediately in the browser. The hint below the placeholder is composed automatically from these limits.

The `Changed` command is invoked after files are added or removed.
