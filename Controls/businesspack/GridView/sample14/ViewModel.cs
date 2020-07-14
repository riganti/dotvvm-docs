using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.ViewModel;

public class ViewModel : DotvvmViewModelBase
{
    public BusinessPackDataSet<Customer> Customers { get; set; } = new BusinessPackDataSet<Customer>
        {
            PagingOptions = { PageSize = 10 }
        };

    public List<int> SelectedCustomerIds { get; set; } = new List<int>();

    public override Task PreRender()
    {
        if (Customers.IsRefreshRequired)
        {
            Customers.LoadFromQueryable(GetQueryable(15));
        }

        return base.PreRender();
    }

    private IQueryable<Customer> GetQueryable(int size)
    {
        var numbers = new List<Customer>();
        for (var i = 0; i < size; i++)
        {
            numbers.Add(new Customer { Id = i + 1, Name = $"Customer {i + 1}", BirthDate = DateTime.Now.AddYears(-i), Orders = i });
        }
        return numbers.AsQueryable();
    }
}