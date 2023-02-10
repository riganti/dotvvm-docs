public class ViewModel : DotvvmViewModelBase
{
    private static IQueryable<Customer> FakeDb()
    {
        return new[]
        {
            new Customer() { Id = 0, Name = "Dani Michele", CountryName = "CZ", CountryId = 1 }, 
            new Customer() { Id = 1, Name = "Elissa Malone", CountryName = "PL", CountryId = 2 }, 
            new Customer() { Id = 2, Name = "Raine Damian", CountryName = "PL", CountryId = 2 },
            new Customer() { Id = 3, Name = "Gerrard Petra", CountryName = "DE", CountryId = 3 }, 
            new Customer() { Id = 4, Name = "Clement Ernie", CountryName = "CZ", CountryId = 1 }, 
            new Customer() { Id = 5, Name = "Rod Fred", CountryName = "DE", CountryId = 3 }
        }.AsQueryable();
    }

    public GridViewDataSet<Customer> Customers { get; set; } = new GridViewDataSet<Customer>() { PagingOptions = { PageSize = 4} };

    public override Task PreRender()
    {
        if (Customers.IsRefreshRequired)
        {
            var queryable = FakeDb();
            Customers.LoadFromQueryable(queryable);
        }
        return base.PreRender();
    }
}

public class Customer
{
    // do not show this field
    [Display(AutoGenerateField = false)]
    public int Id { get; set; }

    public string Name { get; set; }

    // will be shown only when ViewName == "List"
    [Visible(ViewNames = "List")]
    public string CountryName { get; set; }

    // will be shown only when ViewName == "Insert" || ViewName == "Edit"
    [Visible(ViewNames = "Insert | Edit")]
    public int CountryId { get; set; }
}