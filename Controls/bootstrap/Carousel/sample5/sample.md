## Sample 5: Carousel Data-binding

The `Carousel` slides can also be loaded from a collection in the viewmodel. Use the `DataSource` property to specify the source collection.
Then use the `ImageUrlBinding` to specify which property of the collection item will be used as the URL of the image. Similarly, the `CaptionBinding` property 
says which property will be the slide caption and finally the `IsActiveBinding` specifies which of the slides will be displayed first.