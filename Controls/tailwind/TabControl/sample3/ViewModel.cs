using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.TabControl.sample3
{
    public class ViewModel : DotvvmViewModelBase
    {
        public int SelectedTabIndex { get; set; }

        public void SelectTab(int index)
        {
            SelectedTabIndex = index;
        }
    }
}
