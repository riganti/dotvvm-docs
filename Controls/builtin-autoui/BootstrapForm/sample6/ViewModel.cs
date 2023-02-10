public class ViewModel : DotvvmViewModelBase
{
    public CustomerModel Customer { get; set; } = new();

    public void LookupCompanyAddress() 
    {
        // TODO
    }
}

public class CustomerModel 
{
    public string Name { get; set; }

    [Display(Name = "Is company")]
    public bool IsCompany { get; set; }

    [Display(Name = "Company number")]
    public string CompanyNumber { get; set; }

    [Display(Name = "International customer")]
    public bool IsInternational { get; set; }

    [Display(Name = "Country")]
    public string CountryName { get; set; }
}
