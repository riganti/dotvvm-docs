public class TabControlViewModel
{
    public int SelectedTabIndex { get; set; } = 0;

    public void SelectTab(int index)
    {
        SelectedTabIndex = index;
    }
}