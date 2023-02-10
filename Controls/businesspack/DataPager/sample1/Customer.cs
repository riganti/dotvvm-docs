namespace DotvvmWeb.Views.Docs.Controls.businesspack.DataPager.sample1
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Customer() 
        {
        }

        public Customer(int id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
