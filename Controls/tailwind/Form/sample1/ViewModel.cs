using DotVVM.Framework.ViewModel;

public class  ViewModel : DotvvmViewModelBase
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";

    public void Save()
    {
        // save logic
    }

    public void Reset()
    {
        FirstName = "";
        LastName = "";
        Email = "";
    }
}