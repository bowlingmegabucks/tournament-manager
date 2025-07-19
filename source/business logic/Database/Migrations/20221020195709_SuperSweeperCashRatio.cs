using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable

namespace BowlingMegabucks.TournamentManager.Database.Migrations;

public partial class SuperSweeperCashRatio : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<decimal>(
            name: "SuperSweperCashRatio",
            table: "Tournaments",
            type: "decimal(3,1)",
            precision: 3,
            scale: 1,
            nullable: false,
            defaultValue: 0m);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "SuperSweperCashRatio",
            table: "Tournaments");
    }
}
