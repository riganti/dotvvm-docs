using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.DropDownButton.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string LastAction { get; set; } = "(none)";

        public void RecordAction(string action)
        {
            LastAction = action;
        }
    }
}
