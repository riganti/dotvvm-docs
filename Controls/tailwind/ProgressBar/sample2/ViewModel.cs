using DotVVM.Controls.Tailwind;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.ProgressBar.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public double Progress { get; set; } = 40;

        public TailwindColor SelectedType { get; set; } = TailwindColor.Primary;

        public void Increase()
        {
            Progress = Progress >= 100 ? 100 : Progress + 10;
        }

        public void Reset()
        {
            Progress = 0;
        }

        public void SetType(int type)
        {
            SelectedType = (TailwindColor)type;
        }
    }
}
