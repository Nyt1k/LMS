using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class AddHoldModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HoldId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Holds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    HoldTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holds_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_HoldId",
                table: "Books",
                column: "HoldId");

            migrationBuilder.CreateIndex(
                name: "IX_Holds_MemberId",
                table: "Holds",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Holds_HoldId",
                table: "Books",
                column: "HoldId",
                principalTable: "Holds",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Holds_HoldId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Holds");

            migrationBuilder.DropIndex(
                name: "IX_Books_HoldId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "HoldId",
                table: "Books");
        }
    }
}
