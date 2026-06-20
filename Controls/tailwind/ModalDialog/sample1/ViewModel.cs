using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.ModalDialog.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public bool IsDialogOpen { get; set; }

        public string Result { get; set; } = "Nothing deleted yet.";

        public void OpenDialog()
        {
            IsDialogOpen = true;
        }

        public void CloseDialog()
        {
            IsDialogOpen = false;
            Result = "Deletion canceled.";
        }

        public void ConfirmDelete()
        {
            IsDialogOpen = false;
            Result = "Report deleted.";
        }
    }
}
