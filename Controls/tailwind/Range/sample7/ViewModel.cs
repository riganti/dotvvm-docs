using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public double DisabledValue { get; set; } = 30;
    public bool Enabled { get; set; } = false;
}
