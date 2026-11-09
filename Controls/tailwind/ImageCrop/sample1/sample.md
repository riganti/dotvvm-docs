## Sample 1: Avatar editor

Bind `ImageData` to a property which holds the source image, and `CroppedValue` to a property which receives the cropped result. Both properties are nullable strings containing data URIs. Choosing a new source image clears the cropped value; removing the image clears both.

The `Shape` property can be `Rectangle`, `Square`, or `Circle`. Use `OutputWidth`, `OutputHeight`, and `OutputFormat` to control the result. Circle crops need `Png` or `Webp` output to preserve transparency.

The `Changed` command is invoked after the crop is applied or the image is removed. The `MaxFileSizeMb`, `MaxImageWidth`, and `MaxImageHeight` properties reject images which are too large.
