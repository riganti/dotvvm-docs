using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public string DynamicImageUrl { get; set; } = "https://picsum.photos/400/200?random=1";

    public string DynamicImageAlt { get; set; } = "Dynamic image";
}

