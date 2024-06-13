using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayBill.Models;

namespace PayBill.Data.EntityConfiguration;

public class ReceiptEntityTypeConfiguration : IEntityTypeConfiguration<Receipt>
{
    public void Configure(EntityTypeBuilder<Receipt> builder)
    {
        builder.HasKey(e => e.ID_Receipt);

        builder.ToTable("T_Receipt");
    }
}
