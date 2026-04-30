using DotVVM.Framework.ViewModel;
using System.ComponentModel.DataAnnotations;

public class ViewModel : DotvvmViewModelBase
{
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = "";

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; } = "";

    public string Street { get; set; } = "";
    public string Zip { get; set; } = "";
    public bool Subscribe { get; set; }
    public bool AgreeToTerms { get; set; }
    public bool Saved { get; set; }

    public void Save()
    {
        Saved = true;
    }

    public void Reset()
    {
        FirstName = LastName = Email = Street = Zip = "";
        Subscribe = AgreeToTerms = Saved = false;
    }
}
