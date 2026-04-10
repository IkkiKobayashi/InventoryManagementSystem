using System;

namespace InventoryManagementSystem
{
    public class TransactionRecord
    {
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public TransactionRecord(int tId, int pId, string type, int qty)
        {
            TransactionId = tId;
            ProductId = pId;
            Type = type;
            Quantity = qty;
            Date = DateTime.Now;
        }
    }
}