using Microsoft.EntityFrameworkCore;
using PayBill.Data.EntityConfiguration;
using PayBill.Models;

namespace PayBill.Data;

public class PayBillDbContext : DbContext
{
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<Receipt_Details> Receipt_Details { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Tables> Tables { get; set; }
    public DbSet<Receipt_Employee> Receipt_Employees { get; set; }
    //private readonly DbContextOptions<PayBillDbContext> _options;

    public PayBillDbContext(DbContextOptions<PayBillDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new DishEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ReceiptEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ReceiptDetailsEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TableEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ReceiptEmployeeEntityTypeConfiguration());
    }
}
