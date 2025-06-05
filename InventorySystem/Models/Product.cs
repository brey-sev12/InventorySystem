namespace InventorySystem.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
        public decimal Price { get; set; } = 0.0m;

        // Make sure SupplierID has public setter so EF can set it properly
        public int SupplierID { get; set; }

        // Navigation property to Supplier (no default string assignment)
        public Supplier Supplier { get; set; } = null!;
    }
}
