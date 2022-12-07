using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.bootstrap.Popover.sample5
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string TitleHtml { get; set; } = "<h3>Title with html</h3>";
        public string ContentHtml { get; set; } = "This <i>content</i> uses <b>html</b> <u>tags</u>";
    }
}