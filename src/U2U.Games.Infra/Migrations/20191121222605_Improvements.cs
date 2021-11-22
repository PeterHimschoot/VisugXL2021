using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace U2U.Games.Infra.Migrations
{
    public partial class Improvements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameInBasket_ShoppingBasket_ShoppingBasketId",
                table: "GameInBasket");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBasket_Customer_CustomerId",
                table: "ShoppingBasket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingBasket",
                table: "ShoppingBasket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "ShoppingBasket",
                newName: "ShoppingBaskets");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingBasket_CustomerId",
                table: "ShoppingBaskets",
                newName: "IX_ShoppingBaskets_CustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Publishers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Games",
                nullable: false,
                defaultValue: new DateTime(2019, 11, 21, 23, 26, 5, 404, DateTimeKind.Local).AddTicks(9211),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2019, 11, 15, 16, 5, 6, 691, DateTimeKind.Local).AddTicks(6286));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Games",
                nullable: false,
                defaultValue: new DateTime(2019, 11, 21, 23, 26, 5, 398, DateTimeKind.Local).AddTicks(9300),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2019, 11, 15, 16, 5, 6, 685, DateTimeKind.Local).AddTicks(2702));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price_Amount",
                table: "Games",
                type: "decimal(4,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address_Street",
                table: "Customers",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address_City",
                table: "Customers",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Publishers_Name",
                table: "Publishers",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingBaskets",
                table: "ShoppingBaskets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2019, 11, 21, 23, 26, 5, 434, DateTimeKind.Local).AddTicks(3304), new DateTime(2019, 11, 21, 23, 26, 5, 434, DateTimeKind.Local).AddTicks(3353) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2019, 11, 21, 23, 26, 5, 434, DateTimeKind.Local).AddTicks(4320), new DateTime(2019, 11, 21, 23, 26, 5, 434, DateTimeKind.Local).AddTicks(4334) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2019, 11, 21, 23, 26, 5, 434, DateTimeKind.Local).AddTicks(4346), new DateTime(2019, 11, 21, 23, 26, 5, 434, DateTimeKind.Local).AddTicks(4349) });

            migrationBuilder.AddForeignKey(
                name: "FK_GameInBasket_ShoppingBaskets_ShoppingBasketId",
                table: "GameInBasket",
                column: "ShoppingBasketId",
                principalTable: "ShoppingBaskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBaskets_Customers_CustomerId",
                table: "ShoppingBaskets",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameInBasket_ShoppingBaskets_ShoppingBasketId",
                table: "GameInBasket");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBaskets_Customers_CustomerId",
                table: "ShoppingBaskets");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Publishers_Name",
                table: "Publishers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingBaskets",
                table: "ShoppingBaskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "ShoppingBaskets",
                newName: "ShoppingBasket");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingBaskets_CustomerId",
                table: "ShoppingBasket",
                newName: "IX_ShoppingBasket_CustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Publishers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2019, 11, 15, 16, 5, 6, 691, DateTimeKind.Local).AddTicks(6286),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 11, 21, 23, 26, 5, 404, DateTimeKind.Local).AddTicks(9211));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2019, 11, 15, 16, 5, 6, 685, DateTimeKind.Local).AddTicks(2702),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 11, 21, 23, 26, 5, 398, DateTimeKind.Local).AddTicks(9300));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price_Amount",
                table: "Games",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address_Street",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address_City",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingBasket",
                table: "ShoppingBasket",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2019, 11, 15, 16, 5, 6, 704, DateTimeKind.Local).AddTicks(3144), new DateTime(2019, 11, 15, 16, 5, 6, 704, DateTimeKind.Local).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2019, 11, 15, 16, 5, 6, 704, DateTimeKind.Local).AddTicks(4217), new DateTime(2019, 11, 15, 16, 5, 6, 704, DateTimeKind.Local).AddTicks(4236) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2019, 11, 15, 16, 5, 6, 704, DateTimeKind.Local).AddTicks(4251), new DateTime(2019, 11, 15, 16, 5, 6, 704, DateTimeKind.Local).AddTicks(4253) });

            migrationBuilder.AddForeignKey(
                name: "FK_GameInBasket_ShoppingBasket_ShoppingBasketId",
                table: "GameInBasket",
                column: "ShoppingBasketId",
                principalTable: "ShoppingBasket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBasket_Customer_CustomerId",
                table: "ShoppingBasket",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
