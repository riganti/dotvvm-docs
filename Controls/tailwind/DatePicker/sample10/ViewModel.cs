using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public string? AppointmentDate { get; set; }
    public bool AppointmentSaved { get; set; }

    public void SaveAppointment()
    {
        AppointmentSaved = !string.IsNullOrEmpty(AppointmentDate);
    }
}

