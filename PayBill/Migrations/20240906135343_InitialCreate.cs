using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayBill.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            

            migrationBuilder.CreateTable(
                name: "T_Spending",
                columns: table => new
                {
                    Spending_ID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SpendingType = table.Column<int>(type: "int", nullable: false),
                    Spending_Amount = table.Column<int>(type: "int", nullable: false),
                    Spending_Value = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Spending", x => x.Spending_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Dish");

            migrationBuilder.DropTable(
                name: "T_Employee");

            migrationBuilder.DropTable(
                name: "T_Receipt");

            migrationBuilder.DropTable(
                name: "T_Receipt_Detail");

            migrationBuilder.DropTable(
                name: "T_Receipt_Employee");

            migrationBuilder.DropTable(
                name: "T_Spending");

            migrationBuilder.DropTable(
                name: "T_Tables");
        }
    }
}
