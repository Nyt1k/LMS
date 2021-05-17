using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class AddMemberModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Members");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNUmber",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserMembershipId",
                table: "Members",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_UserMembershipId",
                table: "Members",
                column: "UserMembershipId",
                unique: true,
                filter: "[UserMembershipId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_AspNetUsers_UserMembershipId",
                table: "Members",
                column: "UserMembershipId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_AspNetUsers_UserMembershipId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_UserMembershipId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "UserMembershipId",
                table: "Members");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNUmber",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
