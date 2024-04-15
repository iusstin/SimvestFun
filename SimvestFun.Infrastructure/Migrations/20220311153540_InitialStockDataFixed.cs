using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimvestFun.Infrastructure.Migrations
{
    public partial class InitialStockDataFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "AMZ");

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "CurrentPrice", "Index", "Industry", "Name" },
                values: new object[] { "AMZN", 2786.0, 4, "E-commerce", "Amazon" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "AMZN");

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "CurrentPrice", "Index", "Industry", "Name" },
                values: new object[] { "AMZ", 2786.0, 4, "E-commerce", "Amazon" });
        }
    }
}
