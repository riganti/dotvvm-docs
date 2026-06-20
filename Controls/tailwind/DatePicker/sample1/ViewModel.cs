using DotVVM.Framework.ViewModel;
using System;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.DatePicker.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public DateTime? MeetingDate { get; set; } = new DateTime(2026, 7, 1);

        public DateTime? MeetingTime { get; set; } = new DateTime(2026, 1, 1, 9, 30, 0);

        public DateTime? PublishedAt { get; set; } = new DateTime(2026, 7, 1, 14, 0, 0);
    }
}
