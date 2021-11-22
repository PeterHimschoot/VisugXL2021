using Microsoft.EntityFrameworkCore.Migrations;

namespace U2U.Currencies.Infra.Migrations
{
    public partial class Created : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 3, nullable: false),
                    ValueInEuro = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Name", "ValueInEuro" },
                values: new object[] { 1, "EUR", 1.0m });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Name", "ValueInEuro" },
                values: new object[] { 2, "DLR", 0.9m });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Name", "ValueInEuro" },
                values: new object[] { 3, "YEN", 1.5m });

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Name",
                table: "Currencies",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
