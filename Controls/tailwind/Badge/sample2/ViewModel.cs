using DotVVM.Controls.Tailwind.Controls;
using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public TailwindColor SelectedBadgeColor { get; set; } = TailwindColor.Primary;
    public TailwindColor[] AllBadgeColors { get; set; } = System.Enum.GetValues<TailwindColor>();
}
