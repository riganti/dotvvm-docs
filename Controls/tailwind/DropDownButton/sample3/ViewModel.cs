using DotVVM.Framework.ViewModel;
using System;

public class ViewModel : DotvvmViewModelBase
{
    public string LastAction { get; set; } = "(none)";
    
    public void ActionClicked()
    {
        LastAction = "Action clicked at " + DateTime.Now.ToString("HH:mm:ss");
    }
}

