using System;
using DotVVM.Controls.Tailwind;
using DotVVM.Controls.Tailwind.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.Icon.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public HeroIcon[] Icons { get; set; } = Enum.GetValues<HeroIcon>();

        public IconType[] IconTypes { get; set; } = Enum.GetValues<IconType>();

        public HeroIcon SelectedIcon { get; set; } = HeroIcon.Sparkles;

        public IconType SelectedIconType { get; set; } = IconType.Outline;
    }
}
