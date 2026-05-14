using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ViewModel : DotvvmViewModelBase
{
    public GridViewDataSet<PersonDto> People { get; set; } = new GridViewDataSet<PersonDto>()
    {
        PagingOptions = { PageSize = 5 }
    };

    public override async Task PreRender()
    {
        People.LoadFromQueryable(GetPeopleQuery());
        await base.PreRender();
    }

    private IQueryable<PersonDto> GetPeopleQuery()
    {
        return new List<PersonDto>
        {
            new() { Id = 1,  Name = "Alice Johnson" },
            new() { Id = 2,  Name = "Bob Smith" },
            new() { Id = 3,  Name = "Charlie Brown" },
            new() { Id = 4,  Name = "Diana Prince" },
            new() { Id = 5,  Name = "Eve Williams" },
            new() { Id = 6,  Name = "Frank Miller" },
            new() { Id = 7,  Name = "Grace Lee" },
            new() { Id = 8,  Name = "Henry Davis" },
            new() { Id = 9,  Name = "Ivy Chen" },
            new() { Id = 10, Name = "Jack Wilson" },
            new() { Id = 11, Name = "Karen Moore" },
            new() { Id = 12, Name = "Liam Taylor" },
            new() { Id = 13, Name = "Mia Anderson" },
            new() { Id = 14, Name = "Noah Thomas" },
            new() { Id = 15, Name = "Olivia Jackson" },
            new() { Id = 16, Name = "Paul White" },
            new() { Id = 17, Name = "Quinn Harris" },
            new() { Id = 18, Name = "Rachel Martin" },
            new() { Id = 19, Name = "Sam Garcia" },
            new() { Id = 20, Name = "Tina Martinez" },
            new() { Id = 21, Name = "Uma Robinson" },
            new() { Id = 22, Name = "Victor Clark" },
        }.AsQueryable();
    }
}

public class PersonDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}