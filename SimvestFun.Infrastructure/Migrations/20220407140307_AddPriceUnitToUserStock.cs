using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimvestFun.Infrastructure.Migrations
{
    public partial class AddPriceUnitToUserStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserStocks",
                table: "UserStocks");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "AAPL");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "AMZN");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "BAC");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "BRK.B");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "CVX");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "FB");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "GOOG");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "HD");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "JNJ");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "JPM");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "MA");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "MSFT");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "NVDA");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "PFE");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "PG");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "TSLA");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "UNH");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "V");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "WMT");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "XOM");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserStocks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "BuyingPricePerUnit",
                table: "UserStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserStocks",
                table: "UserStocks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserStocks_ApplicationUserId",
                table: "UserStocks",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserStocks",
                table: "UserStocks");

            migrationBuilder.DropIndex(
                name: "IX_UserStocks_ApplicationUserId",
                table: "UserStocks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserStocks");

            migrationBuilder.DropColumn(
                name: "BuyingPricePerUnit",
                table: "UserStocks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserStocks",
                table: "UserStocks",
                columns: new[] { "ApplicationUserId", "StockId" });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "CurrentPrice", "Index", "Industry", "Name", "PriceUpdatedOn" },
                values: new object[,]
                {
                    { "AAPL", 162.95m, 1, "Consumer electronics, software and online services", "Apple", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9943) },
                    { "AMZN", 2786m, 4, "E-commerce", "Amazon", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9950) },
                    { "BAC", 41.04m, 17, "Financial services", "Bank of America", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9957) },
                    { "BRK.B", 488245m, 6, "Financial services", "Berkshire Hathaway", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9951) },
                    { "CVX", 166.27m, 18, "Oil and gas", "Chevron", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9958) },
                    { "FB", 198.50m, 8, "Information technology", "Meta", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9953) },
                    { "GOOG", 2677m, 3, "Conglomerate", "Alphabet", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9949) },
                    { "HD", 317.20m, 16, "Retail", "Home Depot", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9957) },
                    { "JNJ", 169.36m, 10, "Pharmaceutical Medical devices Consumer healthcare", "Johnson & Johnson", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9954) },
                    { "JPM", 133.44m, 12, "Financial services", "JPMorgan Chase", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9955) },
                    { "MA", 328.13m, 19, "Payments", "Mastercard", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9958) },
                    { "MSFT", 288.50m, 2, "Multinational technology", "Microsoft", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9948) },
                    { "NVDA", 230.14m, 7, "Visual computing", "Nvidia", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9952) },
                    { "PFE", 48.75m, 20, "Pharmaceutical and biotechnology", "Pfizer", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9959) },
                    { "PG", 148.77m, 14, "Consumer goods", "Procter & Gamble", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9956) },
                    { "TSLA", 858.97m, 5, "Electric vehicle and clean energy", "Tesla", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9951) },
                    { "UNH", 485.57m, 9, "Managed healthcare insurance", "United Health", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9953) },
                    { "V", 199.76m, 11, "Financial services", "Visa", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9954) },
                    { "WMT", 139.46m, 13, "Retail", "Walmart", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9955) },
                    { "XOM", 82.79m, 15, "Oil and gas", "Exxon Mobil", new DateTime(2022, 4, 1, 11, 53, 22, 201, DateTimeKind.Utc).AddTicks(9956) }
                });
        }
    }
}
