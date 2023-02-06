public class ViewModel
{
    public List<ItemSectionModel> ItemSectionsSource { get; set; }

    public ViewModel()
    {
        ItemSectionsSource = new List<ItemSectionModel>()
        {
            new ItemSectionModel()
            {
                Text = "Riganti",
                Url = "https://riganti.cz/",
                Visible = false,
                IconType = Icons.Window_Desktop
            },
            new ItemSectionModel()
            {
                Text = "Google",
                Url = "https://www.google.com/",
                Selected = true,
                Visible = true,
                IconType = Icons.Window_Dock
            },
        };
    }
}


public class ItemSectionModel
{
    public string Text { get; set; }
    public string Url { get; set; }
    public Icons IconType { get; set; }
    public bool Visible { get; set; }
    public bool Selected { get; set; }
}