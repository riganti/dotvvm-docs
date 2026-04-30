using DotVVM.Framework.ViewModel;
using System.Collections.Generic;

public class ViewModel : DotvvmViewModelBase
{
    public string SelectedOptionSmall { get; set; } = "";
    public string SelectedOptionDefault { get; set; } = "";
    public string SelectedOptionLarge { get; set; } = "";
    public string SelectedOptionExtraLarge { get; set; } = "";

    public List<OptionDto> Options { get; set; } = new()
    {
        new() { Text = "Option A", Value = "a" },
        new() { Text = "Option B", Value = "b" },
        new() { Text = "Option C", Value = "c" }
    };
}

public class OptionDto
{
    public string Text { get; set; } = "";
    public string Value { get; set; } = "";
}

