using System.Data.Entity;
using System.Reflection.Emit;
using UnicornSupplies;

namespace UnicornSupplies
{
    public class UnicornSuppliesContext : DbContext
    {
        static UnicornSuppliesContext()
        {
            Database.SetInitializer<UnicornSuppliesContext>(null);
        }
        
        public UnicornSuppliesContext()
            : base("name=UnicornSupplies")
        {
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
    }
}
