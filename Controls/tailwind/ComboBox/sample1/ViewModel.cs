using DotVVM.Framework.ViewModel;

public class  ViewModel : DotvvmViewModelBase
{
    public string SelectedOption { get; set; } = "";

    public List<OptionItem> Options { get; set; } = new()
    {
        new() { Text = "Option A", Value = "a" },
        new() { Text = "Option B", Value = "b" },
        new() { Text = "Option C", Value = "c" }
    };

    public class OptionItem
    {
        public string Text { get; set; } = "";
        public string Value { get; set; } = "";
    }
}