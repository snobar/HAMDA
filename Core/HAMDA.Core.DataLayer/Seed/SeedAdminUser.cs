using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMDA.Core.DataLayer.Seed
{
    public partial class SeedAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add the user
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount" },
                values: new object[]
                {
                "20dcdd1d-134c-4ce7-ba60-63aaa7535590", // Use a GUID generator here or a specific GUID
                "hamdasmtp@gmail.com",
                "HAMDASMTP@GMAIL.COM",
                "hamdasmtp@gmail.com",
                "HAMDASMTP@GMAIL.COM",
                false,
                "AQAAAAIAAYagAAAAEDepnKtDmohgyaT4UFwRoBA/UkqboGo7M7DXqrWvEwoN03xtvqJuIuOkHrWUDahvwg==",
                "4RAPC6RZ2CNSJBAF5YVLA26W7EJDTDM7",
                "4b563cb4-d080-4eae-a917-d99fdafca137",
                null,
                false,
                false,
                null,
                true,
                0
                });

            // Assign the "Admin" role to the user
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[]
                {
                // Replace "new-guid" with the actual GUID used in the user record
                ("20dcdd1d-134c-4ce7-ba60-63aaa7535590", "3ea9a713-aaf4-419d-bb97-849714fe91a2") // Replace "admin-role-id" with the actual role ID (could be looked up if needed)
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the user
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20dcdd1d-134c-4ce7-ba60-63aaa7535590");

            // Remove the role assignment
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "20dcdd1d-134c-4ce7-ba60-63aaa7535590", "3ea9a713-aaf4-419d-bb97-849714fe91a2" });

            // Remove the role
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ea9a713-aaf4-419d-bb97-849714fe91a2");
        }
    }

}
