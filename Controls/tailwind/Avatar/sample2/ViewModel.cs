using System;
using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public string DynamicImageUrl { get; set; } = "https://i.pravatar.cc/150?img=2";
    public string DynamicAltText { get; set; } = "User avatar";
    public ControlSize SelectedSize { get; set; } = ControlSize.Default;
    public ControlSize[] AllSizes { get; set; } = (ControlSize[])Enum.GetValues(typeof(ControlSize));
    public string InitialsText { get; set; } = "AB";
}
