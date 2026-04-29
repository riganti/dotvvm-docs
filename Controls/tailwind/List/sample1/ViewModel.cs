using DotVVM.Framework.ViewModel;

public class  ViewModel : DotvvmViewModelBase
{
    public List<ListItemData> Items { get; set; } = new()
    {
        new() { Title = "Alice Johnson", Text = "Engineering Lead", BadgeText = "Active", BadgeColor = TailwindColor.Success },
        new() { Title = "Bob Smith", Text = "Design", BadgeText = "Away", BadgeColor = TailwindColor.Warning }
    };

    public class ListItemData
    {
        public string Title { get; set; } = "";
        public string Text { get; set; } = "";
        public string BadgeText { get; set; } = "";
        public TailwindColor BadgeColor { get; set; } = TailwindColor.Default;
    }
}