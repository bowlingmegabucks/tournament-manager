using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewEnglandClassic.Database.Migrations;

public partial class ReDoTournamentAndDivision : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Tournaments",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Name = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                End = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                EntryFee = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                Games = table.Column<short>(type: "smallint", nullable: false),
                FinalsRatio = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: false),
                CashRatio = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: false),
                BowlingCenter = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Completed = table.Column<bool>(type: "tinyint(1)", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Tournaments", x => x.Id))
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Divisions",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Name = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Number = table.Column<short>(type: "smallint", nullable: false),
                TournamentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                MinimumAge = table.Column<short>(type: "smallint", nullable: true),
                MaximumAge = table.Column<short>(type: "smallint", nullable: true),
                MinimumAverage = table.Column<int>(type: "int", nullable: true),
                MaximumAverage = table.Column<int>(type: "int", nullable: true),
                HandicapPercentage = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: true),
                HandicapBase = table.Column<int>(type: "int", nullable: true),
                MaximumHandicapPerGame = table.Column<int>(type: "int", nullable: true),
                Gender = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Divisions", x => x.Id);
                table.ForeignKey(
                    name: "FK_Divisions_Tournaments_TournamentId",
                    column: x => x.TournamentId,
                    principalTable: "Tournaments",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_Divisions_TournamentId",
            table: "Divisions",
            column: "TournamentId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Divisions");

        migrationBuilder.DropTable(
            name: "Tournaments");
    }
}
