public class CheckBoxViewModel
{
    public bool Agreed { get; set; } = false;
    public List<string> SelectedFruits { get; set; } = new() { "apple" };
}