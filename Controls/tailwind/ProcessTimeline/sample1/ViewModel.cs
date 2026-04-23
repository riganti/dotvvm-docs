using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public int CurrentStep { get; set; } = 1;

    public void Next() { if (CurrentStep < 4) CurrentStep++; }
    public void Prev() { if (CurrentStep > 1) CurrentStep--; }
}
