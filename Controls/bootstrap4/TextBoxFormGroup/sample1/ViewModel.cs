using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.bootstrap4.TextBoxFormGroup.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string LabelText { get; set; } = "Bound label";
        public string ValueText { get; set; } = "Bound value";
    }
}