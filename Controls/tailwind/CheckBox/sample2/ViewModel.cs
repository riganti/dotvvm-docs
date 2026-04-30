using DotVVM.Framework.ViewModel;
using System.Collections.Generic;

public class ViewModel : DotvvmViewModelBase
{
    public List<string> SelectedFruits { get; set; } = new() { "apple" };
}

