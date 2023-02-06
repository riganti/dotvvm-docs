public class ViewModel : DotvvmViewModelBase
{
    private static IQueryable<CustomerData> FakeDb()
    {
        return new[]
        {
            new CustomerData() { CustomerId = 1, Name = "John Doe", BirthDate = DateTime.Parse("1976-04-01"), Color = BootstrapColor.Danger },
            new CustomerData() { CustomerId = 2, Name = "John Deer", BirthDate = DateTime.Parse("1984-03-02"), Color = BootstrapColor.Light },
            new CustomerData() { CustomerId = 3, Name = "Johnny Walker", BirthDate = DateTime.Parse("1934-01-03"), Color = BootstrapColor.Light },
            new CustomerData() { CustomerId = 4, Name = "Jim Hacker", BirthDate = DateTime.Parse("1912-11-04"), Color = BootstrapColor.Light },
            new CustomerData() { CustomerId = 5, Name = "Joe E. Brown", BirthDate = DateTime.Parse("1947-09-05"), Color = BootstrapColor.Light },
            new CustomerData() { CustomerId = 6, Name = "Jim Harris", BirthDate = DateTime.Parse("1956-07-06"), Color = BootstrapColor.Warning },
            new CustomerData() { CustomerId = 7, Name = "J. P. Morgan", BirthDate = DateTime.Parse("1969-05-07"), Color = BootstrapColor.Light },
            new CustomerData() { CustomerId = 8, Name = "J. R. Ewing", BirthDate = DateTime.Parse("1987-03-08"), Color = BootstrapColor.Warning },
            new CustomerData() { CustomerId = 9, Name = "Jeremy Clarkson", BirthDate = DateTime.Parse("1994-04-09"), Color = BootstrapColor.Danger },
            new CustomerData() { CustomerId = 10, Name = "Jenny Green", BirthDate = DateTime.Parse("1947-02-10"), Color = BootstrapColor.Light },
            new CustomerData() { CustomerId = 11, Name = "Joseph Blue", BirthDate = DateTime.Parse("1948-12-11"), Color = BootstrapColor.Light },
            new CustomerData() { CustomerId = 12, Name = "Jack Daniels", BirthDate = DateTime.Parse("1968-10-12"), Color = BootstrapColor.Light },
            new CustomerData() { CustomerId = 13, Name = "Jackie Chan", BirthDate = DateTime.Parse("1978-08-13"), Color = BootstrapColor.Light },
            new CustomerData() { CustomerId = 14, Name = "Jasper", BirthDate = DateTime.Parse("1934-06-14"), Color = BootstrapColor.Light },
            new CustomerData() { CustomerId = 15, Name = "Jumbo", BirthDate = DateTime.Parse("1965-06-15"), Color = BootstrapColor.Light },
            new CustomerData() { CustomerId = 16, Name = "Junkie Doodle", BirthDate = DateTime.Parse("1977-05-16"), Color = BootstrapColor.Light }
        }.AsQueryable();
    }


	public GridViewDataSet<CustomerData> Customers { get; set; } = new GridViewDataSet<CustomerData>() { PagingOptions = { PageSize = 4} };

	public override Task PreRender()
	{
		if (Customers.IsRefreshRequired)
		{
			var queryable = FakeDb();
			Customers.LoadFromQueryable(queryable);
		}
		return base.PreRender();
	}

    public void SortCustomers(string column)
    {
        Customers.SetSortExpression(column);
    }

}

public class CustomerData
{
    public int CustomerId { get; set; }

    public string Name { get; set; }

    public DateTime BirthDate { get; set; }

    public BootstrapColor Color { get; set; }
}
