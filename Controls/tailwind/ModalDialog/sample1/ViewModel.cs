public class ModalDialogViewModel
{
    public bool IsModalOpen { get; set; } = false;

    public void ToggleModal()
    {
        IsModalOpen = !IsModalOpen;
    }
}