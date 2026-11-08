An image editor which lets the user pick an image (from a file, by pasting or dropping it, or from a URL), and then crop, zoom, rotate, and flip it. The result is stored in the viewmodel as a data URI.

The original image (`ImageData`) and the committed crop (`CroppedValue`) are stored separately, so the user can return and change the crop later without losing the original image.

> The `ImageCrop` control is new in DotVVM 5.1.

Never trust the images received from the client. To validate and re-encode the image on the server, use the `ImageService` class:

```CSHARP
var bytes = await new ImageService().ProcessImageAsync(stream,
    o => o.HasFormat(ImageFormat.Jpeg).HasMaxDimensions(1000, 1000));
```

The service preserves the aspect ratio, never upscales the image, and throws `InvalidDataException` for invalid images.
