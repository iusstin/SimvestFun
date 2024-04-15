using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimvestFun.Infrastructure.Migrations
{
    public partial class InsertIntoEmailHash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE AspNetUsers SET EmailHash = LOWER(CONVERT(nvarchar(max), HashBytes('MD5',CONVERT(varchar(256), Email)),2)) WHERE EmailHash=''");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailHash",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "EmailHash",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
