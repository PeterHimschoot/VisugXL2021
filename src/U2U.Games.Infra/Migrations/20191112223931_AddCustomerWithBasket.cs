using Microsoft.EntityFrameworkCore.Migrations;

namespace U2U.Games.Infra.Migrations
{
    public partial class AddCustomerWithBasket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Publishers_PublisherId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherId",
                table: "Games",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Games",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Games_Name",
                table: "Games",
                column: "Name");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Address_Street = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingBasket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingBasket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingBasket_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameInBasket",
                columns: table => new
                {
                    ShoppingBasketId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameInBasket", x => new { x.ShoppingBasketId, x.GameId });
                    table.ForeignKey(
                        name: "FK_GameInBasket_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameInBasket_ShoppingBasket_ShoppingBasketId",
                        column: x => x.ShoppingBasketId,
                        principalTable: "ShoppingBasket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_Name",
                table: "Games",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameInBasket_GameId",
                table: "GameInBasket",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBasket_CustomerId",
                table: "ShoppingBasket",
                column: "CustomerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Publishers_PublisherId",
                table: "Games",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Publishers_PublisherId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "GameInBasket");

            migrationBuilder.DropTable(
                name: "ShoppingBasket");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Games_Name",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_Name",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherId",
                table: "Games",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Games",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Publishers_PublisherId",
                table: "Games",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
