using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public double PriceMin { get; set; } = 200;
    public double PriceMax { get; set; } = 800;
}
