using DotVVM.BusinessPack.Controls;
public class ViewModel : DotvvmViewModelBase
{
    public BusinessPackDataSet<Customer> Customers { get; set; } = new BusinessPackDataSet<Customer>
        {
            PagingOptions = { PageSize = 10 }
        };
    
    
    public string OrderFilter { get; set; }

    public override Task PreRender()
    {
        // you can initialize default filters
        if (!Context.IsPostBack) 
        {
            Customers.FilteringOptions =
            {
                FilterGroup = new FilterGroup()
                {
                    Filters = new List<FilterBase>()
                    {                        
                        new FilterCondition() { FieldName = "Name", Operator = FilterOperatorType.Contains, Value = "1" }
                    },
                    Logic = FilterLogicType.And
                }
            }
        }
        
        // refresh data
        if (Customers.IsRefreshRequired)
        {
            Customers.LoadFromQueryable(GetQueryable(15));
        }

        return base.PreRender();
    }

    public void OnOrderFilterChanged()
    {
        // do your logic
        Customers.RequestRefresh();
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