using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayBill.Models;

namespace PayBill.Data.EntityConfiguration;

public class SpendingEntityTypeConfiguration : IEntityTypeConfiguration<Spending>
{
    public void Configure(EntityTypeBuilder<Spending> builder)
    {
        builder.HasKey(s=> s.Spending_ID);

        builder.ToTable("T_Spending");

        builder
        .Property(s => s.SpendingType)
        .HasConversion(
            v => (int)v,
            v => (SpendingType)v);
    }
}
