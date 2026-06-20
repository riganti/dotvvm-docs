using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.Range.sample3
{
    public class ViewModel : DotvvmViewModelBase
    {
        public bool IsEnabled { get; set; } = true;

        public double Temperature { get; set; } = 21.5;
    }
}
