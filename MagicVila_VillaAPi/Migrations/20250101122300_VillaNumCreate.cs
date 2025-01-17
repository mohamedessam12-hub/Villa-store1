using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVila_VillaAPi.Migrations
{
    /// <inheritdoc />
    public partial class VillaNumCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "villaNumbers",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_villaNumbers", x => x.VillaNo);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 1, 14, 22, 57, 422, DateTimeKind.Local).AddTicks(190));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 1, 14, 22, 57, 422, DateTimeKind.Local).AddTicks(251));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 1, 14, 22, 57, 422, DateTimeKind.Local).AddTicks(257));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 1, 14, 22, 57, 422, DateTimeKind.Local).AddTicks(261));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreateDate",
                value: new DateTime(2025, 1, 1, 14, 22, 57, 422, DateTimeKind.Local).AddTicks(266));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "villaNumbers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreateDate",
                value: new DateTime(2024, 12, 30, 12, 52, 27, 551, DateTimeKind.Local).AddTicks(2));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreateDate",
                value: new DateTime(2024, 12, 30, 12, 52, 27, 551, DateTimeKind.Local).AddTicks(62));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreateDate",
                value: new DateTime(2024, 12, 30, 12, 52, 27, 551, DateTimeKind.Local).AddTicks(66));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreateDate",
                value: new DateTime(2024, 12, 30, 12, 52, 27, 551, DateTimeKind.Local).AddTicks(70));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreateDate",
                value: new DateTime(2024, 12, 30, 12, 52, 27, 551, DateTimeKind.Local).AddTicks(74));
        }
    }
}
