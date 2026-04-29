using DotVVM.Framework.ViewModel;

public class  ViewModel : DotvvmViewModelBase
{
    public GridViewDataSet<Person> People { get; set; } = new()
    {
        PagingOptions = { PageSize = 5 }
    };

    public override Task PreRender()
    {
        if (People.IsRefreshRequired)
        {
            People.LoadFromQueryable(GetPeople().AsQueryable());
        }
        return base.PreRender();
    }

    private IEnumerable<Person> GetPeople()
    {
        for (int i = 1; i <= 50; i++)
            yield return new Person { Id = i, Name = $"Person {i}" };
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }
}