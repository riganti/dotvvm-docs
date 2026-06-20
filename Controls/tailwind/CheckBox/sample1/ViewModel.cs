using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.CheckBox.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public bool ReceiveUpdates { get; set; }

        public bool AcceptTerms { get; set; }

        public bool LockedOption { get; set; } = true;

        public int ChangeCount { get; set; }

        public void IncrementChanges()
        {
            ChangeCount++;
        }
    }
}
