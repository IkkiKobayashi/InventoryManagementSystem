namespace InventoryManagementSystem
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }

        public Supplier(int id, string name, string contact)
        {
            Id = id;
            Name = name;
            Contact = contact;
        }
    }
}