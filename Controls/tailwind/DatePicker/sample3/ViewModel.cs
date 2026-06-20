using DotVVM.Framework.ViewModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.DatePicker.sample3
{
    public class ViewModel : DotvvmViewModelBase
    {
        public DateTime? StartDate { get; set; } = new DateTime(2026, 9, 15, 13, 30, 0);

        [Required]
        public DateTime? ReminderTime { get; set; }

        public bool Saved { get; set; }

        public void Save()
        {
            Saved = true;
        }
    }
}
