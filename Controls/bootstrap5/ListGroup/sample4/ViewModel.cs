public class ViewModel : DotvvmViewModelBase
{
    public List<string> Values => new List<string>() { "one", "two", "three" };

    public string SelectedValue { get; set; }


    public void Select(string value)
    {
        SelectedValue = value;
    }
}