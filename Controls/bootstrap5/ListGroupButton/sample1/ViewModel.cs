public class ViewModel : DotvvmViewModelBase
{
    public string Text { get; set; } = "Data-bound text of the item.";
    public bool Enabled { get; set; }
    public bool IsSelected { get; set; }
    public void ChangeListGroup()
    {
        Enabled = !Enabled;
        IsSelected = !IsSelected;
        Text = "Changed";
    }

}