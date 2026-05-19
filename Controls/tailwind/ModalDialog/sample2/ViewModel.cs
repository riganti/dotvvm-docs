using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public bool IsCustomModalOpen { get; set; }

    public string UserName { get; set; } = "John Doe";

    public void ToggleCustomModal()
    {
        IsCustomModalOpen = !IsCustomModalOpen;
    }
}

