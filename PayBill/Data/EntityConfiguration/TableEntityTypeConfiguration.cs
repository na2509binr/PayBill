using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayBill.Models;

namespace PayBill.Data.EntityConfiguration;

public class TableEntityTypeConfiguration : IEntityTypeConfiguration<Tables>
{
    public void Configure(EntityTypeBuilder<Tables> builder)
    {
        builder.HasKey(e => e.ID);

        builder.ToTable("T_Tables");
    }
}
