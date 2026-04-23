public class TablesViewModel
{
    public List<Customer> Customers { get; set; } = new()
    {
        new() { Id = 1, Name = "Alice", Email = "alice@example.com" },
        new() { Id = 2, Name = "Bob", Email = "bob@example.com" }
    };

    public void Edit(Customer customer)
    {
        // edit logic
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }
}