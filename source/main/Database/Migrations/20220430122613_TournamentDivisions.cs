using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewEnglandClassic.Database.Migrations;

public partial class TournamentDivisions : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "TournamentDivisions",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Number = table.Column<short>(type: "smallint", nullable: false),
                TournamentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                MinimumAge = table.Column<short>(type: "smallint", nullable: true),
                MaximumAge = table.Column<short>(type: "smallint", nullable: true),
                MinimumAverage = table.Column<int>(type: "int", nullable: true),
                MaximumAverage = table.Column<int>(type: "int", nullable: true),
                HandicapPercentage = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                HandicapBase = table.Column<int>(type: "int", nullable: true),
                MaximumHandicapPerGame = table.Column<int>(type: "int", nullable: true),
                Gender = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TournamentDivisions", x => x.Id);
                table.ForeignKey(
                    name: "FK_TournamentDivisions_Tournaments_TournamentId",
                    column: x => x.TournamentId,
                    principalTable: "Tournaments",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_TournamentDivisions_TournamentId",
            table: "TournamentDivisions",
            column: "TournamentId");
    }

    protected override void Down(MigrationBuilder migrationBuilder) 
        => migrationBuilder.DropTable(name: "TournamentDivisions");
}
