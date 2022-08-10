using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.businesspack.TreeView.sample6
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<TreeItem> SelectedFiles { get; set; } = new List<TreeItem>();

        public List<TreeItem> Files { get; set; } = new List<TreeItem> {
            new TreeItem {
                Name = "Documents",
                HasItems = true // LoadChildren is called when HasItems is true and Items are empty
            }
        };

        [AllowStaticCommand]
        public IEnumerable<TreeItem> LoadChildren(TreeItem parent)
        {
            yield return new TreeItem {
                Name = "Invoices",
                HasItems = true,
                Items = new List<TreeItem> {
                    new TreeItem { Name = "Invoice.pdf" }
                }
            };
        }
    }
}
