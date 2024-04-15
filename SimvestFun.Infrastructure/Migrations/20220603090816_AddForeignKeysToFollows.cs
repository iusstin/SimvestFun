using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimvestFun.Infrastructure.Migrations
{
    public partial class AddForeignKeysToFollows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Follows_UserId",
                table: "Follows",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_UserId",
                table: "Follows",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_FollowedUserId",
                table: "Follows",
                column: "FollowedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_UserId",
                table: "Follows");
         
            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_FollowedUserId",
                table: "Follows");

            migrationBuilder.DropIndex(
                name: "IX_Follows_UserId",
                table: "Follows");
        }
    }
}
