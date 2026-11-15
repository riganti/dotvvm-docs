using System;
using System.Collections.Generic;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.GridView.sample3
{
    public class ViewModel : DotvvmViewModelBase
    {
        public GridViewDataSet<TaskItem> Tasks { get; set; } = new()
        {
            RowEditOptions = { PrimaryKeyPropertyName = nameof(TaskItem.Id) },
            Items =
            [
                new() { Id = 1, Title = "Prepare the release", DueAt = new DateTime(2026, 11, 20, 10, 0, 0), Tags = [TaskTag.Urgent] },
                new() { Id = 2, Title = "Write the blog post", DueAt = new DateTime(2026, 11, 25, 14, 30, 0), Tags = [TaskTag.Marketing, TaskTag.Docs] },
                new() { Id = 3, Title = "Update the docs", DueAt = new DateTime(2026, 11, 18, 9, 0, 0), IsDone = true, Tags = [TaskTag.Docs] }
            ]
        };
    }

    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime DueAt { get; set; }

        public bool IsDone { get; set; }

        public List<TaskTag> Tags { get; set; } = [];
    }

    public enum TaskTag
    {
        Urgent,
        Marketing,
        Docs
    }
}
