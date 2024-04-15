using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimvestFun.Infrastructure.Migrations
{
    public partial class StocksPricesModifiedOnField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PriceUpdatedOn",
                table: "Stocks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "AAPL",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2641));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "AMZN",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2648));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "BAC",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2656));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "BRK.B",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2649));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "CVX",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2657));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "FB",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2650));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "GOOG",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2647));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "HD",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2656));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "JNJ",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2652));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "JPM",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2653));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "MA",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2657));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "MSFT",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2646));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "NVDA",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2650));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "PFE",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2658));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "PG",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2654));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "TSLA",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2649));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "UNH",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2651));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "V",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2652));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "WMT",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2654));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: "XOM",
                column: "PriceUpdatedOn",
                value: new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2655));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceUpdatedOn",
                table: "Stocks");
        }
    }
}
