using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PayBill.Models;

namespace PayBill.Data.EntityConfiguration;

public class ReceiptDetailsEntityTypeConfiguration : IEntityTypeConfiguration<Receipt_Details>
{
    public void Configure(EntityTypeBuilder<Receipt_Details> builder)
    {
        builder.HasKey(e => e.ID_Receipt_Details);

        builder.ToTable("T_Receipt_Detail");
    }
}
