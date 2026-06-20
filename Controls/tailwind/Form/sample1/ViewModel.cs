using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.Form.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string FirstName { get; set; } = "Anna";

        public string LastName { get; set; } = "Novak";

        public string Email { get; set; } = "anna@example.com";

        public bool Saved { get; set; }

        public void Save()
        {
            Saved = true;
        }
    }
}
