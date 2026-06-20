using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.RadioButton.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string ContactMethod { get; set; } = "email";

        public int ChangeCount { get; set; }

        public void IncrementChanges()
        {
            ChangeCount++;
        }
    }
}
