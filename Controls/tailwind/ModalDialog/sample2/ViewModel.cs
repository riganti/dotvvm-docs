using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.ModalDialog.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public bool IsSettingsOpen { get; set; }

        public string UserName { get; set; } = "Jane Doe";

        public string SaveStatus { get; set; } = "No changes saved.";

        public void ToggleSettings()
        {
            IsSettingsOpen = !IsSettingsOpen;
        }

        public void SaveSettings()
        {
            IsSettingsOpen = false;
            SaveStatus = "Settings saved.";
        }
    }
}
