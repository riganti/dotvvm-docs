using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.Rating.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public double Score { get; set; } = 3.5;

        public double WholeScore { get; set; } = 2;

        public bool IsEnabled { get; set; } = true;

        public int ChangeCount { get; set; }

        public void OnChanged()
        {
            ChangeCount++;
        }
    }
}
