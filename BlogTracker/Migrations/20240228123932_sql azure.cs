using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogTracker.Migrations
{
    public partial class sqlazure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AdminInfo",
                columns: new[] { "EmailId", "Password" },
                values: new object[] { "admin@gmail.com", "admin123" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdminInfo",
                keyColumn: "EmailId",
                keyValue: "admin@gmail.com");
        }
    }
}
