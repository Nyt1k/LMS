using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class ChangeMemberModel3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Members_UserMembershipId",
                table: "Members");

            migrationBuilder.AlterColumn<string>(
                name: "UserMembershipId",
                table: "Members",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNUmber",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Members_UserMembershipId",
                table: "Members",
                column: "UserMembershipId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Members_UserMembershipId",
                table: "Members");

            migrationBuilder.AlterColumn<string>(
                name: "UserMembershipId",
                table: "Members",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNUmber",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_UserMembershipId",
                table: "Members",
                column: "UserMembershipId",
                unique: true,
                filter: "[UserMembershipId] IS NOT NULL");
        }
    }
}
