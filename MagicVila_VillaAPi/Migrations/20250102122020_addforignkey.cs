using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVila_VillaAPi.Migrations
{
    /// <inheritdoc />
    public partial class addforignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaId",
                table: "villaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_villaNumbers_VillaId",
                table: "villaNumbers",
                column: "VillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_villaNumbers_Villas_VillaId",
                table: "villaNumbers",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_villaNumbers_Villas_VillaId",
                table: "villaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_villaNumbers_VillaId",
                table: "villaNumbers");

            migrationBuilder.DropColumn(
                name: "VillaId",
                table: "villaNumbers");

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
    }
}
