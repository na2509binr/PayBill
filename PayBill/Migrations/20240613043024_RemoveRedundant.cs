using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayBill.Migrations
{
    public partial class RemoveRedundant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "T_Dish");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "T_Dish",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
