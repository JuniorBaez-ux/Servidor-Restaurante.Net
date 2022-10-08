using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServidorSocketPrimary.Migrations
{
    public partial class nuevoCampo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservaciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Cedula = table.Column<string>(type: "TEXT", nullable: false),
                    Mesa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservaciones", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Reservaciones",
                columns: new[] { "id", "Cedula", "Fecha", "Mesa", "Nombre" },
                values: new object[] { 1, "402-1274726-1", new DateTime(2022, 9, 28, 16, 31, 28, 15, DateTimeKind.Local).AddTicks(4356), "Mesa 1", "Juancito Francisco" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservaciones");
        }
    }
}
