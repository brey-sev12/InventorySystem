using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Models
{
    public class ISDBContext : DbContext
    {
        public ISDBContext(DbContextOptions<ISDBContext> options) : base(options) { }

        //Table Products
        public DbSet<Product> Products { get; set; }

        //Table Categories
        //public DbSet<Category> Categories { get; set; }

        //Table Suppliers
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
