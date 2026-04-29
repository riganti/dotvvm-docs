using DotVVM.Framework.ViewModel;

public class  ViewModel : DotvvmViewModelBase
{
    public string ButtonLabel { get; set; } = "Click Me";
    public bool IsEnabled { get; set; } = true;

    public void DoAction()
    {
        // handle click
    }
}