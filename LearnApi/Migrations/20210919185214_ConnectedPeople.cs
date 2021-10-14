using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnApi.Migrations
{
    public partial class ConnectedPeople : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConnectedPersons",
                columns: table => new
                {
                    ConnectedPersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FizikPiriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectedPersons", x => x.ConnectedPersonId);
                    table.ForeignKey(
                        name: "FK_ConnectedPersons_FizikPiris_FizikPiriId",
                        column: x => x.FizikPiriId,
                        principalTable: "FizikPiris",
                        principalColumn: "FizikPiriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectedPersons_FizikPiriId",
                table: "ConnectedPersons",
                column: "FizikPiriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectedPersons");
        }
    }
}
