using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public bool CheckboxValue { get; set; }
    public bool DisabledCheckboxUnchecked { get; set; } = false;
    public bool DisabledCheckboxChecked { get; set; } = true;
}