using DotVVM.Framework.ViewModel;
using System;

public class ViewModel : DotvvmViewModelBase
{
    public List<string> MenuItems { get; set; } = new() { "Edit", "Duplicate", "Archive", "Delete" };
    
    public string LastClicked { get; set; } = "(none)";
    
    public void ItemClicked(string item)
    {
        LastClicked = $"{item} clicked at {DateTime.Now:HH:mm:ss}";
    }
}

