using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAMDA.Core.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class onlycheckup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20dcdd1d-134c-4ce7-ba60-63aaa7535590",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "90ecb97e-8432-4897-b27e-d227ace2cfc4", "987a5b6e-a30d-4346-8a10-8a4ae49303c9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20dcdd1d-134c-4ce7-ba60-63aaa7535590",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "082e689d-2679-4e37-99eb-2efe793e03a5", "41903bea-d513-4766-84de-4b3a2290a4e2" });
        }
    }
}
