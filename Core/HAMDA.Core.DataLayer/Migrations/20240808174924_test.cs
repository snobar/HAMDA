using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAMDA.Core.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20dcdd1d-134c-4ce7-ba60-63aaa7535590",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "082e689d-2679-4e37-99eb-2efe793e03a5", "41903bea-d513-4766-84de-4b3a2290a4e2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20dcdd1d-134c-4ce7-ba60-63aaa7535590",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5250bf48-8d04-4fad-9f25-5c231ba5d6cc", "d8f47101-33ca-4c34-9d75-54fe8acab12a" });
        }
    }
}
