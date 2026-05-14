using DotVVM.Controls.Tailwind.Controls;
using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public ListItemData[] TeamMembers { get; set; } =
    [
        new("Alice Johnson", "Engineering Lead", TailwindColor.Success, "Active", "/profile/alice", ListItemLinkMode.Row),
        new("Bob Smith", "Product Designer", TailwindColor.Primary, "Review", "/profile/bob", null),
        new("Carol White", "QA Engineer", TailwindColor.Warning, "Away", "/profile/carol", null),
        new("David Brown", "Backend Developer", TailwindColor.Danger, "Offline", "/profile/david", null),
    ];
}

public record ListItemData(string Title, string Text, TailwindColor BadgeColor, string BadgeText, string? Href, ListItemLinkMode? LinkMode);

