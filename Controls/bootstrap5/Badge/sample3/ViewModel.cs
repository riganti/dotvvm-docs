public class ViewModel : DotvvmViewModelBase
{
    public int Clicks { get; set; } = 0;

    public void Increment()
    {
        Clicks++;
    }
}
