using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public bool IsModalOpen { get; set; } = false;

    public void ToggleModal()
    {
        IsModalOpen = !IsModalOpen;
    }
}