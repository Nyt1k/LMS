using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class SeedBooksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "ISBN", "ImageUrl", "Location", "NumberOfCopies", "Title", "Year" },
                values: new object[] { 1, "testat", 1234, "/images/republic.jpg", 2, 10, "test", "1337" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
