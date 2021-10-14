using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnApi.Migrations
{
    public partial class Cult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GvariLatinuri",
                table: "FizikPiris",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaxeliLatinuri",
                table: "FizikPiris",
                type: "varchar(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GvariLatinuri",
                table: "FizikPiris");

            migrationBuilder.DropColumn(
                name: "SaxeliLatinuri",
                table: "FizikPiris");
        }
    }
}
