namespace InventoryManagementSystem
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

        public User(int id, string username, string role)
        {
            Id = id;
            Username = username;
            Role = role;
        }
    }
}