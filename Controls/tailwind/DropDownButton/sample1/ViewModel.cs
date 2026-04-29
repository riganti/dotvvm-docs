using DotVVM.Framework.ViewModel;

public class  ViewModel : DotvvmViewModelBase
{
    public List<string> MenuItems { get; set; } = new() { "Profile", "Settings", "Sign Out" };
    public string LastAction { get; set; } = "";

    public void ActionClicked()
    {
        LastAction = "Clicked";
    }
}