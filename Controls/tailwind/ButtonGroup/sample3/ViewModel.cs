using DotVVM.Framework.ViewModel;
using System;

public class ViewModel : DotvvmViewModelBase
{
    public List<string> Actions { get; set; } = new() { "Copy", "Cut", "Paste" };
    
    public string LastClicked { get; set; } = "(none)";
    
    public void ItemClicked(string item)
    {
        LastClicked = $"{item} clicked at {DateTime.Now:HH:mm:ss}";
    }
}

