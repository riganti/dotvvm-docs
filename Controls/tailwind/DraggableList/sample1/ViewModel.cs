using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.DraggableList.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<TaskItem> Tasks { get; set; } =
        [
            new() { Title = "Write the specification", Priority = "High" },
            new() { Title = "Implement the feature", Priority = "Medium" },
            new() { Title = "Write the documentation", Priority = "Low" }
        ];

        public bool IsEnabled { get; set; } = true;

        public string Order { get; set; }

        public void OnDrop()
        {
            Order = string.Join(", ", Tasks.Select(t => t.Title));
        }
    }

    public class TaskItem
    {
        public string Title { get; set; }

        public string Priority { get; set; }
    }
}
