Displays a preview of a single file. When the file name has an image extension and a safe URL is provided, the image is displayed; otherwise, the control renders a chip with a document icon, the file extension, and the file name.

The URL can be absolute (HTTP or HTTPS), application-relative (`~/...`), root-relative, or relative. SVG images and `data:` or `blob:` URLs are never previewed.

> The `FilePreview` control is new in DotVVM 5.1.
