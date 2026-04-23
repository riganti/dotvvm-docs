using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public string FullName { get; set; } = "";
    public string SelectedCountry { get; set; } = "";
    public string SelectedRole { get; set; } = "";

    public List<CountryDto> Countries { get; set; } = new()
    {
        new() { Code = "us", Name = "United States" },
        new() { Code = "uk", Name = "United Kingdom" },
        new() { Code = "de", Name = "Germany" },
    };

    public List<string> Roles { get; set; } = new() { "Admin", "Editor", "Viewer" };

    public void Register() { }
}

public class CountryDto
{
    public string Code { get; set; } = "";
    public string Name { get; set; } = "";
}
