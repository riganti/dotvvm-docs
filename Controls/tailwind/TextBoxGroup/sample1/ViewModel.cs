using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.TextBoxGroup.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string Message { get; set; }

        public int ChangeCount { get; set; }

        public void OnChanged()
        {
            ChangeCount++;
        }
    }
}
