using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.SkeletonCard.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public bool IsLoaded { get; set; }

        public async Task Load()
        {
            // simulate a slow data source
            await Task.Delay(1500);
            IsLoaded = true;
        }
    }
}
