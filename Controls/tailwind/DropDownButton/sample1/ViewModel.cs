using DotVVM.Framework.ViewModel;

public class  ViewModel : DotvvmViewModelBase
{
    public List<string> MenuItems { get; set; } = new() { "Profile", "Settings", "Sign Out" };
    public string LastClicked { get; set; } = "(none)";

    public void ItemClicked(string item)
    {
        LastClicked = $"{item} clicked at {DateTime.Now:HH:mm:ss}";
    }
}