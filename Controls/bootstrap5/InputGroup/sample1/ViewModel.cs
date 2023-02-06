public class ViewModel : ViewModelBase
{
    public bool RadioButtonValue { get; set; }
    public string Text { get; set; } = "Text";
    public bool Checked { get; set; } = false;

    public ButtonType Type { get; set; } = ButtonType.Warning;

    public int SelectedValue { get; set; }
    public List<ListItem> ComboBoxDataSource { get; set; } = new()
        {
            new() { Value = 1, Text = "Too long text", Title = "Nice title" },
            new() { Value = 2, Text = "Text", Title = "Even nicer title" }
        };

    public class ListItem
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
    }
    public List<ListItemDto> ItemDataSet { get; set; } = new()
        {
            new() { Text = "Page One", ParamId = 0, Enabled = true, IsSelected = true, NavigateUrl = "https://www.google.com/" },
            new() { Text = "Page Two", ParamId = 1, NavigateUrl = "https://www.dotvvm.com/" },
            new() { Text = "Page Three", ParamId = 3, Enabled = false, NavigateUrl = "https://www.riganti.cz/" }
        };
}