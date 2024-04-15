using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimvestFun.Infrastructure.Migrations
{
    public partial class AddEmailHashToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailHash",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailHash",
                table: "AspNetUsers");
        }
    }
}
