public class ViewModel : DotvvmViewModelBase
{
    public List<ButtonDto> Buttons { get; set; }

    public override Task Init()
    {
        Buttons = new List<ButtonDto>() { new ButtonDto(), new ButtonDto(), new ButtonDto(), new ButtonDto() };
        return base.Init();
    }
}

public class ButtonDto
{
    public string Text { get; set; } = "Button";
    public ButtonType Color { get; set; } = ButtonType.Danger;
    public ButtonVisualStyle Style { get; set; } = ButtonVisualStyle.OutLine;

    public void Action()
    {
        Text = "CHANGED1";
        Color = ButtonType.Success;
        Style = ButtonVisualStyle.SolidFill;
    }
}