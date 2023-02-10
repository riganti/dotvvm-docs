public class ViewModel : DotvvmViewModelBase
{
    public CustomerModel Customer { get; set; } = new();
}

public class CustomerModel 
{
    [Display(Name = "Person or company name", GroupName = "BasicInfo")]
    public string Name { get; set; }

    [Display(Name = "Is company", GroupName = "BasicInfo")]
    public bool IsCompany { get; set; }


    [Display(Name = "E-mail address", GroupName = "ContactInfo")]
    public string Email { get; set; }

    [Display(Name = "Phone number", GroupName = "ContactInfo")]
    public string Phone { get; set; } 
}
