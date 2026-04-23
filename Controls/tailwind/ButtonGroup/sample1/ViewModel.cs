public class ButtonGroupsViewModel
{
    public List<string> Actions { get; set; } = new() { "Copy", "Paste", "Cut" };
    public string LastClicked { get; set; } = "";

    public void ItemClicked(string item)
    {
        LastClicked = item;
    }
}