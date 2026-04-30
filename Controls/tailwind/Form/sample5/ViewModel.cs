using DotVVM.Framework.ViewModel;
using System.Collections.Generic;

public class ViewModel : DotvvmViewModelBase
{
    public string FullName { get; set; } = "";
    public string SelectedCountry { get; set; } = "";
    public bool Subscribe { get; set; }

    public List<CountryDto> Countries { get; set; } = new()
    {
        new() { Code = "us", Name = "United States" },
        new() { Code = "uk", Name = "United Kingdom" },
        new() { Code = "de", Name = "Germany" },
        new() { Code = "fr", Name = "France" },
        new() { Code = "jp", Name = "Japan" }
    };
}

public class CountryDto
{
    public string Code { get; set; } = "";
    public string Name { get; set; } = "";
}

