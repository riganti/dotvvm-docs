public class ViewModel
{
    public bool DarkTheme { get; set; } = false;
    public BootstrapBackgroundColor ColorOfBackground { get; set; } = BootstrapBackgroundColor.Light;
    public DropdownBackground DropDownBackGround { get; set; } = DropdownBackground.Light;

    public void ChangeTheme()
    {
        DarkTheme = !DarkTheme;
        if (ColorOfBackground == BootstrapBackgroundColor.Dark)
        {
            ColorOfBackground = BootstrapBackgroundColor.Light;
            DropDownBackGround = DropdownBackground.Light;
        }
        else
        {
            ColorOfBackground = BootstrapBackgroundColor.Dark;
            DropDownBackGround = DropdownBackground.Dark;
        }
    }
}