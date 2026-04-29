using DotVVM.Framework.ViewModel;

public class  ViewModel : DotvvmViewModelBase
{
    public string Initials { get; set; } = "JS";
    public ControlSize SelectedSize { get; set; } = ControlSize.Default;
}