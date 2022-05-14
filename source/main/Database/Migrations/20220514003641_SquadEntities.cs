using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewEnglandClassic.Database.Migrations;

public partial class SquadEntities : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Squads",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                TournamentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                MaxPerPair = table.Column<int>(type: "int", nullable: false),
                Complete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                SquadType = table.Column<int>(type: "int", nullable: false),
                EntryFee = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                Games = table.Column<short>(type: "smallint", nullable: true),
                CashRatio = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Squads", x => x.Id);
                table.ForeignKey(
                    name: "FK_Squads_Tournaments_TournamentId",
                    column: x => x.TournamentId,
                    principalTable: "Tournaments",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_Squads_TournamentId",
            table: "Squads",
            column: "TournamentId");
    }

    protected override void Down(MigrationBuilder migrationBuilder) 
        => migrationBuilder.DropTable(name: "Squads");
}
