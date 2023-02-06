public class ViewModel : DotvvmViewModelBase
{
    public List<string> Strings { get; set; } = new List<string> { "one", "two", "three" };
    public string SelectedString { get; set; }
}