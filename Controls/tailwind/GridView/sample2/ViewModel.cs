using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Controls.Tailwind.Controls;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.GridView.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public FilteredGridViewDataSet<Order> Orders { get; set; } = new()
        {
            PagingOptions = { PageSize = 4 },
            SortingOptions = { SortExpression = nameof(Order.Customer) },
            FilteringOptions =
            {
                // filter by a property which is not displayed in the grid
                AdditionalFilters = [new("Country", o => o.Country)]
            }
        };

        public override Task PreRender()
        {
            if (Orders.IsRefreshRequired)
            {
                Orders.LoadFromQueryable(GetOrders());
            }
            return base.PreRender();
        }

        private static IQueryable<Order> GetOrders() => new List<Order>
        {
            new() { Customer = "John Doe", Country = "USA", Total = 120.5m, CreatedDate = new DateTime(2026, 1, 5), IsPaid = true },
            new() { Customer = "Jane Smith", Country = "UK", Total = 89.9m, CreatedDate = new DateTime(2026, 2, 12), IsPaid = false },
            new() { Customer = "Jan Novák", Country = "Czech Republic", Total = 340m, CreatedDate = new DateTime(2026, 3, 1), IsPaid = true },
            new() { Customer = "Anna Müller", Country = "Germany", Total = 15.25m, CreatedDate = new DateTime(2026, 3, 18), IsPaid = true },
            new() { Customer = "Bob Johnson", Country = "USA", Total = 560m, CreatedDate = new DateTime(2026, 4, 2), IsPaid = false },
            new() { Customer = "Alice Brown", Country = "UK", Total = 72.4m, CreatedDate = new DateTime(2026, 4, 20), IsPaid = true }
        }.AsQueryable();
    }

    public class Order
    {
        public string Customer { get; set; }

        public string Country { get; set; }

        public decimal Total { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsPaid { get; set; }
    }
}
