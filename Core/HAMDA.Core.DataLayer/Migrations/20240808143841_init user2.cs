using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAMDA.Core.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class inituser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7cee4936-131e-4b86-a5f1-0141844e57c3");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "20dcdd1d-134c-4ce7-ba60-63aaa7535590", 0, "90de18a7-5a39-4482-b2c6-a5b46c4f628a", "Admin@hamda.com", true, false, null, null, "Admin@hamda.com", "AQAAAAIAAYagAAAAEDepnKtDmohgyaT4UFwRoBA/UkqboGo7M7DXqrWvEwoN03xtvqJuIuOkHrWUDahvwg==", null, false, "7ff7fd36-2620-4b6b-958f-562ca7ee3be2", false, "Admin@hamda.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3ea9a713-aaf4-419d-bb97-849714fe91a2", "20dcdd1d-134c-4ce7-ba60-63aaa7535590" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3ea9a713-aaf4-419d-bb97-849714fe91a2", "20dcdd1d-134c-4ce7-ba60-63aaa7535590" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20dcdd1d-134c-4ce7-ba60-63aaa7535590");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7cee4936-131e-4b86-a5f1-0141844e57c3", 0, "6d8ac924-0d10-41cc-994b-d7654ada781b", "Admin@hamda.com", true, false, null, null, "Admin@hamda.com", "AQAAAAIAAYagAAAAEDepnKtDmohgyaT4UFwRoBA/UkqboGo7M7DXqrWvEwoN03xtvqJuIuOkHrWUDahvwg==", null, false, "2e346fb1-62af-44ac-ab0e-49180425c9e8", false, "Admin@hamda.com" });
        }
    }
}
