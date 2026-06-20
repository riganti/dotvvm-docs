using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public bool IsFirstOpen { get; set; } = true;

    public bool IsSecondOpen { get; set; } = false;
}

