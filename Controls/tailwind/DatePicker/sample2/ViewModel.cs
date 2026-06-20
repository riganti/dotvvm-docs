using DotVVM.Framework.ViewModel;
using System;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.DatePicker.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public bool PickerEnabled { get; set; } = true;

        public DateTime? VacationDate { get; set; } = new DateTime(2026, 8, 10);

        public DateTime? OfficeHours { get; set; } = new DateTime(2026, 1, 1, 10, 0, 0);

        public DateTime? MinDate { get; set; } = new DateTime(2026, 8, 1);

        public DateTime? MaxDate { get; set; } = new DateTime(2026, 8, 31);

        public int MinHours { get; set; } = 8;

        public int MaxHours { get; set; } = 18;
    }
}
