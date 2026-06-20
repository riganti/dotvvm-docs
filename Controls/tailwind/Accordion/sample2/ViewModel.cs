using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public List<FaqItem> FaqItems { get; set; } = new()
    {
        new() { Question = "What is DotVVM?", Answer = "DotVVM is an MVVM framework for ASP.NET Core applications.", IsInitiallyExpanded = true },
        new() { Question = "Why use the Tailwind package?", Answer = "It provides ready-made Tailwind-styled controls for common UI patterns." },
        new() { Question = "Can items start opened?", Answer = "Yes. Bind ItemIsExpanded to a boolean property on each item." }
    };

    public class FaqItem
    {
        public string Question { get; set; } = "";
        public string Answer { get; set; } = "";
        public bool IsInitiallyExpanded { get; set; }
    }
}
