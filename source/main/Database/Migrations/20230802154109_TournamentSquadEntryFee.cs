using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NortheastMegabuck.Database.Migrations;

/// <inheritdoc />
public partial class TournamentSquadEntryFee : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "EntryFee",
            table: "Squads",
            newName: "SweeperEntryFee");

        migrationBuilder.AddColumn<decimal>(
            name: "SquadEntryFee",
            table: "Squads",
            type: "decimal(5,2)",
            precision: 5,
            scale: 2,
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "SquadEntryFee",
            table: "Squads");

        migrationBuilder.RenameColumn(
            name: "SweeperEntryFee",
            table: "Squads",
            newName: "EntryFee");
    }
}
