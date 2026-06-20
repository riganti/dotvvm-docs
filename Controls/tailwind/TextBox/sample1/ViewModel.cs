using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.TextBox.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string SearchText { get; set; } = "Tailwind";

        public string Password { get; set; } = "secret";

        public bool PasswordEnabled { get; set; } = true;

        public int ChangeCount { get; set; }

        public void IncrementChanges()
        {
            ChangeCount++;
        }
    }
}
