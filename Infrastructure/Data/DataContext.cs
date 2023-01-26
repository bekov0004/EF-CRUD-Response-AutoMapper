using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    public  DbSet<Customer> Customers {get; set;}
    public  DbSet<Address> Addresses {get; set;}
    public  DbSet<Order> Orders {get; set;}
    public  DbSet<OrderItem> OrderItems {get; set;}
    public  DbSet<Product> Products {get; set;}
    public  DbSet<Supplier> Suppliers {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderItem>()
            .HasKey(es => new{es.OrderId, es.ProductId});
        base.OnModelCreating(modelBuilder);
    }
}
