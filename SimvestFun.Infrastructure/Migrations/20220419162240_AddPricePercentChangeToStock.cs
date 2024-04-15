using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimvestFun.Infrastructure.Migrations
{
    public partial class AddPricePercentChangeToStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PricePercentChange",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePercentChange",
                table: "Stocks");
        }
    }
}
