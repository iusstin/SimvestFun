using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimvestFun.Infrastructure.Migrations
{
    public partial class InitialStockData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentPrice = table.Column<double>(type: "float", nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "CurrentPrice", "Index", "Industry", "Name" },
                values: new object[,]
                {
                    { "AAPL", 162.94999999999999, 1, "Consumer electronics, software and online services", "Apple" },
                    { "AMZ", 2786.0, 4, "E-commerce", "Amazon" },
                    { "BAC", 41.039999999999999, 17, "Financial services", "Bank of America" },
                    { "BRK.B", 488245.0, 6, "Financial services", "Berkshire Hathaway" },
                    { "CVX", 166.27000000000001, 18, "Oil and gas", "Chevron" },
                    { "FB", 198.5, 8, "Information technology", "Meta" },
                    { "GOOG", 2677.0, 3, "Conglomerate", "Alphabet" },
                    { "HD", 317.19999999999999, 16, "Retail", "Home Depot" },
                    { "JNJ", 169.36000000000001, 10, "Pharmaceutical Medical devices Consumer healthcare", "Johnson & Johnson" },
                    { "JPM", 133.44, 12, "Financial services", "JPMorgan Chase" },
                    { "MA", 328.13, 19, "Payments", "Mastercard" },
                    { "MSFT", 288.5, 2, "Multinational technology", "Microsoft" },
                    { "NVDA", 230.13999999999999, 7, "Visual computing", "Nvidia" },
                    { "PFE", 48.75, 20, "Pharmaceutical and biotechnology", "Pfizer" },
                    { "PG", 148.77000000000001, 14, "Consumer goods", "Procter & Gamble" },
                    { "TSLA", 858.97000000000003, 5, "Electric vehicle and clean energy", "Tesla" },
                    { "UNH", 485.56999999999999, 9, "Managed healthcare insurance", "United Health" },
                    { "V", 199.75999999999999, 11, "Financial services", "Visa" },
                    { "WMT", 139.46000000000001, 13, "Retail", "Walmart" },
                    { "XOM", 82.790000000000006, 15, "Oil and gas", "Exxon Mobil" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
