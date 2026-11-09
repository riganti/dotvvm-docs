using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.ImageCrop.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string SourceImage { get; set; }

        public string Avatar { get; set; }

        public string Status { get; set; } = "Choose an image and apply the crop.";

        public void OnChanged()
        {
            Status = Avatar == null
                ? "The image was removed."
                : $"The crop was applied ({Avatar.Length} characters of data).";
        }
    }
}
