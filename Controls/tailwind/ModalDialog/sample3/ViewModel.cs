using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.ModalDialog.sample3
{
    public class ViewModel : DotvvmViewModelBase
    {
        public bool IsReleaseNotesOpen { get; set; }

        public void ToggleReleaseNotes()
        {
            IsReleaseNotesOpen = !IsReleaseNotesOpen;
        }
    }
}
