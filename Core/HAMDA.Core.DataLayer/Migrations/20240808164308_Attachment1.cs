using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAMDA.Core.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Attachment1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Attachments");

            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "Attachments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20dcdd1d-134c-4ce7-ba60-63aaa7535590",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "23065e18-3dee-413f-ae1c-65c5bd50bbc7", "e1789f3b-cced-4e3f-8ae1-bf5e8a24a036" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Attachments");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20dcdd1d-134c-4ce7-ba60-63aaa7535590",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "90de18a7-5a39-4482-b2c6-a5b46c4f628a", "7ff7fd36-2620-4b6b-958f-562ca7ee3be2" });
        }
    }
}
