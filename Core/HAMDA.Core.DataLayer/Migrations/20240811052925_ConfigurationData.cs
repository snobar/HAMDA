using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAMDA.Core.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurationData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20dcdd1d-134c-4ce7-ba60-63aaa7535590",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7a42f55b-6f50-45e1-8dba-7d59a627dfe3", "bfe87818-0af8-40de-b4e6-0011b93723c1" });

            migrationBuilder.InsertData(
                table: "Configuration",
                columns: new[] { "Id", "CreatedDate", "ModifiedById", "NumberOfSeats", "Status" },
                values: new object[] { 1L, new DateTime(2024, 8, 11, 8, 29, 24, 658, DateTimeKind.Local).AddTicks(5589), null, 0, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Configuration",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20dcdd1d-134c-4ce7-ba60-63aaa7535590",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "87e91ed3-4b9e-4e74-9997-d4f32412d36d", "0cd36370-fcdf-4dd0-a1f6-d19858254521" });
        }
    }
}
