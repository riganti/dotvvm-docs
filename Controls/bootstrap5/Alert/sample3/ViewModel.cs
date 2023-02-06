public class ViewModel : MyViewModelBase
{
    
    public void DoSomething()
    {
        ExecuteWithAlert(() =>
        {
            // the action
        });
    }
}