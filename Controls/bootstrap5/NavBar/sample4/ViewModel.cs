public class ViewModel
{
    public bool DarkTheme { get; set; } = false;
    public BootstrapBackgroundColor ColorOfBackground { get; set; } = BootstrapBackgroundColor.Light;

    public void ChangeTheme()
    {
        DarkTheme = !DarkTheme;
        if (ColorOfBackground == BootstrapBackgroundColor.Dark)
        {
            ColorOfBackground = BootstrapBackgroundColor.Light;
        }
        else
        {
            ColorOfBackground = BootstrapBackgroundColor.Dark;
        }
    }
}