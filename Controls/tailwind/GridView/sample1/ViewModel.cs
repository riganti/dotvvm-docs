using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ViewModel : DotvvmViewModelBase
{
    public GridViewDataSet<CustomerDto> Customers { get; set; } = new GridViewDataSet<CustomerDto>()
    {
        SortingOptions = { SortExpression = nameof(CustomerDto.Id) }
    };

    private static readonly List<CustomerDto> AllCustomers = new()
    {
        new() { Id = 1, Name = "John Doe", Email = "john@example.com" },
        new() { Id = 2, Name = "Jane Smith", Email = "jane@example.com" },
        new() { Id = 3, Name = "Bob Johnson", Email = "bob@example.com" },
        new() { Id = 4, Name = "Alice Brown", Email = "alice@example.com" },
        new() { Id = 5, Name = "Charlie Davis", Email = "charlie@example.com" },
    };

    public override Task PreRender()
    {
        if (Customers.IsRefreshRequired)
        {
            Customers.LoadFromQueryable(AllCustomers.AsQueryable());
        }
        return base.PreRender();
    }

    public void Edit(CustomerDto customer)
    {
        // Edit logic here
    }
}

public class CustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}