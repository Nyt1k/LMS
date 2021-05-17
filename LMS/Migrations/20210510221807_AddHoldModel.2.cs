using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class AddHoldModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_HoldId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "HoldId",
                table: "Holds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_HoldId",
                table: "Books",
                column: "HoldId",
                unique: true,
                filter: "[HoldId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_HoldId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "HoldId",
                table: "Holds");

            migrationBuilder.CreateIndex(
                name: "IX_Books_HoldId",
                table: "Books",
                column: "HoldId");
        }
    }
}
