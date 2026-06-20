using DotVVM.Framework.ViewModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.Form.sample3
{
    public class ViewModel : DotvvmViewModelBase
    {
        [Required]
        public string UserName { get; set; }

        public string Password { get; set; }

        public List<RoleOption> Roles { get; set; } =
        [
            new() { Id = "author", Name = "Author" },
            new() { Id = "editor", Name = "Editor" },
            new() { Id = "admin", Name = "Administrator" }
        ];

        [Required]
        public string SelectedRole { get; set; }

        public bool RoleEnabled { get; set; } = true;

        [Required]
        public System.DateTime? ReminderTime { get; set; }

        public bool Submitted { get; set; }

        public void Submit()
        {
            Submitted = true;
        }
    }

    public class RoleOption
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
