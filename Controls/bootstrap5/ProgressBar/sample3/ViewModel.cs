public class ViewModel : DotvvmViewModelBase
{
    public double Width { get; set; } = 95;
    public BootstrapColor Color { get; set; } = BootstrapColor.Info;

    public void ChangeWidth()
    {
        var random = new Random();
        Width = random.Next(101);
    }

    public void ChangeColor()
    {
        var colors = Enum.GetValues(typeof(BootstrapColor)).Cast<BootstrapColor>().ToList();
        var random = new Random();
        var c = random.Next(colors.Count);
        Color = colors[c];
    }
}
