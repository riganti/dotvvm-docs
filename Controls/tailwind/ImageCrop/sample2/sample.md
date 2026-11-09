## Sample 2: Loading images from URL

Set `AllowLocalFile` to `false` to let the user enter only a URL of an image. HTTP(S) URLs are downloaded in the browser, so the server hosting the image must allow it using CORS. You can also set `ImageData` to a URL from the viewmodel.

The `AspectRatio` property restricts the crop area to a fixed ratio of width and height. `Size="Small"` renders a compact layout, and `ZoomStep` sets how much the image is zoomed with each step.

On mobile devices, `CameraCapture` tells the browser to offer the camera (`User` for the front camera, `Environment` for the back one). `AcceptedFileTypes` limits the file types offered in the file dialog, and `OutputQuality` sets the quality of `Jpeg` and `Webp` output.

The `Enabled` property disables the editor - when it is not set, the value of `FormControls.Enabled` is used.
