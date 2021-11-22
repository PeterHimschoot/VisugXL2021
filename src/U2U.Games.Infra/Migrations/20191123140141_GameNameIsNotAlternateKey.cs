using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace U2U.Games.Infra.Migrations
{
    public partial class GameNameIsNotAlternateKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Games_Name",
                table: "Games");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Games",
                nullable: false,
                defaultValue: new DateTime(2019, 11, 23, 15, 1, 40, 648, DateTimeKind.Local).AddTicks(4528),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2019, 11, 21, 23, 26, 5, 404, DateTimeKind.Local).AddTicks(9211));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Games",
                nullable: false,
                defaultValue: new DateTime(2019, 11, 23, 15, 1, 40, 642, DateTimeKind.Local).AddTicks(1849),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2019, 11, 21, 23, 26, 5, 398, DateTimeKind.Local).AddTicks(9300));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2019, 11, 23, 15, 1, 40, 676, DateTimeKind.Local).AddTicks(7110), new DateTime(2019, 11, 23, 15, 1, 40, 676, DateTimeKind.Local).AddTicks(7161) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2019, 11, 23, 15, 1, 40, 676, DateTimeKind.Local).AddTicks(8132), new DateTime(2019, 11, 23, 15, 1, 40, 676, DateTimeKind.Local).AddTicks(8147) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2019, 11, 23, 15, 1, 40, 676, DateTimeKind.Local).AddTicks(8160), new DateTime(2019, 11, 23, 15, 1, 40, 676, DateTimeKind.Local).AddTicks(8165) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2019, 11, 21, 23, 26, 5, 404, DateTimeKind.Local).AddTicks(9211),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 11, 23, 15, 1, 40, 648, DateTimeKind.Local).AddTicks(4528));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2019, 11, 21, 23, 26, 5, 398, DateTimeKind.Local).AddTicks(9300),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 11, 23, 15, 1, 40, 642, DateTimeKind.Local).AddTicks(1849));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Games_Name",
                table: "Games",
                column: "Name");

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
        }
    }
}
