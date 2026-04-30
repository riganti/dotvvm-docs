using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public bool IsLoading { get; set; }

    public void ToggleLoading()
    {
        IsLoading = !IsLoading;
    }
}

