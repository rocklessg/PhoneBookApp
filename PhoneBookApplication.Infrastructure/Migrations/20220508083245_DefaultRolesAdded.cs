using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneBookApplication.Infrastructure.Migrations
{
    public partial class DefaultRolesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7e714400-44e6-4811-9df9-5ec60a07c33c", "c2d670a8-ea51-46fd-a59e-eb1662251300", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9ea78671-d464-45e3-af75-d00af927fd2a", "546fc3d5-68db-4d07-8c40-87c4ec56e8b5", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e714400-44e6-4811-9df9-5ec60a07c33c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ea78671-d464-45e3-af75-d00af927fd2a");
        }
    }
}
