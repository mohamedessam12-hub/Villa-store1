using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVila_VillaAPi.Migrations
{
    /// <inheritdoc />
    public partial class changerolepass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "localUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 8, 22, 35, 13, 560, DateTimeKind.Local).AddTicks(9983));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 8, 22, 35, 13, 561, DateTimeKind.Local).AddTicks(47));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 8, 22, 35, 13, 561, DateTimeKind.Local).AddTicks(53));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 8, 22, 35, 13, 561, DateTimeKind.Local).AddTicks(57));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 8, 22, 35, 13, 561, DateTimeKind.Local).AddTicks(61));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Password",
                table: "localUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 8, 19, 31, 38, 185, DateTimeKind.Local).AddTicks(8444));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 8, 19, 31, 38, 185, DateTimeKind.Local).AddTicks(8503));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 8, 19, 31, 38, 185, DateTimeKind.Local).AddTicks(8508));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 8, 19, 31, 38, 185, DateTimeKind.Local).AddTicks(8512));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 8, 19, 31, 38, 185, DateTimeKind.Local).AddTicks(8516));
        }
    }
}
