using System.Collections.Generic;
using DotVVM.Controls.Tailwind;
using DotVVM.Controls.Tailwind.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.List.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string SelectedTask { get; set; } = "(none)";

        public List<TaskRow> Tasks { get; set; } = new List<TaskRow>
        {
            new TaskRow { Title = "Review pull request", StatusText = "Ready", StatusColor = TailwindColor.Info },
            new TaskRow { Title = "Deploy release", StatusText = "Blocked", StatusColor = TailwindColor.Warning },
            new TaskRow { Title = "Verify metrics", StatusText = "Done", StatusColor = TailwindColor.Success }
        };

        public List<SectionRow> Sections { get; set; } = new List<SectionRow>
        {
            new SectionRow { Title = "Backlog", Description = "Open the backlog section.", Url = "#backlog" },
            new SectionRow { Title = "Release", Description = "Jump to the release notes section.", Url = "#release" }
        };

        public void SelectTask(string taskName)
        {
            SelectedTask = taskName;
        }
    }

    public class TaskRow
    {
        public string Title { get; set; }

        public string StatusText { get; set; }

        public TailwindColor StatusColor { get; set; }
    }

    public class SectionRow
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
    }
}
