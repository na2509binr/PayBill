using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayBill.Models;

namespace PayBill.Data.EntityConfiguration;

public class ReceiptEmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Receipt_Employee>
{
    public void Configure(EntityTypeBuilder<Receipt_Employee> builder)
    {
        builder.HasKey(e => e.ID_Receipt_Details);

        builder.ToTable("T_Receipt_Employee");
    }
}
