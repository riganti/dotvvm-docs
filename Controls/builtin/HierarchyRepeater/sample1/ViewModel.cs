using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.builtin.Repeater.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public PageViewModel[] Pages { get; set; } = {
            new PageViewModel("Products", "/products", new PageViewModel[] {
                new PageViewModel("Bootstrap", "/products/bootstrap-for-dotvvm", new PageViewModel[0]),
                new PageViewModel("Bussiness Pack", "/products/dotvvm-business-pack", new PageViewModel[] {
                    new PageViewModel("Documentation", "/docs/latest/pages/business-pack/getting-started", new PageViewModel[0])
                }),
            }),
            new PageViewModel("Forum", "https://forum.dotvvm.com", new PageViewModel[0]),
            new PageViewModel("About", "/about", new PageViewModel[0]),
        };
    }

    public record PageViewModel(string Title, string Link, PageViewModel[] ChildPages);
}
