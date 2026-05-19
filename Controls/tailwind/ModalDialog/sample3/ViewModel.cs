using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public bool IsLargeModalOpen { get; set; }

    public void ToggleLargeModal()
    {
        IsLargeModalOpen = !IsLargeModalOpen;
    }
}

