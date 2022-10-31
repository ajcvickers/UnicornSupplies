using Microsoft.EntityFrameworkCore;
using UnicornSupplies;

public class UnicornsContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\MSSQLLocalDB;Database=UnicornSupplies");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder
        //     .Entity<Customer>()
        //     .OwnsOne(e => e.ContactDetails, b =>
        //     {
        //         b.OwnsMany(e => e.Addresses);
        //         b.OwnsMany(e => e.PhoneNumbers);
        //         b.OwnsMany(e => e.EmailAddresses);
        //         b.ToJson();
        //     });
        //
        // modelBuilder
        //     .Entity<Order>()
        //     .OwnsOne(e => e.DeliveryAddress)
        //     .ToJson();
    }
}
