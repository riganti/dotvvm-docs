public class ViewModel : DotvvmViewModelBase
{
    public CustomerModel Customer { get; set; } = new();
}

public class CustomerModel 
{
    [Display(Name = "Person or company name")]
    public string Name { get; set; }

    [Display(Name = "Is company")]
    public bool IsCompany { get; set; }


    [Display(Name = "E-mail address")]
    public string Email { get; set; }

    [Display(Name = "Phone number")]
    public string Phone { get; set; } 
}
