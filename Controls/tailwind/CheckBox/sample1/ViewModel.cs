using DotVVM.Framework.ViewModel;

public class  ViewModel : DotvvmViewModelBase
{
    public bool Agreed { get; set; } = false;
    public List<string> SelectedFruits { get; set; } = new() { "apple" };
}