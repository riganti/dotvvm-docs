public class ViewModel : DotvvmViewModelBase
{
    public ImageModel Image { get; set; } = new();

    public UploadedFilesCollection ImageUpload { get; set; } = new();

    public string[] Types => new[] { "Small", "Large" };

    public void ProcessImage() 
    {
        // TODO
    }
}

public class ImageModel 
{
    public string ImageUrl { get; set; }

    public string Type { get; set; }
}
