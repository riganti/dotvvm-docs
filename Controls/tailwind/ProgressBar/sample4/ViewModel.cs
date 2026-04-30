using DotVVM.Framework.ViewModel;
using DotVVM.Controls.Tailwind.Controls;

public class ViewModel : DotvvmViewModelBase
{
    public TailwindColor SelectedColor { get; set; } = TailwindColor.Primary;
    
    public void SetColor(int colorValue)
    {
        SelectedColor = (TailwindColor)colorValue;
    }
}

