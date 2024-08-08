using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAMDA.Core.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedById : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ea9a713-aaf4-419d-bb97-849714fe91a2");

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "Costumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "Attachments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "20dcdd1d-134c-4ce7-ba60-63aaa7535590", null, "Admin", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20dcdd1d-134c-4ce7-ba60-63aaa7535590");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Costumers");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Attachments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3ea9a713-aaf4-419d-bb97-849714fe91a2", null, "Admin", "Admin" });
        }
    }
}
