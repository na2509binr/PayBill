using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayBill.Models;
namespace PayBill.Data.EntityConfiguration;

public class DishEntityTypeConfiguration : IEntityTypeConfiguration<Dish>
{
    public void Configure(EntityTypeBuilder<Dish> builder)
    {
        builder.HasKey(e => e.ID_Dish);

        builder.ToTable("T_Dish");
        builder.Ignore(e => e.TotalPrice);
    }
}
