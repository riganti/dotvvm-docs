using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public int Progress { get; set; } = 30;
    
    public void IncreaseProgress()
    {
        if (Progress < 100) Progress += 10;
    }
    
    public void DecreaseProgress()
    {
        if (Progress > 0) Progress -= 10;
    }
    
    public void ResetProgress()
    {
        Progress = 0;
    }
}

