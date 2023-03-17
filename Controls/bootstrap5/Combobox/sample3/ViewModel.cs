public class ViewModel : DotvvmViewModelBase
{
    public List<ListItemModel> ComboBoxDataSource { get; set; } = new List<ListItemModel>
            {
                new ListItemModel()  { Value = 1, Text = "Too long text", Title = "Nice title",  },
                new ListItemModel()  { Value = 2, Text = "Text", Title = "Even nicer title" }
            };


    public int SelectedValue { get; set; }

    public class ListItemModel
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
    }
}