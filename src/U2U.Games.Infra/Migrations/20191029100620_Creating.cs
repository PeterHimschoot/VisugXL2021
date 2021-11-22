using Microsoft.EntityFrameworkCore.Migrations;

namespace U2U.Games.Infra.Migrations
{
    public partial class Creating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    PublisherId = table.Column<int>(nullable: true),
                    Price_Amount = table.Column<decimal>(nullable: true),
                    Price_Currency = table.Column<string>(maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    ImageLocation = table.Column<string>(maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameImages_Games_Id",
                        column: x => x.Id,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "999 Games" });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Goliath" });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Days of Wonder" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Name", "PublisherId", "Price_Amount", "Price_Currency" },
                values: new object[] { 1, "Qwirkle", 1, 29.95m, "EUR" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Name", "PublisherId", "Price_Amount", "Price_Currency" },
                values: new object[] { 2, "Rummikub", 2, 28.95m, "USD" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Name", "PublisherId", "Price_Amount", "Price_Currency" },
                values: new object[] { 3, "Ticket To Ride", 3, 34.95m, "EUR" });

            migrationBuilder.InsertData(
                table: "GameImages",
                columns: new[] { "Id", "ImageLocation", "Name" },
                values: new object[] { 1, "https://u2ublogimages.blob.core.windows.net/cleanarchitecture/GamesStore_Qwirkle.png", "Qwirkle" });

            migrationBuilder.InsertData(
                table: "GameImages",
                columns: new[] { "Id", "ImageLocation", "Name" },
                values: new object[] { 2, "https://u2ublogimages.blob.core.windows.net/cleanarchitecture/GamesStore_Rummikub.jpg", "Rummikub" });

            migrationBuilder.CreateIndex(
                name: "IX_Games_PublisherId",
                table: "Games",
                column: "PublisherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameImages");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
