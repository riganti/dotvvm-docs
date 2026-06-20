using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public List<SlideData> Slides { get; set; } = new()
    {
        new() { Title = "Scenic mountain", ImageUrl = "https://picsum.photos/id/1018/1200/800" },
        new() { Title = "Wild nature", ImageUrl = "https://picsum.photos/id/1025/1200/800" },
        new() { Title = "Misty forest", ImageUrl = "https://picsum.photos/id/1037/1200/800" }
    };

    public class SlideData
    {
        public string Title { get; set; } = "";
        public string ImageUrl { get; set; } = "";
    }
}
