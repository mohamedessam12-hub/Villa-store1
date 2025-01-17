using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVila_VillaAPi.Migrations
{
    /// <inheritdoc />
    public partial class nulable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Amenity",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SpecialDetails",
                table: "villaNumbers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 5, 17, 0, 19, 162, DateTimeKind.Local).AddTicks(3071));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 5, 17, 0, 19, 162, DateTimeKind.Local).AddTicks(3128));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 5, 17, 0, 19, 162, DateTimeKind.Local).AddTicks(3134));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 5, 17, 0, 19, 162, DateTimeKind.Local).AddTicks(3139));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 5, 17, 0, 19, 162, DateTimeKind.Local).AddTicks(3143));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Amenity",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SpecialDetails",
                table: "villaNumbers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 2, 14, 20, 17, 454, DateTimeKind.Local).AddTicks(1990));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 2, 14, 20, 17, 454, DateTimeKind.Local).AddTicks(2054));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 2, 14, 20, 17, 454, DateTimeKind.Local).AddTicks(2062));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 2, 14, 20, 17, 454, DateTimeKind.Local).AddTicks(2067));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 2, 14, 20, 17, 454, DateTimeKind.Local).AddTicks(2072));
        }
    }
}
