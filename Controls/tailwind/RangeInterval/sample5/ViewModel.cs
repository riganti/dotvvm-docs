using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public double DisabledMin { get; set; } = 30;
    public double DisabledMax { get; set; } = 60;
    public bool Enabled { get; set; } = false;
}
