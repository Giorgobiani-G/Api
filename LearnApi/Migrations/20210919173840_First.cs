using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnApi.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FizikPiris",
                columns: table => new
                {
                    FizikPiriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Saxeli = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gvari = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Piradoba = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DabTarigi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Misamarti = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FizikPiris", x => x.FizikPiriId);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfo",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Info = table.Column<int>(type: "int", nullable: false),
                    FizikPiriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfo", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_ContactInfo_FizikPiris_FizikPiriId",
                        column: x => x.FizikPiriId,
                        principalTable: "FizikPiris",
                        principalColumn: "FizikPiriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FizikPiriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_FizikPiris_FizikPiriId",
                        column: x => x.FizikPiriId,
                        principalTable: "FizikPiris",
                        principalColumn: "FizikPiriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_FizikPiriId",
                table: "ContactInfo",
                column: "FizikPiriId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_FizikPiriId",
                table: "Images",
                column: "FizikPiriId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInfo");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "FizikPiris");
        }
    }
}
