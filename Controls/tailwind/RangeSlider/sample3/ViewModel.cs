using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.RangeSlider.sample3
{
    public class ViewModel : DotvvmViewModelBase
    {
        public bool IsEnabled { get; set; } = true;

        public double TemperatureFrom { get; set; } = 18;

        public double TemperatureTo { get; set; } = 24.5;
    }
}
