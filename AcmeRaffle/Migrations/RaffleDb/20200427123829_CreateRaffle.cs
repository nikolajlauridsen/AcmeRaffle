using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AcmeRaffle.Migrations.RaffleDb
{
    public partial class CreateRaffle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SoldProducts",
                columns: table => new
                {
                    SoldProductId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SerialNumber = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldProducts", x => x.SoldProductId);
                });

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    RaffleEntryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    SoldProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.RaffleEntryId);
                    table.ForeignKey(
                        name: "FK_Entries_SoldProducts_SoldProductId",
                        column: x => x.SoldProductId,
                        principalTable: "SoldProducts",
                        principalColumn: "SoldProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entries_SoldProductId",
                table: "Entries",
                column: "SoldProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "SoldProducts");
        }
    }
}
