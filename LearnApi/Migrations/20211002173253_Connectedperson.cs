using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnApi.Migrations
{
    public partial class Connectedperson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonTobeConnecedId",
                table: "ConnectedPersons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonTobeConnecedId",
                table: "ConnectedPersons");
        }
    }
}
