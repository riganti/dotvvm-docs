using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public string DeleteResult { get; set; } = "To be deleted";

    public void DeleteItem()
    {
        DeleteResult = "Item deleted successfully!";
    }
}

