using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public int SelectedTabIndex { get; set; } = 0;

    public void SelectTab(int index)
    {
        SelectedTabIndex = index;
    }
}