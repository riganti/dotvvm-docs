using DotVVM.Framework.ViewModel;


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
    
    public record Customer(int Id, string Name);
}
