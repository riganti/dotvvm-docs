## Sample 1: Basic Usage

The `ImageUrl` property specifies the URL of the image that is being edited.

The `Operations` property must be bound to a property of type `ImageOperations` in the viewmodel. This object contains the following properties:

* `Resize` represents the scale ratio of the image.

* `Rotate` represents represents the rotation angle.

* `Crop` represents a rectangular area (with `Left`, `Top`, `Width` and `Height` properties) the crop settings.

* `Round` represents the radius of the rounded corners.

You can use the `DefaultImageFactory` class to load the image, apply the `ImageOperations` to it and save the result image.