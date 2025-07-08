using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable

namespace NortheastMegabuck.Database.Migrations;

public partial class SquadScores : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "SquadScores",
            columns: table => new
            {
                BowlerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                SquadId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Game = table.Column<short>(type: "smallint", nullable: false),
                Score = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SquadScores", x => new { x.BowlerId, x.SquadId, x.Game });
                table.ForeignKey(
                    name: "FK_SquadScores_Bowlers_BowlerId",
                    column: x => x.BowlerId,
                    principalTable: "Bowlers",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_SquadScores_Squads_SquadId",
                    column: x => x.SquadId,
                    principalTable: "Squads",
                    principalColumn: "Id");
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_SquadScores_SquadId",
            table: "SquadScores",
            column: "SquadId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "SquadScores");
    }
}
