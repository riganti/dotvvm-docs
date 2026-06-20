using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.tailwind.DataPager.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public bool Enabled { get; set; } = true;

        public GridViewDataSet<PersonDto> People { get; set; } = new GridViewDataSet<PersonDto>()
        {
            PagingOptions = { PageSize = 4 }
        };

        public override Task PreRender()
        {
            if (People.IsRefreshRequired)
            {
                People.LoadFromQueryable(GetPeople().AsQueryable());
            }

            return base.PreRender();
        }

        private static List<PersonDto> GetPeople()
        {
            return new List<PersonDto>
            {
                new PersonDto { Id = 1, Name = "Alice Johnson" },
                new PersonDto { Id = 2, Name = "Bob Smith" },
                new PersonDto { Id = 3, Name = "Carol White" },
                new PersonDto { Id = 4, Name = "David Brown" },
                new PersonDto { Id = 5, Name = "Eva Green" },
                new PersonDto { Id = 6, Name = "Frank Miller" },
                new PersonDto { Id = 7, Name = "Grace Lee" },
                new PersonDto { Id = 8, Name = "Henry Davis" }
            };
        }
    }

    public class PersonDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
