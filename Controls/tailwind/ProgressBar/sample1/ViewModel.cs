using DotVVM.Framework.ViewModel;

public class  ViewModel : DotvvmViewModelBase
{
    public double Progress { get; set; } = 10;

    public void IncreaseProgress()
    {
        Progress = Math.Min(100, Progress + 10);
    }
}