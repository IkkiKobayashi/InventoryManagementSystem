using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryManagementSystem
{
    class Program
    {
        static List<Category> categories = new List<Category>
        {
            new Category(1, "Laptops & PCs"),
            new Category(2, "Gaming Peripherals"),
            new Category(3, "Components")
        };

        static List<Supplier> suppliers = new List<Supplier>
        {
            new Supplier(1, "ASUS Philippines", "support@asus.com.ph"),
            new Supplier(2, "Logitech G Official", "sales@logitechg.com"),
            new Supplier(3, "SteelSeries Global", "contact@steelseries.com")
        };

        static List<Product> products = new List<Product>
        {
            new Product(1, "ASUS ROG Zephyrus G14", 1, 1, 98995.00, 10),
            new Product(2, "ASUS TUF Gaming F15", 1, 1, 54990.00, 3), 
            
            new Product(3, "Logitech G Pro X Superlight", 2, 2, 7490.00, 25),
            new Product(4, "SteelSeries Apex Pro TKL", 2, 3, 11500.00, 8),
            new Product(5, "Logitech C922 Pro Webcam", 2, 2, 5250.00, 2), 

            new Product(6, "ASUS ROG Strix RTX 4080", 3, 1, 78200.00, 5)
        };

        static List<TransactionRecord> transactions = new List<TransactionRecord>
        {
            new TransactionRecord(1, 1, "Initial Stock", 10),
            new TransactionRecord(2, 2, "Initial Stock", 3),
            new TransactionRecord(3, 3, "Initial Stock", 25),
            new TransactionRecord(4, 4, "Initial Stock", 8),
            new TransactionRecord(5, 5, "Initial Stock", 2),
            new TransactionRecord(6, 6, "Initial Stock", 5)
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("==============================================");
                Console.WriteLine("    TECH INVENTORY MANAGEMENT SYSTEM (PHP)    ");
                Console.WriteLine("==============================================");
                Console.WriteLine("1. Manage Categories/Suppliers");
                Console.WriteLine("2. Product Management (Add/View/Update/Delete)");
                Console.WriteLine("3. Stock Operations (Restock/Deduct)");
                Console.WriteLine("4. Reports (Low Stock/Total Value)");
                Console.WriteLine("5. Transaction History");
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect an option: ");

                try
                {
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1": SetupMenu(); break;
                        case "2": ProductMenu(); break;
                        case "3": StockMenu(); break;
                        case "4": ReportMenu(); break;
                        case "5": ViewTransactions(); break;
                        case "0": running = false; break;
                        default: Console.WriteLine("Invalid option."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        #region Sub-Menus
        static void SetupMenu()
        {
            Console.WriteLine("\n--- Manage Setup ---");
            Console.WriteLine("1. Add Category\n2. Add Supplier");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Category Name: ");
                string name = Console.ReadLine();
                int newId = categories.Any() ? categories.Max(c => c.Id) + 1 : 1;
                categories.Add(new Category(newId, name));
            }
            else if (choice == "2")
            {
                Console.Write("Supplier Name: ");
                string name = Console.ReadLine();
                Console.Write("Contact: ");
                string contact = Console.ReadLine();
                int newId = suppliers.Any() ? suppliers.Max(s => s.Id) + 1 : 1;
                suppliers.Add(new Supplier(newId, name, contact));
            }
            Console.WriteLine("Success! Press any key...");
            Console.ReadKey();
        }

        static void ProductMenu()
        {
            Console.Clear();
            Console.WriteLine("--- Product Management ---");
            Console.WriteLine("1. Add Product\n2. View All Products\n3. Search\n4. Update\n5. Delete");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1": AddProduct(); break;
                case "2": ViewProducts(); break;
                case "3": SearchProduct(); break;
                case "4": UpdateProduct(); break;
                case "5": DeleteProduct(); break;
            }
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
        #endregion

        #region Core Features
        static void AddProduct()
        {
            Console.Write("Product Name: ");
            string name = Console.ReadLine();
            Console.Write("Price (₱): ");
            double price = double.Parse(Console.ReadLine());
            Console.Write("Stock: ");
            int stock = int.Parse(Console.ReadLine());

            int newId = products.Any() ? products.Max(p => p.Id) + 1 : 1;
            products.Add(new Product(newId, name, 1, 1, price, stock));
            Console.WriteLine("Product Added!");
        }

        static void ViewProducts()
        {
            Console.WriteLine(String.Format("\n{0,-5} | {1,-30} | {2,-15} | {3,-5}", "ID", "Name", "Price (₱)", "Stock"));
            Console.WriteLine(new String('-', 65));
            foreach (var p in products)
            {
                Console.WriteLine(String.Format("{0,-5} | {1,-30} | ₱{2,-14:N2} | {3,-5}", p.Id, p.Name, p.Price, p.Stock));
            }
        }

        static void SearchProduct()
        {
            Console.Write("Search (Name): ");
            string query = Console.ReadLine().ToLower();
            var results = products.Where(p => p.Name.ToLower().Contains(query));
            foreach (var p in results) Console.WriteLine($"[ID: {p.Id}] {p.Name} - ₱{p.Price:N2} ({p.Stock} in stock)");
        }

        static void UpdateProduct()
        {
            Console.Write("Enter Product ID to Update Price: ");
            int id = int.Parse(Console.ReadLine());
            var prod = products.Find(p => p.Id == id);
            if (prod != null)
            {
                Console.Write($"New Price for {prod.Name}: ");
                prod.Price = double.Parse(Console.ReadLine());
                Console.WriteLine("Update successful!");
            }
        }

        static void DeleteProduct()
        {
            Console.Write("Enter Product ID to Delete: ");
            int id = int.Parse(Console.ReadLine());
            int count = products.RemoveAll(p => p.Id == id);
            Console.WriteLine(count > 0 ? "Deleted." : "Not found.");
        }

        static void StockMenu()
        {
            Console.WriteLine("1. Restock\n2. Deduct");
            string choice = Console.ReadLine();
            Console.Write("Product ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Qty: ");
            int qty = int.Parse(Console.ReadLine());

            var prod = products.Find(p => p.Id == id);
            if (prod != null)
            {
                int tId = transactions.Any() ? transactions.Max(t => t.TransactionId) + 1 : 1;
                if (choice == "1")
                {
                    prod.Stock += qty;
                    transactions.Add(new TransactionRecord(tId, id, "Restock", qty));
                    Console.WriteLine("Stock Increased!");
                }
                else if (prod.Stock >= qty)
                {
                    prod.Stock -= qty;
                    transactions.Add(new TransactionRecord(tId, id, "Deduction", qty));
                    Console.WriteLine("Stock Decreased!");
                }
                else
                {
                    Console.WriteLine("Error: Insufficient stock!");
                }
            }
            Console.ReadKey();
        }

        static void ReportMenu()
        {
            Console.WriteLine("1. View Low Stock Items\n2. View Total Inventory Value");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                var low = products.Where(p => p.Stock < 5);
                Console.WriteLine("\n--- LOW STOCK ALERT ---");
                foreach (var p in low) Console.WriteLine($"{p.Name}: Only {p.Stock} remaining!");
            }
            else
            {
                double total = products.Sum(p => p.Price * p.Stock);
                Console.WriteLine($"\nGRAND TOTAL INVENTORY VALUE: ₱{total:N2}");
            }
            Console.ReadKey();
        }

        static void ViewTransactions()
        {
            Console.WriteLine("\n--- TRANSACTION HISTORY ---");
            foreach (var t in transactions)
            {
                var name = products.Find(p => p.Id == t.ProductId)?.Name ?? "Item Deleted";
                Console.WriteLine($"{t.Date:yyyy-MM-dd HH:mm} | {t.Type} | {t.Quantity} units | {name}");
            }
            Console.ReadKey();
        }
        #endregion
    }
}