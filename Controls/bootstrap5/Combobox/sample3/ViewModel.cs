public class ViewModel : DotvvmViewModelBase
{
    public List<ListItemModel> ComboBoxDataSource { get; set; } 

        public override Task Init()
        {
            foreach (var listItem in GetData())
            {
                ComboBoxDataSource.Add(listItem);
            }
            return base.Init();
        }

        private static List<ListItemModel> GetData()
        {
            return new List<ListItemModel>
            {
                new ListItemModel()  { Value = 1, Text = "Too long text", Title = "Nice title" },
                new ListItemModel()  { Value = 2, Text = "Text", Title = "Even nicer title" }
            };
        }

        public class ListItemModel
        {
            public int Value { get; set; }
            public string Text { get; set; }
            public string Title { get; set; }
        }
}