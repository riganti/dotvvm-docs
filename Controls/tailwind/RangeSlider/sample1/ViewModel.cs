using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.RangeSlider.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public double PriceFrom { get; set; } = 20;

        public double PriceTo { get; set; } = 80;

        public double BudgetFrom { get; set; } = 100;

        public double BudgetTo { get; set; } = 400;
    }
}
