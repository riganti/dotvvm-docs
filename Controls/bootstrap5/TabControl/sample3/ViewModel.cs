public class ViewModel
{
    public List<TabData> Tabs { get; set; }

    public string Selected { get; set; }


    public ViewModel()
    {
        Tabs = new List<TabData>()
        {
            new TabData() { Id = 1, Name = "Tab 1", Description = "Tab one" },
            new TabData() { Id = 2, Name = "Tab 2", Description = "Tab two" },
            new TabData() { Id = 3, Name = "Tab 3", Description = "Tab three"},
            new TabData() { Id = 4, Name = "Tab 4", Description = "Tab four" }
        };
    }

    public void SelectTab(int index)
    {
        var selectedTab = Tabs.Single(t => t.Id == index);
        Selected = selectedTab.Id + " - " + selectedTab.Name + " - " + selectedTab.Description;
    }

}

public class TabData
{
    
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

}
