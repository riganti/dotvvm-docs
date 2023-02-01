using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.builtin.GridView.Sample6
{
    public class ViewModel : DotvvmViewModelBase
    {
        private static IQueryable<Customer> FakeDb()
        {
            return new[]
            {
                new Customer(0, "Dani Michele"), new Customer(1, "Elissa Malone"), new Customer(2,"Raine Damian"),
                new Customer(3, "Gerrard Petra"), new Customer(4, "Clement Ernie"), new Customer(5, "Rod Fred")
            }.AsQueryable();
        }

        public GridViewDataSet<Customer> Customers { get; set; } = new GridViewDataSet<Customer>() { RowEditOptions = { PrimaryKeyPropertyName = "Id" } };

        public void Edit(Customer customer)
        {
            Customers.RowEditOptions.EditRowId = customer.Id;
        }

        public override Task PreRender()
        {
            if (Customers.IsRefreshRequired)
            {
                var queryable = FakeDb();
                Customers.LoadFromQueryable(queryable);
            }
            return base.PreRender();
        }

        public void Update(Customer customer)
        {
            // TODO: save changes to the database
            Customers.RowEditOptions.EditRowId = null;

            // uncomment this line - it's here only for the sample to work without database
            //Customers.RequestRefresh();
        }

        public void CancelEdit()
        {
            Customers.RowEditOptions.EditRowId = null;

            // Refresh GridView items
            Customers.RequestRefresh();
        }
    }

    public record Customer(int Id, string Name);
}
