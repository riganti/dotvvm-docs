public class ViewModel : DotvvmViewModelBase
{
    public string TitleHtml { get; set; } = "<h3>Title with html</h3>";
    public string ContentHtml { get; set; } = "This <i>content</i> uses <b>html</b> <u>tags</u>";
}