public class ProgressBarsViewModel
{
    public double Progress { get; set; } = 10;

    public void IncreaseProgress()
    {
        Progress = Math.Min(100, Progress + 10);
    }
}