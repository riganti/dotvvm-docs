using DotVVM.Controls.Tailwind.Controls;
using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public AlertType SelectedAlertType { get; set; } = AlertType.Info;


    public void SetAlertType(int typeValue)
    {
        SelectedAlertType = (AlertType)typeValue;
    }
}
