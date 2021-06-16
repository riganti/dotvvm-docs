using System;
using System.Collections.Generic;
using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.businesspack.DatePicker.sample3
{
    public class ViewModel : DotvvmViewModelBase
    {
        public DateTime SelectedDate { get; set; } = DateTime.Now;

        public List<DateRestriction> Restrictions { get; set; } = new List<DateRestriction>()
        {
            //These will prevent saturdays and sundays from being selected
            new DayOfWeekRestriction() { DayOfWeek = DayOfWeek.Saturday },
            new DayOfWeekRestriction() { DayOfWeek = DayOfWeek.Sunday },
            //This will prevent selection of all dates from the 1st of June to the 6th of June of current year
            new DateRangeRestriction() 
            { 
                StartDate = new DateTime(DateTime.Today.Year, 6, 1), 
                EndDate = new DateTime(DateTime.Today.Year, 6, 7)
            }
        };
    }
}
