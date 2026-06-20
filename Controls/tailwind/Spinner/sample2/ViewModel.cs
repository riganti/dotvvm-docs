using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.Spinner.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public bool IsLoading { get; set; }

        public void ToggleLoading()
        {
            IsLoading = !IsLoading;
        }
    }
}
