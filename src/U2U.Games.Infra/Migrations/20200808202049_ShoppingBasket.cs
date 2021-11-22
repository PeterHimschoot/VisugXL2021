using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace U2U.Games.Infra.Migrations
{
    public partial class ShoppingBasket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBaskets_Customers_CustomerId",
                table: "ShoppingBaskets");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingBaskets_CustomerId",
                table: "ShoppingBaskets");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ShoppingBaskets",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Games",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 8, 22, 20, 48, 832, DateTimeKind.Local).AddTicks(1337),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 8, 8, 22, 9, 31, 672, DateTimeKind.Local).AddTicks(5255));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Games",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 8, 22, 20, 48, 823, DateTimeKind.Local).AddTicks(5940),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 8, 8, 22, 9, 31, 663, DateTimeKind.Local).AddTicks(6976));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2020, 8, 8, 22, 20, 48, 861, DateTimeKind.Local).AddTicks(9677), new DateTime(2020, 8, 8, 22, 20, 48, 861, DateTimeKind.Local).AddTicks(9742) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2020, 8, 8, 22, 20, 48, 862, DateTimeKind.Local).AddTicks(778), new DateTime(2020, 8, 8, 22, 20, 48, 862, DateTimeKind.Local).AddTicks(793) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2020, 8, 8, 22, 20, 48, 862, DateTimeKind.Local).AddTicks(810), new DateTime(2020, 8, 8, 22, 20, 48, 862, DateTimeKind.Local).AddTicks(813) });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBaskets_CustomerId",
                table: "ShoppingBaskets",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBaskets_Customers_CustomerId",
                table: "ShoppingBaskets",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBaskets_Customers_CustomerId",
                table: "ShoppingBaskets");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingBaskets_CustomerId",
                table: "ShoppingBaskets");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ShoppingBaskets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 8, 22, 9, 31, 672, DateTimeKind.Local).AddTicks(5255),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 8, 22, 20, 48, 832, DateTimeKind.Local).AddTicks(1337));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 8, 22, 9, 31, 663, DateTimeKind.Local).AddTicks(6976),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 8, 22, 20, 48, 823, DateTimeKind.Local).AddTicks(5940));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2020, 8, 8, 22, 9, 31, 708, DateTimeKind.Local).AddTicks(3657), new DateTime(2020, 8, 8, 22, 9, 31, 708, DateTimeKind.Local).AddTicks(3723) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2020, 8, 8, 22, 9, 31, 708, DateTimeKind.Local).AddTicks(4882), new DateTime(2020, 8, 8, 22, 9, 31, 708, DateTimeKind.Local).AddTicks(4900) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2020, 8, 8, 22, 9, 31, 708, DateTimeKind.Local).AddTicks(4915), new DateTime(2020, 8, 8, 22, 9, 31, 708, DateTimeKind.Local).AddTicks(4918) });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBaskets_CustomerId",
                table: "ShoppingBaskets",
                column: "CustomerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBaskets_Customers_CustomerId",
                table: "ShoppingBaskets",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
