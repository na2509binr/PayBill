using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PayBill.Models;

namespace PayBill.Data.EntityConfiguration;

public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.ID_Employee);
        builder.Property(e => e.ID_Employee)
           .ValueGeneratedOnAdd();
        builder.ToTable("T_Employee");
    }
}
