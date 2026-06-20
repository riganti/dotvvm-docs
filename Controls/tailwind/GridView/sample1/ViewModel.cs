using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.GridView.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string SelectedCustomerName { get; set; } = "(none)";

        public GridViewDataSet<CustomerDto> Customers { get; set; } = new GridViewDataSet<CustomerDto>()
        {
            PagingOptions = { PageSize = 4 },
            SortingOptions = { SortExpression = nameof(CustomerDto.Name) }
        };

        public override Task PreRender()
        {
            if (Customers.IsRefreshRequired)
            {
                Customers.LoadFromQueryable(GetCustomers().AsQueryable());
            }

            return base.PreRender();
        }

        public void SelectCustomer(CustomerDto customer)
        {
            SelectedCustomerName = customer.Name;
        }

        private static List<CustomerDto> GetCustomers()
        {
            return new List<CustomerDto>
            {
                new CustomerDto { Id = 1, Name = "Alice Johnson", Department = "Engineering" },
                new CustomerDto { Id = 2, Name = "Bob Smith", Department = "Design" },
                new CustomerDto { Id = 3, Name = "Carol White", Department = "QA" },
                new CustomerDto { Id = 4, Name = "David Brown", Department = "Product" },
                new CustomerDto { Id = 5, Name = "Eva Green", Department = "Support" },
                new CustomerDto { Id = 6, Name = "Frank Miller", Department = "Sales" }
            };
        }
    }

    public class CustomerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }
    }
}
