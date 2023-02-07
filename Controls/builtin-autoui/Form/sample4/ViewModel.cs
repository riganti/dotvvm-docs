public class ViewModel : DotvvmViewModelBase
{
    public CustomerModel Customer { get; set; } = new();
}

public class CustomerModel 
{
    [Visible(ViewNames = "List")]
    public string CountryName { get; set; }

    [Visible(ViewNames = "Insert | Edit")]
    public int CountryId { get; set; }
}
