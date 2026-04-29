using DotVVM.Framework.ViewModel;

public class  ViewModel : DotvvmViewModelBase
{
    public TailwindColor BadgeColor { get; set; } = TailwindColor.Danger;
    public int Count { get; set; } = 4;
}