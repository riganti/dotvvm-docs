public class ViewModel : DotvvmViewModelBase
{
    private static IQueryable<Customer> FakeDb()
    {
        return new[]
        {
            new Customer() { Id = 0, Name = "Dani Michele", BirthDate = new DateTime(1990, 2, 3), IsVIP = false }, 
            new Customer() { Id = 1, Name = "Elissa Malone", BirthDate = new DateTime(1984, 10, 6), IsVIP = false }, 
            new Customer() { Id = 2, Name = "Raine Damian", BirthDate = new DateTime(1988, 5, 17), IsVIP = false },
            new Customer() { Id = 3, Name = "Gerrard Petra", BirthDate = new DateTime(1994, 1, 23), IsVIP = false }, 
            new Customer() { Id = 4, Name = "Clement Ernie", BirthDate = new DateTime(1991, 4, 29), IsVIP = true }, 
            new Customer() { Id = 5, Name = "Rod Fred", BirthDate = new DateTime(1987, 6, 5), IsVIP = true }
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

    // show date only
    [DisplayFormat(DataFormatString = "d")]
    public DateTime BirthDate { get; set; }

    public bool IsVIP { get; set; }
}