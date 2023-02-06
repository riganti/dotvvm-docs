using DotVVM.Framework.ViewModel;
using Newtonsoft.Json;

namespace DotvvmWeb.Views.Docs.Controls.builtin.GridView.Sample5
{
    public class ViewModel : DotvvmViewModelBase
    {
        public Customer[] Customers { get; set; } = {
            new Customer(0, "Dani Michele"), new Customer(1, "Elissa Malone"),new Customer(2,"Raine Emmerson Damian III"),
            new Customer(3, "Gerrard Petra"), new Customer(4, "Clement Ernie"), new Customer(5, "Rod Fred")
        };

        public string ClickedRowId { get; set; }

        public void RowClicked(int id)
        {
            ClickedRowId = id.ToString();
        }

    }
    
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonConstructor]
        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
