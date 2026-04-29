using DotVVM.Framework.ViewModel;

public class  ViewModel : DotvvmViewModelBase
{
    public bool IsLoading { get; set; } = false;

    public void ToggleLoading()
    {
        IsLoading = !IsLoading;
    }
}