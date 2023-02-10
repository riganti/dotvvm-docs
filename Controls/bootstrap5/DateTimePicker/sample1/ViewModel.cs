
public class ViewModel : DotvvmViewModelBase
{
    public Size Size { get; set; } = Size.Small;
    public DateTime SelectedValue { get; set; } = DateTime.UtcNow;
    public DateTime MinValue { get; set; } = DateTime.UtcNow.AddDays(-3);
    public DateTime MaxValue { get; set; } = DateTime.UtcNow.AddDays(3);
    public int Counter { get; set; } = 0;
    
    public ViewModel()
    {
    }

    public void ChangeSize()
    {
        if (Size == Size.Small)
        {
            Size = Size.Default;
        }
        else if (Size == Size.Default)
        {
            Size = Size.Large;
        }
        else if (Size == Size.Large)
        {
            Size = Size.Small;
        }
    }
}
