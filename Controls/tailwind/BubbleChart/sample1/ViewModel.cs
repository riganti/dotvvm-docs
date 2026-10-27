using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.BubbleChart.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<City> Cities { get; set; } = new() { new() { Name = "Brno", Population = 0.38, Gdp = 38, Size = 8 }, new() { Name = "Prague", Population = 1.36, Gdp = 62, Size = 18 }, new() { Name = "Vienna", Population = 2.0, Gdp = 57, Size = 22 } };
    }
    public class City { public string Name { get; set; } public double Population { get; set; } public double Gdp { get; set; } public double Size { get; set; } }
}
