using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace U2U.Games.Infra.Migrations
{
    public partial class TimeStamps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "GameImages");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Games",
                nullable: false,
                defaultValue: new DateTime(2019, 11, 15, 16, 5, 6, 685, DateTimeKind.Local).AddTicks(2702));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Games",
                nullable: false,
                defaultValue: new DateTime(2019, 11, 15, 16, 5, 6, 691, DateTimeKind.Local).AddTicks(6286));

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GameImages",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "GameImages",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Qwirkle");

            migrationBuilder.UpdateData(
                table: "GameImages",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Rummikub");
        }
    }
}
