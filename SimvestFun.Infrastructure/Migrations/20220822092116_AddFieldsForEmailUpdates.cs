using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimvestFun.Infrastructure.Migrations
{
    public partial class AddFieldsForRememberUs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUnsubscribedFromEmails",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEmailSentOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: null);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastVisitedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.Sql("UPDATE AspNetUsers SET LastVisitedOn = ISNULL((SELECT MAX(TimeStamp) FROM UserActions " +
                "WHERE UserActions.ApplicationUserId = AspNetUsers.Id), DATETIMEFROMPARTS(2022, 5, 10, 0, 0, 0, 0))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUnsubscribedFromEmails",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastEmailSentOn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastVisitedOn",
                table: "AspNetUsers");
        }
    }
}
