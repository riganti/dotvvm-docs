public class ViewModel : DotvvmViewModelBase
{
    public bool IsCollapsed { get; set; } = true;
    public bool IsCollapsed2 { get; set; } = true;
    public bool IsCollapsed3 { get; set; } = true;
    public string BodyText { get; set; } = "Bindable Body Text";
    public string HeaderText { get; set; } = "Bindable Header Text";
}
