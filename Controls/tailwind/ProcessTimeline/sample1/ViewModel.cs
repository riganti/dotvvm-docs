using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.ProcessTimeline.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public int CurrentStep { get; set; } = 2;

        public void Previous()
        {
            if (CurrentStep > 1)
            {
                CurrentStep--;
            }
        }

        public void Next()
        {
            if (CurrentStep < 4)
            {
                CurrentStep++;
            }
        }
    }
}
