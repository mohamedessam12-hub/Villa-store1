using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVila_VillaAPi.Migrations
{
    /// <inheritdoc />
    public partial class addlocaluser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "localUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localUsers", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "localUsers");

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
    }
}
