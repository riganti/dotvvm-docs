using DotVVM.Controls.Tailwind.Controls;
using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public double BindableColorValue { get; set; } = 50;
    public TailwindColor SelectedColor { get; set; } = TailwindColor.Primary;

    public void SetColor(int colorValue)
    {
        SelectedColor = (TailwindColor)colorValue;
    }
}
