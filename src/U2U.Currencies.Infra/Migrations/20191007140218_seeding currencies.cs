using Microsoft.EntityFrameworkCore.Migrations;

namespace U2U.Currencies.Infra.Migrations
{
    public partial class seedingcurrencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "USD");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "JPY");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "DLR");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "YEN");
        }
    }
}
