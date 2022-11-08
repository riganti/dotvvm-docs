public class ViewModel : DotvvmViewModelBase
{
    public double Value { get; set; } = 1000;
    public int ValueChanges { get; set; } = 0;
    public bool Enabled { get; set; } = true;
}
