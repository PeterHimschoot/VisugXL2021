using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace U2U.Games.Infra.Migrations
{
    public partial class UpgradeTo5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Games");

            migrationBuilder.AlterColumn<string>(
                name: "Price_Currency",
                table: "Games",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price_Amount",
                table: "Games",
                type: "decimal(4,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UtcCreated",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UtcModified",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UtcCreated",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "UtcModified",
                table: "Games");

            migrationBuilder.AlterColumn<string>(
                name: "Price_Currency",
                table: "Games",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price_Amount",
                table: "Games",
                type: "decimal(4,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 8, 22, 20, 48, 823, DateTimeKind.Local).AddTicks(5940));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 8, 22, 20, 48, 832, DateTimeKind.Local).AddTicks(1337));

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
        }
    }
}
