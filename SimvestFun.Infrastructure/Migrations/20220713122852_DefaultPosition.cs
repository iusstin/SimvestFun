using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimvestFun.Infrastructure.Migrations
{
    public partial class DefaultPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "HasBoughtAnyStocks",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "CurrentPosition",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "YesterdayPosition",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CurrentPosition",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                defaultValue: null);

            migrationBuilder.AddColumn<int>(
                name: "YesterdayPosition",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                defaultValue: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "HasBoughtAnyStocks",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.DropColumn(
                name: "CurrentPosition",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "YesterdayPosition",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CurrentPosition",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YesterdayPosition",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
