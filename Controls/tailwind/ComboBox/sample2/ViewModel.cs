using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.ComboBox.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        private static readonly List<Product> AllProducts =
        [
            new() { Id = 1, Name = "Keyboard", Description = "Mechanical keyboard" },
            new() { Id = 2, Name = "Mouse", Description = "Wireless mouse" },
            new() { Id = 3, Name = "Monitor", Description = "27-inch 4K monitor" },
            new() { Id = 4, Name = "Headset", Description = "Noise-cancelling headset" },
            new() { Id = 5, Name = "Webcam", Description = "Full HD webcam" }
        ];

        // only a few items are loaded initially, the rest is found by the search
        public List<Product> Products { get; set; } = AllProducts.Take(2).ToList();

        public Product SelectedProduct { get; set; }

        [AllowStaticCommand]
        public static async Task<List<Product>> SearchProducts(string query)
        {
            await Task.Delay(200); // simulate a database query
            return AllProducts
                .Where(p => p.Name.Contains(query ?? "", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
