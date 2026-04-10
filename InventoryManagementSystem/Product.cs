namespace InventoryManagementSystem
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }

        public Product(int id, string name, int categoryId, int supplierId, double price, int stock)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            SupplierId = supplierId;
            Price = price;
            Stock = stock;
        }
    }
}