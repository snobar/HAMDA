using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAMDA.Core.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class inituser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7cee4936-131e-4b86-a5f1-0141844e57c3", 0, "6d8ac924-0d10-41cc-994b-d7654ada781b", "Admin@hamda.com", true, false, null, null, "Admin@hamda.com", "AQAAAAIAAYagAAAAEDepnKtDmohgyaT4UFwRoBA/UkqboGo7M7DXqrWvEwoN03xtvqJuIuOkHrWUDahvwg==", null, false, "2e346fb1-62af-44ac-ab0e-49180425c9e8", false, "Admin@hamda.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7cee4936-131e-4b86-a5f1-0141844e57c3");
        }
    }
}
