using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.TextBoxGroup.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string Prompt { get; set; }

        public string Mode { get; set; } = "Chat";

        public List<string> Models { get; set; } = ["Auto", "Fast", "Thorough"];

        public string SelectedModel { get; set; } = "Auto";

        public string Result { get; set; }

        public void Send()
        {
            Result = $"Sent \"{Prompt}\" to the {SelectedModel} model in {Mode} mode.";
            Prompt = "";
        }
    }
}
