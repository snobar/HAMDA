using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAMDA.Core.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class initUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20dcdd1d-134c-4ce7-ba60-63aaa7535590",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5250bf48-8d04-4fad-9f25-5c231ba5d6cc", "AQAAAAIAAYagAAAAEPkTIuEco1Bk2uZSa78Gp+82zNJA5MC3+zgF/Y6qHnuRrqFaVbFU/ksBSKVCCa2d8w==", "d8f47101-33ca-4c34-9d75-54fe8acab12a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20dcdd1d-134c-4ce7-ba60-63aaa7535590",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23065e18-3dee-413f-ae1c-65c5bd50bbc7", "AQAAAAIAAYagAAAAEDepnKtDmohgyaT4UFwRoBA/UkqboGo7M7DXqrWvEwoN03xtvqJuIuOkHrWUDahvwg==", "e1789f3b-cced-4e3f-8ae1-bf5e8a24a036" });
        }
    }
}
