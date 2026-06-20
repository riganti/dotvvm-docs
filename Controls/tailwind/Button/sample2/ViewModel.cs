using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public int ClickCount { get; set; }

    public void IncrementClick()
    {
        ClickCount++;
    }
}
