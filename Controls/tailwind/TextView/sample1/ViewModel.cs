using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.TextView.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string Note { get; set; } = "Contact us at info@dotvvm.com\nor visit https://www.dotvvm.com.\n\n<b>HTML tags</b> are displayed as text.";

        public string ReleaseNotes { get; set; } = "## Release notes\n\n* **Bold** and _italic_ text\n* Links: https://www.dotvvm.com";
    }
}
