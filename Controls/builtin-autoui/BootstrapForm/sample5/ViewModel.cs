public class ViewModel : DotvvmViewModelBase
{
    public CustomerModel Customer { get; set; } = new();

    public bool AgreeWithConditions { get; set; }
}

public class CustomerModel 
{
    [Display(Name = "Person or company name")]
    public string Name { get; set; }

    [Display(Name = "Is company")]
    public bool IsCompany { get; set; }
}
