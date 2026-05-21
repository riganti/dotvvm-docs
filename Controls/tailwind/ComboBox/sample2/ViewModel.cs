using DotVVM.Framework.ViewModel;
using System.Collections.Generic;

public class ViewModel : DotvvmViewModelBase
{
    public string SelectedOptionForm { get; set; } = "";

    public List<OptionDto> Options { get; set; } = new()
    {
        new() { Text = "Czech Republic", Value = "CZE" },
        new() { Text = "Germany", Value = "DEU" },
        new() { Text = "United States ", Value = "USA" }
    };
}

public class OptionDto
{
    public string Text { get; set; } = "";
    public string Value { get; set; } = "";
}

