using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimvestFun.Infrastructure.Migrations
{
    public partial class PriceTypeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPrice",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "AAPL",
                column: "CurrentPrice",
                value: 162.95m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "AMZN",
                column: "CurrentPrice",
                value: 2786m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "BAC",
                column: "CurrentPrice",
                value: 41.04m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "BRK.B",
                column: "CurrentPrice",
                value: 488245m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "CVX",
                column: "CurrentPrice",
                value: 166.27m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "FB",
                column: "CurrentPrice",
                value: 198.50m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "GOOG",
                column: "CurrentPrice",
                value: 2677m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "HD",
                column: "CurrentPrice",
                value: 317.20m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "JNJ",
                column: "CurrentPrice",
                value: 169.36m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "JPM",
                column: "CurrentPrice",
                value: 133.44m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "MA",
                column: "CurrentPrice",
                value: 328.13m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "MSFT",
                column: "CurrentPrice",
                value: 288.50m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "NVDA",
                column: "CurrentPrice",
                value: 230.14m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "PFE",
                column: "CurrentPrice",
                value: 48.75m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "PG",
                column: "CurrentPrice",
                value: 148.77m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "TSLA",
                column: "CurrentPrice",
                value: 858.97m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "UNH",
                column: "CurrentPrice",
                value: 485.57m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "V",
                column: "CurrentPrice",
                value: 199.76m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "WMT",
                column: "CurrentPrice",
                value: 139.46m);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "XOM",
                column: "CurrentPrice",
                value: 82.79m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "CurrentPrice",
                table: "Stocks",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "AAPL",
                column: "CurrentPrice",
                value: 162.94999999999999);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "AMZN",
                column: "CurrentPrice",
                value: 2786.0);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "BAC",
                column: "CurrentPrice",
                value: 41.039999999999999);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "BRK.B",
                column: "CurrentPrice",
                value: 488245.0);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "CVX",
                column: "CurrentPrice",
                value: 166.27000000000001);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "FB",
                column: "CurrentPrice",
                value: 198.5);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "GOOG",
                column: "CurrentPrice",
                value: 2677.0);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "HD",
                column: "CurrentPrice",
                value: 317.19999999999999);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "JNJ",
                column: "CurrentPrice",
                value: 169.36000000000001);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "JPM",
                column: "CurrentPrice",
                value: 133.44);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "MA",
                column: "CurrentPrice",
                value: 328.13);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "MSFT",
                column: "CurrentPrice",
                value: 288.5);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "NVDA",
                column: "CurrentPrice",
                value: 230.13999999999999);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "PFE",
                column: "CurrentPrice",
                value: 48.75);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "PG",
                column: "CurrentPrice",
                value: 148.77000000000001);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "TSLA",
                column: "CurrentPrice",
                value: 858.97000000000003);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "UNH",
                column: "CurrentPrice",
                value: 485.56999999999999);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "V",
                column: "CurrentPrice",
                value: 199.75999999999999);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "WMT",
                column: "CurrentPrice",
                value: 139.46000000000001);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "XOM",
                column: "CurrentPrice",
                value: 82.790000000000006);
        }
    }
}
