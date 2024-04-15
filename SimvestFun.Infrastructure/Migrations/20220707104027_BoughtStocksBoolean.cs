using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimvestFun.Infrastructure.Migrations
{
    public partial class BoughtStocksBoolean : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBoughtAnyStocks",
                table: "AspNetUsers",
                type: "bit",
                defaultValue: false);

            migrationBuilder.Sql("UPDATE AspNetUsers SET HasBoughtAnyStocks = 'True' " +
                "where (select count(*) from UserActions as UA " +
                "where UA.ApplicationUserId=AspNetUsers.Id and UA.ActionType = 'Buy') > 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBoughtAnyStocks",
                table: "AspNetUsers");
        }
    }
}
