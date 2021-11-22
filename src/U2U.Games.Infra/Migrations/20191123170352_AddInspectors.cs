using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace U2U.Games.Infra.Migrations
{
    public partial class AddInspectors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Games",
                nullable: false,
                defaultValue: new DateTime(2019, 11, 23, 18, 3, 52, 97, DateTimeKind.Local).AddTicks(3007),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2019, 11, 23, 15, 1, 40, 648, DateTimeKind.Local).AddTicks(4528));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Games",
                nullable: false,
                defaultValue: new DateTime(2019, 11, 23, 18, 3, 52, 91, DateTimeKind.Local).AddTicks(416),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2019, 11, 23, 15, 1, 40, 642, DateTimeKind.Local).AddTicks(1849));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2019, 11, 23, 18, 3, 52, 124, DateTimeKind.Local).AddTicks(3457), new DateTime(2019, 11, 23, 18, 3, 52, 124, DateTimeKind.Local).AddTicks(3504) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2019, 11, 23, 18, 3, 52, 124, DateTimeKind.Local).AddTicks(4438), new DateTime(2019, 11, 23, 18, 3, 52, 124, DateTimeKind.Local).AddTicks(4453) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2019, 11, 23, 18, 3, 52, 124, DateTimeKind.Local).AddTicks(4466), new DateTime(2019, 11, 23, 18, 3, 52, 124, DateTimeKind.Local).AddTicks(4469) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2019, 11, 23, 15, 1, 40, 648, DateTimeKind.Local).AddTicks(4528),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 11, 23, 18, 3, 52, 97, DateTimeKind.Local).AddTicks(3007));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2019, 11, 23, 15, 1, 40, 642, DateTimeKind.Local).AddTicks(1849),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 11, 23, 18, 3, 52, 91, DateTimeKind.Local).AddTicks(416));

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
    }
}
